using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Part
{
    public Sprite characterImage;
    public string characterName;
    [MultilineAttribute]
    public string dialogue;
    public float readTime;
}

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/Dialog", order = 1)]
public class Dialog : ScriptableObject
{
    public List<Part> parts;


}

