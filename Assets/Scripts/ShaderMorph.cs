using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct SampleData
{
    public Vector3 position;
    public Color32 color;
}

public struct MorphData
{
    public float startSize;
    public float targetSize;
    public SampleData[] targetValues;
    public SampleData[] startValues;
}



[System.Serializable]
public abstract class ShaderMorphStrategy
{
    protected int particleCount;
    protected MorphData morphData;


    public void SetMorphData(int particleCount, MorphData morphData)
    {
        this.morphData = morphData;
        this.particleCount = particleCount;
    }

    public abstract void OnUpdate(ParticleSystem.Particle[] particles);
    public abstract void CreateParticles(ParticleSystem.Particle[] particles);
}

static class MyExtensions
{
    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}


[System.Serializable]
public class SimpleMorphStrategy : ShaderMorphStrategy
{
    [SerializeField]
    public AnimationCurve animationCurve = new AnimationCurve();
    [SerializeField, Min(0)]
    float afterIdleTime = 0;


    public override void CreateParticles(ParticleSystem.Particle[] particles)
    {
        if (animationCurve.length == 0)
            Debug.LogWarning("emptyCurve");

        float lifetime = animationCurve.length == 0 ? afterIdleTime : animationCurve.keys[animationCurve.length - 1].time + afterIdleTime;


        morphData.targetValues.Shuffle();

        for (int i = 0; i < particleCount; i++)
        {
            particles[i].startColor = morphData.startValues[i].color;
            particles[i].position = morphData.startValues[i].position;
            particles[i].startSize = morphData.startSize;
            particles[i].remainingLifetime = lifetime;
            particles[i].startLifetime = particles[i].remainingLifetime;

        }
    }

    public override void OnUpdate(ParticleSystem.Particle[] particles)
    {
        for (int i = 0; i < particleCount; i++)
        {
            float passedTime = particles[i].startLifetime - particles[i].remainingLifetime;
            float t = animationCurve.Evaluate(passedTime);
            t = Mathf.Clamp01(t);
            particles[i].position = Vector3.Lerp(morphData.startValues[i].position, morphData.targetValues[i].position, t);
            particles[i].startSize = Mathf.Lerp(morphData.startSize, morphData.targetSize, t);
            particles[i].startColor = Color.Lerp(morphData.startValues[i].color, morphData.targetValues[i].color, t);
        }
    }
}

[System.Serializable]
public class ExplodeMorphStrategy : ShaderMorphStrategy
{
    [SerializeField]
    float minExplosionForce = 1;
    [SerializeField]
    float maxExplosionForce = 10;
    [SerializeField]
    float slow = 1f;

    [SerializeField]
    float pullTime = 0.7f;

    Vector3[] normalVel;
    Vector3[] dirSlow;
    Vector3[] stopPos;
    float[] ts;

    [SerializeField]
    float magDelay = 1;
    [SerializeField]
    float fullMagTime = 6;
    float magStr = 0;
    float passedTime = 0;

    public override void CreateParticles(ParticleSystem.Particle[] particles)
    {
        passedTime = 0;
        magStr = 0;
        normalVel = new Vector3[particles.Length];
        dirSlow = new Vector3[particles.Length];
        stopPos = new Vector3[particles.Length];
        ts = new float[particles.Length];

        for (int i = 0; i < particleCount; i++)
            stopPos[i].x = 0;
        for (int i = 0; i < particleCount; i++)
            ts[i] = 0;

        //    morphData.targetValues.Shuffle();

        for (int i = 0; i < particleCount; i++)
        {
            particles[i].startColor = morphData.startValues[i].color;
            particles[i].position = morphData.startValues[i].position;
            particles[i].startSize = morphData.startSize;
            particles[i].remainingLifetime = pullTime + 20;
            particles[i].startLifetime = particles[i].remainingLifetime;


            Vector3 dir = new Vector3
            {
                x = UnityEngine.Random.Range(-1.0f, 1.0f),
                y = UnityEngine.Random.Range(-1.0f, 1.0f),
                z = 0
            };
            dir.Normalize();

            particles[i].velocity = dir * UnityEngine.Random.Range(minExplosionForce, maxExplosionForce);
            normalVel[i] = particles[i].velocity;
            dirSlow[i] = dir * slow;
        }
    }

