using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public struct Vector2_Serializable
{
    public Vector2_Serializable(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public float x { get; set; }
    public float y { get; set; }

    public static implicit operator Vector2_Serializable(Vector2 v)
    {
        Vector2_Serializable vs = new Vector2_Serializable();
        vs.x = v.x;
        vs.y = v.y;
        return vs;
    }
}