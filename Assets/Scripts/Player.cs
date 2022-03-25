using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public int hp = 3;
    public int maxHp = 3;
    [SerializeField]
    float afterDmgImmunityDuration = 1;


    bool isImmune = false;
    float immuneRemainingTime = 0;
    

    public static event Action OnDamagePlayer;
    public static event Action OnPlayerDead;


    public void Die()
    {
        hp = 0;

        OnPlayerDead?.Invoke();
    }

    public void ReceiveDmg()
    {
        if (isImmune)
            return;

        hp--;
        MakeImmune();

        OnDamagePlayer?.Invoke();
        if (hp <= 0)
            OnPlayerDead?.Invoke();
    }

    void MakeImmune()
    {
        immuneRemainingTime = afterDmgImmunityDuration;
        isImmune = true;
    }

    void UpdateImmunity()
    {
        if (!isImmune)
            return;

        immuneRemainingTime -= Time.deltaTime;
        if (immuneRemainingTime <= 0)
        {
            isImmune = false;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        UpdateImmunity();
    }
}
