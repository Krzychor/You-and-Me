using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    public GameObject fistParticles;
    public float speed = 20;
    public Vector2 dir;
    public AudioClip HitSound;

    Rigidbody2D rigid;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player?.ReceiveDmg();
        }
        else
        {
            Vector2 hitPoint = collision.GetContact(0).point;
            Instantiate(fistParticles, hitPoint, Quaternion.identity);
            SoundManager.PlaySound(HitSound, hitPoint);
        }
        Destroy(this.gameObject);
    }


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigid.velocity = dir * speed;
    }
}
