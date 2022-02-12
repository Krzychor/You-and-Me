using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    float distanceToGround = 0;
    public float accepted = 0.2f;



    public bool IsOnGround()
    {
        return distanceToGround < accepted;
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject.tag != "Player")
        {
    //        Debug.Log(hit.collider.gameObject.name);
            distanceToGround = hit.distance;
        }
        else
            distanceToGround = Mathf.Infinity;
    }
}
