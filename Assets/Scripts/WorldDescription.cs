using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldDescription", menuName = "ScriptableObjects/WorldDescription", order = 2)]
public class WorldDescription : ScriptableObject
{
    [System.Serializable]
    public struct Part
    {
        public string text;
        public float readTime;
    }


    public List<Part> parts;


}
