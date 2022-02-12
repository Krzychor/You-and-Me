using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject FistPrefab;
    public float SpawnFrequency = 2;
    public float FistSpawnDistance = 10;
    public LayerMask IgnoreMe;

    Transform player;

    void Start()
    {
        GameObject G = GameObject.FindGameObjectWithTag("Player");
        if (G != null)
            player = G.transform;
        else
        {
            Debug.LogError("boss can't find player!");
            Destroy(this.gameObject);
        }
        StartCoroutine(StartFight());
    }


    IEnumerator StartFight()
    {
        while(true)
        {
            yield return new WaitForSeconds(SpawnFrequency);
            TrySpawnFist();
        }

    }


    bool TrySpawnAbove(out Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.up, FistSpawnDistance, IgnoreMe);
        if(hit.collider == null)
        {
            position = new Vector2(player.transform.position.x, player.transform.position.y) + Vector2.up*FistSpawnDistance;
            return true;
        }

        position = new Vector2();


        return false;
    }

    bool GetFistPosition(out Vector2 position, out Vector2 direction)
    {
        if (TrySpawnAbove(out position))
        {
            direction = Vector2.down;
            return true;
        }

        direction = Vector2.zero;
        return false;
    }

    void SpawnFist(Vector2 position, Vector2 dir)
    {
        GameObject G = Instantiate(FistPrefab, position, Quaternion.identity);
        Fist fist = G.GetComponent<Fist>();
        fist.dir = dir;
    }

    void TrySpawnFist()
    {
        if(GetFistPosition(out Vector2 position, out Vector2 dir))
            SpawnFist(position, dir);



    }
}
