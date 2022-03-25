using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI_HP
{
    public class UI_HP : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] GameObject normalHeart;
        [SerializeField] GameObject shatteredHeart;
        [SerializeReference] Sprite full;
        [SerializeReference] Sprite empty;
        [SerializeReference] Image[] images;

        private void RefreshHP()
        {
            return;
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].sprite == full)
                {
                    images[i].sprite = empty;
                    if (i == images.Length && images[i].sprite == empty )
                    {
                        // Player Dead event call
                        Debug.Log("Player Dead !!! [*]");
                    }
                 //   StatisticsMenager.statisticsPlayer.HP = player.hp;
                    break;
                }
            }

        //    if(IsDead())
         //       Events.Event.InvokePlayerDead();

        }


        private void DestroyHeart()
        {
            int index = player.hp;
            if (index == -1 || index >= images.Length)
                return;

            images[index].sprite = empty;
            images[index].gameObject.GetComponent<AudioSource>()?.Play();
            Instantiate(shatteredHeart, images[index].transform.position, Quaternion.identity,
                images[index].gameObject.GetComponentInParent<Canvas>().transform);
        }

        private void LoadStatHP()
        {
       //     hp = StatisticsMenager.statisticsPlayer.HP;
            /*
            for (int i = 0; i < hp.Length; i++)
            {
                if (hp[i] == false)
                {
                    images[i].sprite = empty;
                    Debug.Log("Load Stat HP XD ");
                }
            }
             **/
        }


        private void Display(Player player)
        {
            this.player = player;
            List<Image> newList = new List<Image>(images);
            while(newList.Count < player.maxHp)
            {
                GameObject G = Instantiate(normalHeart, transform);
                Image image = G.GetComponent<Image>();
                newList.Add(image);
            }
            
            while(newList.Count > player.maxHp)
            {
                if (newList.Count == 0)
                    break;
                Destroy(newList[newList.Count - 1].gameObject);
                newList.RemoveAt(newList.Count - 1);
            }

            images = newList.ToArray();

            for (int i = 0; i < player.hp; i++)
                images[i].sprite = full;

            for (int i = player.hp; i < player.maxHp; i++)
                images[i].sprite = empty;
        }

        private void OnEnable()
        {
            Player.OnDamagePlayer += DestroyHeart;
            PlayerSwitch.OnPlayerSwitch += Display;
     //       Events.Event.load += LoadStatHP;
        }
        private void OnDisable()
        {
            //   Events.Event.load -= LoadStatHP;
            Player.OnDamagePlayer -= DestroyHeart;
            PlayerSwitch.OnPlayerSwitch -= Display;
        }
    }
}