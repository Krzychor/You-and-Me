using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlink : MonoBehaviour
{
    public GameObject oko;


    private void Start()
    {
        StartCoroutine(cos());
    }

    public IEnumerator cos()
    {
        while (true)
        {
            oko.SetActive(true);
            yield return new WaitForSecondsRealtime(0.3f);
            oko.SetActive(false);
            yield return new WaitForSecondsRealtime(2f);
        }
    }

    private void Update()
    {
   //     cos();
    }

}
