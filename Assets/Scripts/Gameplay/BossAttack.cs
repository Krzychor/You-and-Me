using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{

  //  GameObject leftHand;
 //   GameObject rightHand;


    public void Init(GameObject left, GameObject right)
    {
    //    leftHand = left;
    //    rightHand = right;
    }

    public abstract IEnumerator StartAttack();

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
