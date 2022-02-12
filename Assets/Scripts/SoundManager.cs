using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager singleton;
    public AudioMixerGroup mixer;


    List<AudioSource> sources = new List<AudioSource>();
    List<AudioSource> sources2 = new List<AudioSource>();
    GameObject listener;


    void Update()
    {
        for(int i = 0; i < sources.Count; i++)
        {
            if(!sources[i].isPlaying)
            {
                Destroy(sources[i].gameObject);
                sources.RemoveAt(i);
                i--;
            }
        }

        for (int i = 0; i < sources2.Count; i++)
        {
            if (!sources2[i].isPlaying)
            {
                Destroy(sources2[i].gameObject);
                sources2.RemoveAt(i);
                i--;
            }
        }
    }

    void Awake()
    {
        singleton = this;
        listener = FindObjectOfType<AudioListener>().gameObject;
    }

    public void playSound(AudioClip clip, Vector3 position)
    {
        if (clip == null)
            return;

        GameObject G = new GameObject();
        AudioSource source = G.AddComponent<AudioSource>();
        sources.Add(source);

        source.PlayOneShot(clip);
    }

    public void playSound(AudioClip clip)
    {
        if (clip == null)
            return;

        GameObject G = new GameObject();
        AudioSource source = G.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = mixer;
        sources2.Add(source);

        source.PlayOneShot(clip);
    }

    public static void PlaySound(AudioClip clip, Vector3 position)
    {
        if (singleton == null)
            return;
        singleton.playSound(clip, position);
    }
    public static void PlaySound(AudioClip clip)
    {
        if (singleton == null)
            return;

        singleton.playSound(clip);
    }


}
