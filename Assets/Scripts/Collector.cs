using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public int counter = 0;
    GameObject G;




    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Collectable")
        {
            counter++;
            other.GetComponent<SpriteRenderer>().enabled = false;

            AudioSource audioSource = other.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.enabled = true;
                audioSource.PlayOneShot(audioSource.clip);
                Destroy(other.gameObject, audioSource.clip.length);
            }
            else
                Destroy(other.gameObject);
        }
    }

}
