using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    static DialogController singleton;
    public GameObject DialogCanva;
    public Text TextField;
    public Text CharacterNameField;
    public Image CharacterImage;
    [Range(1, 1000)]
    public float LettersPerSecond = 15;

    PlayerMovement playerMovement;
    VisionSwitch visionSwitch;
    Dialog dialog;
    int currentPart = 0;
    float lastReadTime = 0;

    void Awake()
    {
        if (singleton != null)
            Debug.LogWarning("Multiple dialog controllers on scene!");
        singleton = this;
        visionSwitch = FindObjectOfType<VisionSwitch>();
        DialogCanva.SetActive(false);
    }

    void Update()
    {
        if (dialog == null)
            return;

        if (Input.GetButtonDown("Fire1"))
            AdvanceDialog();

        if (Input.GetMouseButtonDown(1))
            FinishDialog();
    }

    void AdvanceDialog()
    {
        currentPart++;

        if (currentPart >= dialog.parts.Count)
        {
            FinishDialog();
            return;
        }
    //    if(dialog.parts[currentPart].characterImage && currentPart-1 > -1) //if no data use last value
    //        CharacterImage.sprite = dialog.parts[currentPart-1].characterImage;
    //    else
            CharacterImage.sprite = dialog.parts[currentPart].characterImage;

        if (dialog.parts[currentPart].characterName != "") //if no data use last value
            CharacterNameField.text = dialog.parts[currentPart].characterName;



        float readTime = dialog.parts[currentPart].readTime;
        if(readTime == 0) //use last value if current is not set
            readTime = lastReadTime;

        lastReadTime = readTime;

        StopAllCoroutines();
        if (StatisticsMenager.statisticsPlayer.autoSkipDialogs)
            StartCoroutine(AutoSkip(readTime));
        StartCoroutine(TypeSentence(dialog.parts[currentPart].dialogue));
    }

    public static void StartDialog(Dialog dialog_, GameObject player)
    {
        if (singleton == null)
        {
            Debug.LogWarning("no dialog controller on scene!");
            return;
        }

        if (dialog_.parts.Count == 0)
            return;
        singleton.dialog = dialog_;

        singleton.BlockPlayer(player);
        singleton.DialogCanva.SetActive(true);


        singleton.lastReadTime = 0;
        singleton.currentPart = -1;

        singleton.AdvanceDialog();
     //   singleton.StopAllCoroutines();
    //    singleton.StartCoroutine(singleton.TypeSentence(dialog_.parts[0].dialogue));
     //   Time.timeScale = 0;
        
    }

    void BlockPlayer(GameObject player)
    {
        Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();
        if(rigid)
            rigid.velocity = new Vector2(0, 0);

        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.enabled = false;
        playerMovement.SetAnimationToIdle();
        if (visionSwitch)
            visionSwitch.enabled = false;
    }

    void UnblockPlayer()
    {
        if(playerMovement)
            playerMovement.enabled = true;
    }

    IEnumerator TypeSentence (string sentence)
    {
        TextField.resizeTextForBestFit = true;
        TextField.text = "";
        TextField.resizeTextForBestFit = false;

        foreach(char letter in sentence.ToCharArray())
        {
            TextField.text += letter;
            yield return new WaitForSecondsRealtime(1.0f / LettersPerSecond);
        }
    }

    IEnumerator AutoSkip (float timeToWait)
    {
        yield return new WaitForSecondsRealtime(timeToWait);

        AdvanceDialog();
    }

    void FinishDialog()
    {
        singleton.StopAllCoroutines();

        if (visionSwitch)
            visionSwitch.enabled = true;
        UnblockPlayer();
        DialogCanva.SetActive(false);
    }
}
