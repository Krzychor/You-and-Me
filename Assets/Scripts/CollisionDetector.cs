using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public bool isColliding()
    {
        return count != 0;
    }

    int count = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.isTrigger)
           count++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger)
            count--;
    }



}
