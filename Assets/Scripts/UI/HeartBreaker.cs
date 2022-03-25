using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBreaker : MonoBehaviour
{
    public float explosionStrength = 1;
    public float fadeDuration = 2;


    void Start()
    {
        StartCoroutine(Fade(GetComponentsInChildren<Renderer>()));

        Rigidbody2D[] rigids = GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D rigid in rigids)
        {
            Vector2 dir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 0));

            dir.Normalize();
            rigid.AddForce(dir * explosionStrength);
        }
    }

    IEnumerator Fade(Renderer[] renderers)
    {
        float time = 0;
        foreach (Renderer renderer in renderers)
        {
            Color color = renderer.material.color;
            color.a = 1;
            renderer.material.color = color;
        }

        while(time < fadeDuration)
        {
            yield return null;
            time += Time.deltaTime;
            float t = time / fadeDuration;
            foreach (Renderer renderer in renderers)
            {
                Color color = renderer.material.color;
                color.a = t;
                renderer.material.color = color;
            }

        }
        //  yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
