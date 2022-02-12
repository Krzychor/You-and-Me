using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    static Canvas canva_;
    public Canvas Canva;




    void Start()
    {
        canva_ = Canva;
        DisplayText(new Vector2(7, 6), "bla bla bla..\nbla bla..bla bla \nbla..bla bloop bla..");
    }

    public static void DisplayText(Vector2 position, string text)
    {
        GameObject G = Instantiate(canva_.gameObject, position, Quaternion.identity);
        G.GetComponentInChildren<Text>().text = text;
    }
}
