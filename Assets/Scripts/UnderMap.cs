using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//reset scene when touched by player
public class UnderMap : MonoBehaviour
{



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.gameObject.GetComponent<Player>()?.Die();
    }
}
