using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;

public class sceneNext : MonoBehaviour
{
    public string nextScene;
    VideoPlayer video;

    void Start()
    {
        video = GetComponent<VideoPlayer>();
        video.loopPointReached += EndReached;
    }


    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(nextScene);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            SceneManager.LoadScene(nextScene);

    }
}
