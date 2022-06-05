using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSlash : BossAttack
{
    [SerializeField]
    GameObject leftHand;
    [SerializeField]
    GameObject rightHand;

    public float speed;
    public float distance;

    public override IEnumerator StartAttack()
    {
        float dir = 1;
        GameObject hand = leftHand;
        if (Random.Range(0, 2) == 0) // choose a hand
        {
            dir = -1;
            hand = rightHand;
        }

        Vector3 startPos = hand.transform.position;
        Vector3 targetPos = hand.transform.position + new Vector3(distance * dir, 0, 0);

        while(hand.transform.position != targetPos) //move hand forward
        {
            hand.transform.position = Vector3.MoveTowards(hand.transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        while (hand.transform.position != startPos) //return hand
        {
            hand.transform.position = Vector3.MoveTowards(hand.transform.position, startPos, 2.0f * speed * Time.deltaTime);
            yield return null;
        }

        hand.transform.position = startPos;
    }







    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
