using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtefactCounter : MonoBehaviour
{
    public DialogCheck check;
    public Text text;




    void Update()
    {
        int x = check.collector.counter;
        if (x > check.required)
            x = check.required;
        text.text = x.ToString() + "/" + check.required.ToString();
    }
}
