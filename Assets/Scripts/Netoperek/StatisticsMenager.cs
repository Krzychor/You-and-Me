using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class StatisticsMenager : MonoBehaviour
{
    public static Stat.Statistics statisticsPlayer;
    public static Settigs settigs;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        statisticsPlayer = new Stat.Statistics();
        statisticsPlayer.LastMap = "";
        statisticsPlayer.autoSkipDialogs = true;
        statisticsPlayer.PlayerPos = new Vector2();
        statisticsPlayer.TransformCollectionItems = new List<Vector2_Serializable>();
        statisticsPlayer.HP = new bool[3] { true, true, true };
        statisticsPlayer.SecretObject = false;
        LoadStatistic();
        Events.Event.InvokeLoad();
        LoadSettings();
        Events.Event.InvokeLoadSttings();
        
    }
    /// <summary>
    /// ³adowanie statysk z pliku binarnego.
    /// </summary>
    public void LoadStatistic()
    {
        if (File.Exists(Application.persistentDataPath + "/data.jp"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/data.jp", FileMode.Open);

            statisticsPlayer = (Stat.Statistics)bf.Deserialize(stream);
            stream.Close();
            Debug.Log("Game is Load :* ");
        }
        else
        {
            Debug.Log("Game isn't load -_- " + "   :" + Application.persistentDataPath + "/data.jp");
        }
    }
    /// <summary>
    /// zapisanie statyst do pliku binarnego .
    /// </summary>
    public void SaveStatistic()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/data.jp", FileMode.Create);

        bf.Serialize(stream, statisticsPlayer);
        stream.Close();
        Debug.Log("Game is Save :* : ");
    }

    public void SaveSettings()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/settings.jp", FileMode.Create);

        bf.Serialize(stream, settigs);
        stream.Close();
        Debug.Log("Settings is Save :* : ");
    }

    public void LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/settings.jp"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/settings.jp", FileMode.Open);

            settigs = (Settigs)bf.Deserialize(stream);
            stream.Close();
            Debug.Log("Settings is Load :* " + Application.persistentDataPath + "/settings.jp");
            Events.Event.InvokeReadSettings();
        }
        else
        {
            Debug.Log("Settings isn't load -_- " + "   :" + Application.persistentDataPath + "/settings.jp");
        }
    }

    private void OnEnable()
    {
        Events.Event.load += LoadStatistic;
        Events.Event.save += SaveStatistic;
        Events.Event.saveSettings += SaveSettings;
        Events.Event.loadSettings += LoadSettings;
    }
    private void OnDisable()
    {
        Events.Event.load -= LoadStatistic;
        Events.Event.save -= SaveStatistic;
        Events.Event.saveSettings -= SaveSettings;
        Events.Event.loadSettings -= LoadSettings;
    }


}

[System.Serializable]
public struct Settigs
{
    public int indexResolutionSave { get; set; }
    public bool isSynC { get; set; }
    public float soundVolume { get; set; }
}
