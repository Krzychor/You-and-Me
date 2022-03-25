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
        int x = Mathf.Min(check.collector.counter, check.required);
        text.text = x.ToString() + "/" + check.required.ToString();
    }
}
