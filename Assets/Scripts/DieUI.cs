using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieUI : MonoBehaviour
{
    static DieUI singleton;

    public GameObject dieCanva;
    public float FadeTime;
    Image img;



    IEnumerator Fade()
    {
        img.enabled = true;
        float time = 0;

        while(time < FadeTime)
        {
            time += Time.deltaTime;
            yield return null;

            Color color = img.color;
            color.a = time / FadeTime;
            img.color = color;
        }


        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        yield return null;
    }

    void Start()
    {
        singleton = this;
        img = dieCanva.GetComponent<Image>();
    }

    private void OnEnable()
    {
        Events.Event.playerDead += die;
    }

    private void OnDisable()
    {
        Events.Event.playerDead -= die;
    }

    void die()
    {
        img.gameObject.SetActive(true);
        StartCoroutine(Fade());
    }



    public static void Die()
    {
        singleton.die();
    }

}
