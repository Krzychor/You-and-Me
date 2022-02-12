using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemi
{
    public class Enemi : MonoBehaviour
    {

        #region Editor 
        [Range(0.001f, 2f)] [SerializeField] float radius = 0.3f;
        private Vector2 end, start;
        [Range(0f, 30f)] [SerializeField] float distance = 0f;
        Rigidbody2D rig;
        #endregion
        [Range(0.001f, 0.2f)] [SerializeField] float speed = 0.01f;
        float wait = 0f;
        bool isBack = false;
        new Collider2D collider ;
        [SerializeField] float deleyOnCollaider = 2f;

#if UNITY_EDITOR
        private void Awake()
        {
            
            if (TryGetComponent<Collider2D>(out Collider2D collider))
            {
                this.collider = collider;
            }
            else
            {

                Debug.LogError("Brak komonentu !!!. Dodaj Collider2D do " + this.name);
            }
        }
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
                    }
                }
                yield return new WaitForFixedUpdate();
            }
        }
        IEnumerator WaitOnCollaider()
        {
            yield return new WaitForSecondsRealtime(deleyOnCollaider);
            collider.enabled = true;
            yield break;
        }

      

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Events.Event.AddDemage();
                collider.enabled = false;
                StartCoroutine(WaitOnCollaider());
            }
        }
    }

    
}