using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCheck : MonoBehaviour
{
    public int required = 2;
    public Collector collector;
    public DialogTrap trap;
    public DialogTrap finalDialog;

    public Collider2D invisibleWall; // :)



    private void OnEnable()
    {
        
    }

    void OnAwake()
    {

    }


    void Start()
    {
        finalDialog.enabled = false;
        trap.enabled = true;
    }

    void Update()
    {
        if (collector.counter >= required)
        {
            finalDialog.enabled = true;
            trap.enabled = false;
            invisibleWall.enabled = false;
            enabled = false;
        }
    }
}
