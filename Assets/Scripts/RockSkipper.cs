using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSkipper : MonoBehaviour
{
    public GameObject myCanva;
    float time = 0;

    
    public GameObject player;



    void Update()
    {
        time += Time.time;
        if(time > 1)
            if (Input.GetButtonDown("Fire1"))
            {
                UnblockPlayer();
                myCanva.SetActive(false);
            }

    }
    void UnblockPlayer()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        FindObjectOfType<PlayerSwitch>().enabled = true;
    }
}
