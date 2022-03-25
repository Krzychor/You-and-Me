using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Events
{
    public class Event : MonoBehaviour
    {
        public delegate void InteriactionWithPlayer();
   //     public static event InteriactionWithPlayer enemyInteraction;
    //    public static event InteriactionWithPlayer damage;
        
        public delegate void GameMenager();
        public static event GameMenager save;
        public static event GameMenager load;
        public static event GameMenager saveSettings;
        public static event GameMenager loadSettings;
        public static event GameMenager readSettings;


        public delegate void Gameover();
  //      public static event Gameover playerDead;

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public static void InvokeLoad()
        {
            load?.Invoke();
        }

        public static void InvokeSaveSettings()
        {
            saveSettings?.Invoke();
        }
        public static void InvokeLoadSttings()
        {
            loadSettings?.Invoke();
        }
        public static void InvokeReadSettings()
        {
            readSettings?.Invoke();
        }

    }
}
