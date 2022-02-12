using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class DialogTrap : MonoBehaviour
{
    public Dialog dialog;
    public bool DisplayOnce = false;

    bool wasTriggered = false;


    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger || !enabled)
            return;
        if((!wasTriggered || !DisplayOnce) && collision.gameObject.tag == "Player")
        {
            wasTriggered = true;
            if (dialog != null)
            {
          //      Debug.Log("Fire for " + collision.name + "/" + collision.gameObject.name);
                DialogController.StartDialog(dialog, collision.gameObject);
            }
        }
           
    }
}
