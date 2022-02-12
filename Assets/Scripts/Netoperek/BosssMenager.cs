using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosssMenager : MonoBehaviour
{
    [SerializeReference] GameObject leftG;
    [SerializeReference] GameObject rightG;
    [SerializeReference] GameObject UpG;
    [SerializeField] float deleyAttack = 1f;
    GameObject lapaatakuje;
    enum Type { Left, Right, Up }
    Type[] typeList = new Type[3] { Type.Left, Type.Right, Type.Up };

  

    public void RandomAattack()
    {
        switch (typeList[Random.Range(0, 3)])
        {
            case Type.Left:
                lapaatakuje = leftG;
                break;
            case Type.Right:
                lapaatakuje = rightG;
                break;
            case Type.Up:
                lapaatakuje = UpG;
                break;
            default:
                lapaatakuje = leftG;
                break;
        }
    }

    IEnumerator WaitOnAttac()
    {
        while (true)
        {
            lapaatakuje.SetActive(true);
            yield return new WaitForSecondsRealtime(deleyAttack);
            RandomAattack();
        }
    }

    private void OnEnable()
    {
        RandomAattack();
        StartCoroutine(WaitOnAttac());
    }
    private void OnDisable()
    {
        StopCoroutine(WaitOnAttac());
    }
}
