using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneHandler1 : MonoBehaviour
{
    public VideoPlayer video;
    public string sceneName;
    public string sceneToSave;

    public GameObject ui;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && sceneName != "")
        {
            video.loopPointReached += EndReached;
            ui.SetActive(true);
        }
    }

    void next()
    {
        StatisticsMenager.statisticsPlayer.LastMap = sceneToSave;
        StatisticsMenager manager = FindObjectOfType<StatisticsMenager>();
        if (manager)
            manager.SaveStatistic();

        SceneManager.LoadScene(sceneName);
    }

    void Start()
    {

    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        next();
    }
}
