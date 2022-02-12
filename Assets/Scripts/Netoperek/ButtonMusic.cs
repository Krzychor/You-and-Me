using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonMusic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button button;
    [SerializeReference] AudioSource audioScorse_click;
    [SerializeReference] AudioSource audioScorse_point;
    [SerializeReference] GameObject select;
    float startValumePoint;

    private void Awake()
    {
        button = GetComponent<Button>();
        startValumePoint = audioScorse_point.volume;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        select.SetActive(true);
        audioScorse_point.Play();

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        select.SetActive(false);
        StartCoroutine(VoliumDown());
    }

    private void OnEnable()
    {
        button.onClick.AddListener(audioScorse_click.Play);

    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(audioScorse_click.Play);

    }
    IEnumerator VoliumDown()
    {
        while (true)
        {
            if (audioScorse_point.volume == 0f)
            {

                audioScorse_point.volume -= 0.1f;
            }
            else
            {

                audioScorse_point.volume = startValumePoint;
                yield break;
            }

            yield return null;
        }
    }
}