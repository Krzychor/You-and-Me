using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeandVeritical : MonoBehaviour
{

    #region Editor 
    [Range(0.001f, 100f)] [SerializeField] float radius = 0.3f;
    private Vector2 end, start;
    [Range(-100f, 100f)] [SerializeField] float distance = 0f;
    Rigidbody2D rig;
    #endregion
    [Range(0.001f, 1f)] [SerializeField] float speed = 0.01f;
    float wait = 0f;
    bool isBack = false;

#if UNITY_EDITOR
   
    private void OnDrawGizmosSelected()
    {
        end.y = transform.position.y;
        end.x = transform.position.x + distance;
        start = transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(start, radius);
        Gizmos.DrawWireSphere(end, radius);
        Gizmos.DrawLine(start, end);

    }
#endif
    private void Start()
    {
        StartCoroutine(Move());
    }
    private void OnEnable()
    {
        start = transform.position;
        end.y = transform.position.y;
        end.x = transform.position.x + distance;
    }
    private void OnDisable()
    {
        StopCoroutine(Move());
    }
    IEnumerator Move()
    {
        while (true)
        {
            if (isBack)
            {

                transform.position = Vector2.MoveTowards(end, start, wait);
                wait += speed;
                if (transform.position.x == start.x)
                {
                    isBack = !isBack;
                    wait = 0f;
                    this.gameObject.SetActive(false);
                }

            }
            else
            {
                transform.position = Vector2.MoveTowards(start, end, wait);
                wait += speed;
                if (transform.position.x == end.x)
                {
                    isBack = !isBack;
                    wait = 0f;
                  //  this.gameObject.SetActive(false);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
  
}