    public override void OnUpdate(ParticleSystem.Particle[] particles)
    {
        passedTime += Time.deltaTime;
        if(passedTime > magDelay)
        {
            magStr = Mathf.Clamp(passedTime - magDelay, 0, fullMagTime)/fullMagTime;
        }

        for (int i = 0; i < particleCount; i++)
        {
            Vector3 dir = morphData.targetValues[i].position - particles[i].position;
            float dist = dir.magnitude;
            dir.Normalize();



            if (dist < 0.1)
            {
                particles[i].position = morphData.targetValues[i].position;
                particles[i].startColor = morphData.targetValues[i].color;
                particles[i].startSize = morphData.targetSize;
                particles[i].velocity = Vector3.zero;
            }
            else
            {
                particles[i].velocity = Vector3.Lerp(normalVel[i], dir * Mathf.Min(1, (passedTime - magDelay)/fullMagTime), magStr);
                particles[i].startColor = Color.Lerp(morphData.startValues[i].color, morphData.targetValues[i].color, magStr);
                particles[i].startSize = Mathf.Lerp(morphData.startSize, morphData.targetSize, magStr);
            }


        }
    }

    public void OnUpdate2(ParticleSystem.Particle[] particles)
    {
        for (int i = 0; i < particleCount; i++)
        {

            if (particles[i].velocity.sqrMagnitude > 0.01)
            {
                particles[i].velocity -= dirSlow[i]*Time.deltaTime;
         //       particles[i].velocity = new Vector3(Mathf.Clamp01(particles[i].velocity.x), Mathf.Clamp01(particles[i].velocity.y), 0);
            }
            else
            {
                if(stopPos[i].x == 0)
                {
                    stopPos[i] = particles[i].position;
                    particles[i].velocity = Vector3.zero;
                }
                else
                {
                    particles[i].position = Vector3.Lerp(stopPos[i], morphData.targetValues[i].position, ts[i]);
                    particles[i].startSize = Mathf.Lerp(morphData.startSize, morphData.targetSize, ts[i]);
                    particles[i].startColor = Color.Lerp(morphData.startValues[i].color, morphData.targetValues[i].color, ts[i]);
                    ts[i] += Time.deltaTime / pullTime;
                }

            }

        }
    }
}


[RequireComponent(typeof(ParticleSystem))]
public class ShaderMorph : MonoBehaviour
{
    [SerializeField, Min(0)]
    int maxParticles = 100000;

    [SerializeField]
    SpriteRenderer sourceRenderer;
    [SerializeField]
    SpriteRenderer targetRenderer;

    new ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;

    [SerializeReference]
    public ShaderMorphStrategy strategy = new SimpleMorphStrategy();

