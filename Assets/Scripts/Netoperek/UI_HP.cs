using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI_HP
{
    public class UI_HP : MonoBehaviour
    {
        [SerializeReference] Sprite full;
        [SerializeReference] Sprite empty;
        [SerializeReference] Image[] images;
        bool[] hp = new bool[3] { true, true, true };

        private void ChangeSprite()
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].sprite == full)
                {
                    hp[i] = false;
                    images[i].sprite = empty;
                    if (i == images.Length && images[i].sprite == empty )
                    {
                        //it's not called, moved below

                   //     Events.Event.InvokePlayerDead();
                        // Player Dead event call
                        Debug.Log("Player Dead !!! [*]");
                    }
                    StatisticsMenager.statisticsPlayer.HP = hp;
                    break;
                }
            }

            if(IsDead())
                Events.Event.InvokePlayerDead();

        }

        bool IsDead()
        {
            for (int i = 0; i < hp.Length; i++)
                if (hp[i])
                    return false;
            return true;
        }

        private void LoadStatHP()
        {
            hp = StatisticsMenager.statisticsPlayer.HP;
            for (int i = 0; i < hp.Length; i++)
            {
                if (hp[i] == false)
                {
                    images[i].sprite = empty;
                    Debug.Log("Load Stat HP XD ");
                }
            }
        }

        private void OnEnable()
        {
            Events.Event.enemiInteraction += ChangeSprite;
            Events.Event.load += LoadStatHP;
        }
        private void OnDisable()
        {
            Events.Event.enemiInteraction -= ChangeSprite;
            Events.Event.load -= LoadStatHP;
        }
    }
}