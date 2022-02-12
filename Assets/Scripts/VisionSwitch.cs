using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VisionSwitch : MonoBehaviour
{
    public PCF CameraController;

    [Range(1, 2)]
    public int startingLayer = 1;

    public GameObject Player1;
    public GameObject Player2;
    public AudioClip switchSound;

    public GameObject particleEmmiter1To2;
    public GameObject particleEmmiter2To1;
    public GameObject particles1To2;
    public GameObject particles2To1;
    public Vector3 scaleParticle1;
    public Vector3 scaleParticle2;
    public float SwitchCooldown = 2;

    bool canSwitch = true;
    int currentLayer = 0;
    private int startCharacter = 0;
    [SerializeField] GameObject post1, post2;

    GameObject[] vision1;
    GameObject[] vision2;

    void CollectLayers()
    {
        vision1 = GameObject.FindGameObjectsWithTag("vision1");
        vision2 = GameObject.FindGameObjectsWithTag("vision2");
    }
    
    void Start()
    {
        CollectLayers();

        currentLayer = startingLayer;
        if(currentLayer == 1)
        {
            ShowVision1();
            vinieta();
        }
        else
        {
            ShowVision2();
            vinieta();
        }
    }

    void ShowVision1()
    {
        Player2.SetActive(false);
        Player1.transform.position = Player2.transform.position;
        Player1.SetActive(true);
        CameraController.target = Player1.transform;
        currentLayer = 1;

        foreach (GameObject G in vision1)
            G.SetActive(true);

        foreach (GameObject G in vision2)
            G.SetActive(false);
    }

    void ShowVision2()
    {
        Player1.SetActive(false);
        Player2.transform.position = Player1.transform.position;
        Player2.SetActive(true);
        CameraController.target = Player2.transform;
        currentLayer = 2;

        foreach (GameObject G in vision1)
            G.SetActive(false);

        foreach (GameObject G in vision2)
            G.SetActive(true);
    }


    void SwitchVision()
    {
        if (currentLayer == 1)
        {
            ShowVision2();
            GameObject G = Instantiate(particles1To2, particleEmmiter1To2.transform);
            G.transform.localScale = scaleParticle1;
        }
        else
        {
            ShowVision1();
            GameObject G = Instantiate(particles2To1, particleEmmiter2To1.transform);
            G.transform.localScale = scaleParticle2;
        }
        SoundManager.PlaySound(switchSound);
    }

    void vinieta()
    {
        if (startCharacter == 0)
        {
            startCharacter = 1;
            post1.SetActive(false);
            post2.SetActive(true);
        }
        else
        {
            startCharacter = 0;
            post1.SetActive(true);
            post2.SetActive(false);
        }
    }

    IEnumerator StartSwitchCooldown()
    {
        canSwitch = false;
        yield return new WaitForSecondsRealtime(SwitchCooldown);
        canSwitch = true;
    }

    void Update()
    {
        if (canSwitch && 
            Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(StartSwitchCooldown());
            SwitchVision();
            vinieta();
        }
    }
}
