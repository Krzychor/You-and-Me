using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuszekBoss : MonoBehaviour
{
    [SerializeField] List<GameObject> attacks;
    [SerializeField] float attackDelay = 1.0f;

    BossAttack currentAttack;


    void OnEnable()
    {
        StartCoroutine(StartFight());
    }

    IEnumerator StartFight()
    {
        if(attacks.Count == 0)
        {
            Debug.LogError("boss have no attacks!");
            yield return 0;
        }

        while(true)
        {
            GameObject choosenAttack = attacks[Random.Range(0, attacks.Count)];
            currentAttack = choosenAttack.GetComponent<BossAttack>();

            Debug.Log("choose " + choosenAttack.name);
       //     currentAttack.Init();
            yield return StartCoroutine(currentAttack.StartAttack());
            yield return new WaitForSeconds(attackDelay);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
