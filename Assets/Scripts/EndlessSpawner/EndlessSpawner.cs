using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSpawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    int randomSpawnPoint;

    public GameObject[] enemy;
    public GameObject enemyBoss;
    public GameObject enemyBoss2;

    int numEnemies = 10;

    float time;


    //Lave 10 spawnPoints

    //Array til at randomize mellem 10 spawnPoints - starter med at der er 2 spawnpoints, efter 15 sekunder spawner der 3 og så 4, 5 osv...

    //Timer, som spawner enemies hvert X sekund, multiplier over tid, måske med + .1 sek
    //Spawner bossSlug efter længere tid, men samme koncept som ovenstående

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawner());
        StartCoroutine(EnemyBossSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 0.001f;
    }


    IEnumerator EnemyBossSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(10 - time);
            randomSpawnPoint = Random.Range(0, 2);
            Instantiate(enemyBoss, spawnPoints[randomSpawnPoint].position, transform.rotation);

        }
    }

    IEnumerator EnemySpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(5 - time);
            randomSpawnPoint = Random.Range(0, 2);
            Instantiate(enemy[Random.Range(0,1)], spawnPoints[randomSpawnPoint].position, transform.rotation);
        }
    }
}
