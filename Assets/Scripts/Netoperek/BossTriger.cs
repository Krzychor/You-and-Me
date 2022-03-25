using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriger : MonoBehaviour
{
    [SerializeReference] Collider2D collider;
    [SerializeField] float deleyOnCollaider;
    IEnumerator WaitOnCollaider()
    {
        yield return new WaitForSecondsRealtime(deleyOnCollaider);
        collider.enabled = true;
        yield break;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Dosta�");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Dosta�");
            Player player = collision.gameObject.GetComponent<Player>();
            player?.ReceiveDmg();
            collider.enabled = false;
            StartCoroutine(WaitOnCollaider());
        }
    }
}
