using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;
public class MainMenu : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Slider sildLoad;
    [SerializeField] Slider sildValume;
    [SerializeField] GameObject buttoms;
    [SerializeField] GameObject title;
    [SerializeField] GameObject beckGraund_Credits;
    [SerializeField] GameObject beckGround_Main;
    List<AsyncOperation> asyncOperation = new List<AsyncOperation>();
    [SerializeField] Image[] imageButtoms;
    [SerializeField] Toggle toggleVsync;
    [SerializeField] GameObject option;
    [SerializeField] Text t_resolution;
    float volumeMusic = 0f;
    Color color;
    Image imageBackgrondMain;
    Image titleImage;
    string NameScene = "cutscene 0";
    [SerializeField] AudioMixer mixer;
    string[] resolutions = new string[8] { "1920x1080", "1680x1050", "1600x900", "1366x768", "1280x1024", "1280x720", "1024x768", "800x600" };
    int indexResolution;
   
    #region Metody
    public void OpenCreadits()
    {
        beckGraund_Credits.SetActive(true);
        imageBackgrondMain = beckGround_Main.GetComponent<Image>();
        color = imageBackgrondMain.color;
        titleImage = title.GetComponent<Image>();
        StartCoroutine(FadeBeckGroundMenu());
        // buttoms.SetActive(false);
        // beckGround_Main.SetActive(false);

    }
    public void BackCredits()
    {
        StartCoroutine(ShowBeckGroundMenu());
    }

    public void OpenSettings()
    {

        buttoms.SetActive(false);
        title.SetActive(false);
        option.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeVsync()
    {
        ///Debug.Log(toggleVsync.isOn.ToString() + QualitySettings.vSyncCount.ToString());
        if (QualitySettings.vSyncCount == 0)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }
    public void ChangeResolution()
    {
        switch (resolutions[indexResolution])
        {
            case "1920x1080":
                Screen.SetResolution(1920, 1080, true);
                break;
            case "1680x1050":
                Screen.SetResolution(1680, 1050, true);
                break;
            case "1600x900":
                Screen.SetResolution(1600, 900, true);
                break;
            case "1366x768":
                Screen.SetResolution(1366, 768, true);
                break;
            case "1280x1024":
                Screen.SetResolution(1280, 1024, true);
                break;
            case "1280x720":
                Screen.SetResolution(1280, 720, true);
                break;
            case "1024x768":
                Screen.SetResolution(1024, 768, true);
                break;
            case "800x600":
                Screen.SetResolution(800, 600, true);
                break;
            default:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }
    public void UpResolution()
    {
        if (0 < indexResolution)
        {
            indexResolution--;
            t_resolution.text = resolutions[indexResolution];
            ChangeResolution();
        }
    }
    public void DownResolution()
    {
        if (resolutions.Length - 1 > indexResolution)
        {
            indexResolution++;
            t_resolution.text = resolutions[indexResolution];
            ChangeResolution();
        }
    }
    public void ChangeVolume()
    {
        //  Debug.Log(sildValume.value);
        mixer.SetFloat("MainVolume", sildValume.value);
    }

    public void BackToMainMenuFormSettings()
    {
        Events.Event.InvokeSaveSettings();
        SaveSettings();
        option.SetActive(false);
        title.SetActive(true);
        buttoms.SetActive(true);
        // Invoke Save Settings;
    }


    #endregion

    public void StartNewGame()
    {
        StartCoroutine(LoadScene());
    }
    public void Continue()
    {
        // Load stat dopisaæ 
        StartCoroutine(LoadScene());
    }
    public void SaveSettings()
    {
        StatisticsMenager.settigs.indexResolutionSave = indexResolution;
        StatisticsMenager.settigs.isSynC = toggleVsync.isOn;
        StatisticsMenager.settigs.soundVolume = sildValume.value;
    }
    public void LoadSettings()
    {
        indexResolution = StatisticsMenager.settigs.indexResolutionSave;
        if (StatisticsMenager.settigs.isSynC)
        {
            QualitySettings.vSyncCount = 0;
            toggleVsync.isOn = false;
        }
        else
        {
            QualitySettings.vSyncCount = 1;
            toggleVsync.isOn = true;
        }
        sildValume.value = StatisticsMenager.settigs.soundVolume;
        mixer.SetFloat("MainVolume", StatisticsMenager.settigs.soundVolume);
        t_resolution.text = resolutions[indexResolution];
        switch (resolutions[indexResolution])
        {
            case "1920x1080":
                Screen.SetResolution(1920, 1080, true);
                break;
            case "1680x1050":
                Screen.SetResolution(1680, 1050, true);
                break;
            case "1600x900":
                Screen.SetResolution(1600, 900, true);
                break;
            case "1366x768":
                Screen.SetResolution(1366, 768, true);
                break;
            case "1280x1024":
                Screen.SetResolution(1280, 1024, true);
                break;
            case "1280x720":
                Screen.SetResolution(1280, 720, true);
                break;
            case "1024x768":
                Screen.SetResolution(1024, 768, true);
                break;
            case "800x600":
                Screen.SetResolution(800, 600, true);
                break;
            default:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }


    IEnumerator LoadScene()
    {



        //Begin to load the Scene you specify
        // List<AsyncOperation> asyncOperation = SceneManager.LoadSceneAsync(NameScene);
        asyncOperation.Add(SceneManager.LoadSceneAsync(NameScene));
        //Don't let the Scene activate until you allow it to
        asyncOperation[0].allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation[0].progress.ToString());
        buttoms.SetActive(false);
        title.SetActive(false);
        sildLoad.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation[0].isDone)
        {
            //Output the current progress
            //text.text = "Loading progress: " + ((asyncOperation[0].progress + asyncOperation[1].progress) * 100f)/1.8f + "%";
            sildLoad.value = ((asyncOperation[0].progress * 100f) / 0.9f);
            // Check if the load has finished
            if (asyncOperation[0].progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                text.gameObject.SetActive(true);
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    asyncOperation[0].allowSceneActivation = true;
            }

            yield return null;
        }
    }

    IEnumerator FadeBeckGroundMenu()
    {
        float speed = 0f;
        while (true)
        {
            speed += 0.05f;
            if (speed <= 1f)
            {
                color.a = Mathf.Lerp(1f, 0, speed);
                for (int i = 0; i < imageButtoms.Length; i++)
                {
                    imageButtoms[i].color = color;
                }

                titleImage.color = color;
                imageBackgrondMain.color = color; // Mathf.Lerp(1f, 0,0.1f);

            }
            else
            {
                // color.a = 1f;
                // imageBackgrondMain.color = color;
                //titleImage.color = color;
                for (int i = 0; i < imageButtoms.Length; i++)
                {
                    imageButtoms[i].color = color;
                }

                beckGround_Main.SetActive(false);
                title.SetActive(false);
                buttoms.SetActive(false);
                Debug.Log("Fade end");
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }

    }
    IEnumerator ShowBeckGroundMenu()
    {
        float speed = 0f;
        buttoms.SetActive(true);
        while (true)
        {
            speed += 0.05f;
            title.SetActive(true);
            beckGround_Main.SetActive(true);
            if (speed <= 1f)
            {
                color.a = Mathf.Lerp(0f, 1f, speed);
                for (int i = 0; i < imageButtoms.Length; i++)
                {
                    imageButtoms[i].color = color;
                }
                titleImage.color = color;
                imageBackgrondMain.color = color; // Mathf.Lerp(1f, 0,0.1f);

            }
            else
            {
                color.a = 1f;
                for (int i = 0; i < imageButtoms.Length; i++)
                {
                    imageButtoms[i].color = color;
                }
                imageBackgrondMain.color = color;
                titleImage.color = color;
                beckGraund_Credits.SetActive(false);


                Debug.Log("Fade end");
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }

    }

    private void OnEnable()
    {
        Events.Event.saveSettings += SaveSettings;
        Events.Event.loadSettings += LoadSettings;
        Events.Event.readSettings += LoadSettings;

    }
    private void OnDisable()
    {
        Events.Event.saveSettings -= SaveSettings;
        Events.Event.readSettings -= LoadSettings;
        Events.Event.loadSettings -= LoadSettings;
    }
}
