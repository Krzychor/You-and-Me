using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class DescriptionDisplayer : MonoBehaviour
{
    public GameObject descPrefab;
    public WorldDescription desc;
    public float LettersPerSecond = 15;

    int currentPart = -1;
    float lastReadTime = 0;
    bool displayCooldownPassed = true;
    float displayCooldown = 5;

    Text textField;
    GameObject canva;

    bool IsDisplaying()
    {
        if (currentPart == -1)
            return false;
        return true;
    }

    void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void FinishDisplay()
    {
        displayCooldownPassed = false;
        currentPart = -1;
        Destroy(canva);

        StartCoroutine(StartDisplayCooldown());
    }

    void StartDisplay()
    {
        if (desc == null)
            return;
        if (desc.parts.Count == 0)
            return;


        canva = Instantiate(descPrefab.gameObject, transform.position, Quaternion.identity);
        textField = canva.GetComponentInChildren<Text>();

        currentPart = -1;
        Advance();
    }

    void Advance()
    {
        currentPart ++;

        if(currentPart >= desc.parts.Count)
        {
            FinishDisplay();
            return;
        }

        float readTime = desc.parts[currentPart].readTime;
        if (readTime == 0)
            readTime = lastReadTime;
        lastReadTime = readTime;


        StopAllCoroutines();
        StartCoroutine(TypeSentence(desc.parts[currentPart].text));
        StartCoroutine(AutoSkip(readTime));
    }

    IEnumerator StartDisplayCooldown()
    {
        yield return new WaitForSecondsRealtime(displayCooldown);
        displayCooldownPassed = true;
    }

    IEnumerator TypeSentence(string sentence)
    {
        textField.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            textField.text += letter;
            yield return new WaitForSecondsRealtime(1.0f / LettersPerSecond);
        }
    }

    IEnumerator AutoSkip(float timeToWait)
    {
        yield return new WaitForSecondsRealtime(timeToWait);

        Advance();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
            return;
        if ((displayCooldownPassed && !IsDisplaying()) && collision.gameObject.tag == "Player")
            StartDisplay();

    }
}
