using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightRock : MonoBehaviour
{
    public GameObject skala;
    public float time = 6;





    IEnumerator des()
    {
        yield return new WaitForSeconds(time);

        skala.SetActive(false);
    }

    void Start()
    {
        StartCoroutine(des());
    }

}
