using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    SpriteRenderer[] renderers;


    public void RotateLeft()
    {
        foreach (SpriteRenderer renderer in renderers)
            renderer.flipX = true;
    }

    public void RotateRight()
    {
        foreach (SpriteRenderer renderer in renderers)
            renderer.flipX = false;
    }



    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }
}
