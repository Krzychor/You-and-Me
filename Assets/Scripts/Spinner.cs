using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spinner : MonoBehaviour
{
    public float spinSpeed = 90;
    float rotation = 0;


    void Update()
    {
        rotation += 90 * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
