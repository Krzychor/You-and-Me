using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameObject rockUI;
    public RockSkipper skipper;

    bool isUsed = false;



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isUsed && collision.gameObject.tag == "Player")
        {
            isUsed = true;
            skipper.player = collision.gameObject;
            rockUI.SetActive(true);
            BlockPlayer(collision.gameObject);
        }
    }

    void BlockPlayer(GameObject player)
    {
        Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();
        if (rigid)
            rigid.velocity = new Vector2(0, 0);

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.enabled = false;
        playerMovement.SetAnimationToIdle();
        FindObjectOfType<PlayerSwitch>().enabled = false;
    }



}
