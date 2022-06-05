using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSlash : BossAttack
{
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    [SerializeField] Transform Player;

    [SerializeField] float handMoveSpeed = 40;
    [SerializeField] float hoverTime = 2.0f;
    [SerializeField] float distanceFromPlayer = 2;
    [SerializeField] float outsideDist = 5;




    public override IEnumerator StartAttack()
    {
        leftHand.SetActive(true);
        rightHand.SetActive(true);
        leftHand.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.5f, 10.0f)) 
            + new Vector3(-outsideDist, 0, 0);
        rightHand.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, 10.0f))
            + new Vector3(outsideDist, 0, 0);


        Vector3 targetHoverLeft = Player.transform.position + new Vector3(-distanceFromPlayer, distanceFromPlayer/2, 0);
        Vector3 targetHoverRight = Player.transform.position + new Vector3(distanceFromPlayer, distanceFromPlayer/2, 0);


        while (leftHand.transform.position != targetHoverLeft && rightHand.transform.position != targetHoverRight)
        {
            leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, targetHoverLeft, handMoveSpeed * Time.deltaTime);
            rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, targetHoverRight, handMoveSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(hoverTime);


        leftHand.SetActive(false);
        rightHand.SetActive(false);
        yield return null;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
