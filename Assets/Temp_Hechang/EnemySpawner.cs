using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnpoints;

    public Transform[] golem;
    public Transform[] Slime;
    public Transform boss;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if(Random.Range(0, 10) < 8)
            {
                Instantiate(boss, spawnpoints[Random.Range(0, spawnpoints.Length)].position, Quaternion.identity);
            }
            else
            {
                if (Random.Range(0, 10) < 5)
                {
                    Instantiate(Slime[Random.Range(0, golem.Length)], spawnpoints[Random.Range(0, spawnpoints.Length)].position, Quaternion.identity);
                }
                else
                {

                    Instantiate(golem[Random.Range(0, golem.Length)], spawnpoints[Random.Range(0, spawnpoints.Length)].position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(Random.Range(4, 10));
        }
    }
}
