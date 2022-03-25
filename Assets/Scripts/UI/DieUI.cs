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
        Player.OnPlayerDead += Die;
    }

    private void OnDisable()
    {
        Player.OnPlayerDead -= Die;
    }



    void Die()
    {
        img.gameObject.SetActive(true);
        StartCoroutine(Fade());
    }

}
