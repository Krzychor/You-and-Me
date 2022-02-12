using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkBoss : MonoBehaviour
{
    public GameObject brama;
    public GameObject Player;

    [SerializeField] GameObject receboga;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.GetComponent<Collector>().counter >= 1)
        {
            brama.SetActive(true);
            receboga.SetActive(false);
        }
    }
}
