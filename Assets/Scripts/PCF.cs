using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCF : MonoBehaviour
{
    //   [SerializeField] Transform target;
    public float teleportDistance = 10;
    public Transform target;
    public Vector3 tr;
    public float smoothCamera = 0.125f;
    private Vector3 velocity = Vector3.zero;
    // Update is called once per frame
    void LateUpdate()
    { 

        Vector3 desiredPosition = target.position + tr;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothCamera);

        float d = (desiredPosition - transform.position).magnitude;
        if (d >= teleportDistance)
            transform.position = desiredPosition;
    }
}
