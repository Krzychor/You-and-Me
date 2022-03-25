using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBlinkEffect : MonoBehaviour
{
    public Color tintColor;
    public float duration = 4;
    public Shader blinkShader;

    List<Renderer> renderers;

    float tintAmount = 0;
    float time = 0;



    void Start()
    {
        Player.OnDamagePlayer += Blink;
        renderers = new List<Renderer>(GetComponentsInChildren<Renderer>());

        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            foreach (Material material in materials)
            {
                tintAmount = 0;
                material.shader = blinkShader;
                material.SetColor("tint", tintColor);
                material.SetFloat("tintAmount", tintAmount);
            }
            renderer.materials = materials;
        }
    }


    public void Blink()
    {
        tintAmount += 1;
    }

     void Update()
    {
        time += Time.deltaTime;
        time = Mathf.Min(time, duration);

        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            foreach(Material material in materials)
            {
                tintAmount = Mathf.Lerp(tintAmount, 0, Time.deltaTime);
                tintAmount = Mathf.Clamp01(tintAmount);
                material.SetFloat("tintAmount", tintAmount);
            }
            renderer.materials = materials;
        }
    }
}
