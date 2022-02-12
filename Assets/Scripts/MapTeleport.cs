using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(BoxCollider2D))]
public class MapTeleport : MonoBehaviour
{
    public string nextScene;
    public GameObject loadingScreen;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && nextScene != "")
            LoadNextScene();
    }

    void LoadNextScene()
    {
        StatisticsMenager.statisticsPlayer.LastMap = nextScene;
        StatisticsMenager manager = FindObjectOfType<StatisticsMenager>();
        if (manager)
            manager.SaveStatistic();


        StartCoroutine(LoadScene());
    }

    void Start()
    {

    }

    IEnumerator LoadScene()
    {
        List<AsyncOperation> asyncOperation = new List<AsyncOperation>();
        asyncOperation.Add(SceneManager.LoadSceneAsync(nextScene));

        if(loadingScreen)
            Instantiate(loadingScreen);


        //Don't let the Scene activate until you allow it to
        asyncOperation[0].allowSceneActivation = false;

      //  yield return new WaitForSecondsRealtime(4);


        asyncOperation[0].allowSceneActivation = true;

        yield return null;
    }

}