    bool isMorphing = false;
    int particlesLastFrame = 0;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        var m = particleSystem.main;
        m.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        isMorphing = false;
    }

    Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
        Color[] rpixels = result.GetPixels(0);
        float incX = (1.0f / (float)targetWidth);
        float incY = (1.0f / (float)targetHeight);
        for (int px = 0; px < rpixels.Length; px++)
        {
            rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * ((float)Mathf.Floor(px / targetWidth)));
        }
        result.SetPixels(rpixels, 0);
        result.Apply();
        return result;
    }

    SampleData[] SamplePositions(int N, SpriteRenderer renderer, out float particleSize)
    {
   //     if (!texture.isReadable)
    //        Debug.LogError("texure is unreadable");
        SampleData[] array = new SampleData[N];

        List<SampleData> eligible = new List<SampleData>();

        float ratio = (float)renderer.sprite.texture.width / (float)renderer.sprite.texture.height;
        Vector2 targetSize = new Vector2(renderer.sprite.texture.width, renderer.sprite.texture.height);
        while(Mathf.FloorToInt(targetSize.x)*Mathf.FloorToInt(targetSize.y) > maxParticles)
        {
            targetSize.y--;
            targetSize.x -= ratio;
        }
        targetSize.x = Mathf.FloorToInt(targetSize.x);
        targetSize.y = Mathf.FloorToInt(targetSize.y);
        Texture2D texture = ScaleTexture(renderer.sprite.texture, (int)targetSize.x, (int)targetSize.y);
     //   Debug.LogWarning("targetSize = " + targetSize);

        float particleSizeX = renderer.sprite.bounds.size.x * 1.0f / targetSize.x;
        float particleSizeY = renderer.sprite.bounds.size.y * 1.0f / targetSize.y;
        particleSize = particleSizeX;
        particleSize = Mathf.Max(particleSizeX, particleSizeY);

        Color32[] colors = texture.GetPixels32(0);
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                if(colors[y*texture.width + x].a > 0)
                {
                    SampleData data = new SampleData();
                    data.position = new Vector3(x, y, 0);
                    data.color = colors[y * texture.width + x];
                    eligible.Add(data);
                }
            }
        }

        if (eligible.Count == 0)
            Debug.LogError("texure is fully transparent!");
        for(int i = 0; i < eligible.Count && i < N; i++)
            array[i] = eligible[i];

        
        for(int remaining = N - eligible.Count; remaining > 0; remaining--)
        {
            int index = Random.Range(0, eligible.Count - 1);
            array[N-remaining] = eligible[index];
        }

        return array;
    }

    int DetermineParticlesNeeded(SpriteRenderer renderer)
    {
        float ratio = (float)renderer.sprite.texture.width / (float)renderer.sprite.texture.height;
        Vector2 targetSize = new Vector2(renderer.sprite.texture.width, renderer.sprite.texture.height);
        while (Mathf.FloorToInt(targetSize.x) * Mathf.FloorToInt(targetSize.y) > maxParticles)
        {
            targetSize.y--;
            targetSize.x -= ratio;
        }
        targetSize.x = Mathf.FloorToInt(targetSize.x);
        targetSize.y = Mathf.FloorToInt(targetSize.y);
        return (int)targetSize.x * (int)targetSize.y;
    }

    void Morph()
    {
        isMorphing = true;
        int n1 = DetermineParticlesNeeded(sourceRenderer);
        int n2 = DetermineParticlesNeeded(targetRenderer);
        int N = Mathf.Max(n1, n2);

        MorphData morphData = new MorphData();
        morphData.startValues = SamplePositions(N, sourceRenderer, out morphData.startSize);
        morphData.targetValues = SamplePositions(N, targetRenderer, out morphData.targetSize);


        particles = new ParticleSystem.Particle[N];

        for (int i = 0; i < N; i++)
        {
            Vector3 startPos = sourceRenderer.gameObject.transform.position;
            startPos += new Vector3(morphData.startValues[i].position.x * morphData.startSize * sourceRenderer.transform.lossyScale.x,
                morphData.startValues[i].position.y * morphData.startSize * sourceRenderer.transform.lossyScale.y, 0);
            startPos += new Vector3(sourceRenderer.sprite.bounds.min.x * sourceRenderer.transform.lossyScale.x,
                sourceRenderer.sprite.bounds.min.y * sourceRenderer.transform.lossyScale.y, 0);
            morphData.startValues[i].position = startPos;


            Vector3 target = targetRenderer.gameObject.transform.position;
            target += new Vector3(morphData.targetValues[i].position.x * morphData.targetSize * targetRenderer.transform.lossyScale.x,
                morphData.targetValues[i].position.y * morphData.targetSize * targetRenderer.transform.lossyScale.y, 0);
            target += new Vector3(targetRenderer.sprite.bounds.min.x * targetRenderer.transform.lossyScale.x,
                targetRenderer.sprite.bounds.min.y * targetRenderer.transform.lossyScale.y, 0);
            morphData.targetValues[i].position = target;
        }

        strategy.SetMorphData(N, morphData);
        strategy.CreateParticles(particles);

        particleSystem.SetParticles(particles, particles.Length);
        particleSystem.Play();
    }

    void UpdateMorphing()
    {
        if (particleSystem.GetParticles(particles) > 0)
        {
            strategy.OnUpdate(particles);
            particleSystem.SetParticles(particles, particles.Length);
        }
        else
            isMorphing = false;
    }

    void Update()
    {
        if (isMorphing)
            UpdateMorphing();

        if (!isMorphing && Input.GetKeyDown(KeyCode.Space))
            Morph();
    }

}
