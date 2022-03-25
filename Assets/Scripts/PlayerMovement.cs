using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GroundData
{
    public GameObject gameObject;
    public Vector2 normal;
}


public class PlayerMovement : MonoBehaviour
{
    /*public CollisionDetector leftSide;
    public CollisionDetector rightSide;*/
    public float MovementSpeed = 10f;
    public float Jumpstrength = 4000f;
    public float groundMaxAngle = 0.3f;
    public GroundDetector groundDetector;
    public int MaxJumps = 1;
    Vector3 temp1;

    private int usedJumps = 0;
    private bool desireJump = false;
    private Rigidbody2D rg;
    private SpriteRenderer sprite;


    public AudioClip jumpSound;
    public AudioClip jumpLand;


    public float castRange = 0.1f;
    public Transform castLeft;
    public Transform castRight;
    public CollisionDetector left;
    public CollisionDetector right;
    public GameObject model;

    public Animator anim;
    PlayerRotator rotator;

    List<GroundData> groundContacts = new List<GroundData>();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GroundData data;
        data.gameObject = collision.gameObject;
        data.normal = collision.contacts[0].normal;

        if (data.normal.y > 1-groundMaxAngle)
        {
            usedJumps = 0;
            groundContacts.Add(data);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        for(int i = 0; i < groundContacts.Count; i++)
        {
            if (groundContacts[i].gameObject == collision.gameObject)
            {
                groundContacts.RemoveAt(i);
                i--;
            }
        }
    }

    Vector2 GetPerpendicular(Vector2 V)
    {
        if(V.x == 0 && V.y == 0)
            return new Vector2(1, 0);

        if (V.y == 0)
            return new Vector2(0, 1);

        float x = 1;
        float y = -x * V.x / V.y;
        return new Vector2(x, y);
    }

    bool OnGround()
    {
        if (groundContacts.Count > 0)
            return true;

        if (groundDetector)
            return groundDetector.IsOnGround();
        return false;
    }

    Vector2 GetGroundNormal()
    {
        Vector2 result  = new Vector2();

        foreach (GroundData data in groundContacts)
            result += data.normal;

        result /= (float)groundContacts.Count;

        return result;
    }

    Vector2 GetParallelToGround(float x)
    {
        if (x == 0)
            return new Vector2(1, 0);

        return GetPerpendicular(GetGroundNormal().normalized).normalized;
    }

    void Start()
    {
        rotator = gameObject.AddComponent<PlayerRotator>();
        anim = model.GetComponent<Animator>();
      //  anim = GetComponent<Animator>();
        temp1 = transform.localScale;
        rg = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            desireJump = true;

        float move = Input.GetAxis("Horizontal");

        UpdateAnimatorState(move);
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");


        if (move > 0 && CanGoRight())
        {
            rg.velocity = new Vector2(move * MovementSpeed, rg.velocity.y);
            model.transform.localScale = new Vector3(Mathf.Abs(model.transform.localScale.x),
                model.transform.localScale.y, model.transform.localScale.z);
        }
        else if (move < 0 && CanGoLeft())
        {
            rg.velocity = new Vector2(move * MovementSpeed, rg.velocity.y);
            model.transform.localScale = new Vector3(-Mathf.Abs(model.transform.localScale.x),
                model.transform.localScale.y, model.transform.localScale.z);
        }


        if (desireJump)
        {
            if (groundContacts.Count > 0 || usedJumps < MaxJumps)
            {
                usedJumps++;
                rg.AddForce(new Vector2(0, Jumpstrength));
            }
        }
        desireJump = false;
    }

    void UpdateAnimatorState(float move)
    {
        if (OnGround())
            anim.SetBool("IsGrounded", true);
        else
            anim.SetBool("IsGrounded", false);


        anim.SetBool("Run", false);

        if (move < 0 && CanGoLeft())
            anim.SetBool("Run", true);
        else if (move > 0 && CanGoRight())
            anim.SetBool("Run", true);
    }

    public void SetAnimationToIdle()
    {
        anim.SetBool("IsGrounded", true);
        anim.SetBool("Run", false);
    }

    private void OnDisable()
    {
        UpdateAnimatorState(0);
    }

    bool CanGoLeft()
    { 

        if (castLeft!= null)
        {
            RaycastHit2D hit = Physics2D.Raycast(castLeft.position, Vector2.left, castRange);
            if (hit.collider != null)
                return false;
        }

        if(left != null)
        {
            if (left.isColliding())
                return false;
        }

        return true;
        //  return !leftSide.isColliding();
    }

    bool CanGoRight()
    {
        if (castRight != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(castRight.position, Vector2.right, castRange);
            if (hit.collider != null)
                return false;
        }

        if (right != null)
        {
            if (right.isColliding())
                return false;
        }

        return true;
        // return !rightSide.isColliding();
    }
    

}
