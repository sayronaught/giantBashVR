using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        EnemySpawner();
        EnemyBossSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    private int waitTimer(int min, int startTimer, float reduxModifier)
    {
        return Mathf.Max(min,startTimer-(int)(time*reduxModifier));
    }

    private async void EnemyBossSpawner()
    {
        while (!Application.isEditor || Application.isPlaying)
        {
            randomSpawnPoint = Random.Range(0, 2);
            var spawn = Instantiate(enemyBoss, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            spawn.GetComponent<EnemyJotunBoss>().health = time * 5f;
            Debug.Log("boss waiting: " + waitTimer(2000, 30000, 0.001f).ToString());
            await Task.Delay(waitTimer(2000, 10000, 0.0001f));
        }
    }

    private async void EnemySpawner()
    {
        while (!Application.isEditor || Application.isPlaying)
        {
            randomSpawnPoint = Random.Range(0, 2);
            var spawn = Instantiate(enemy[Random.Range(0,1)], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            spawn.GetComponent<EnemyJotunBoss>().health = time * 0.5f;
            Debug.Log("enemy waiting: " + waitTimer(1000, 5000, 0.01f).ToString());
            await Task.Delay(waitTimer(1000, 5000, 0.001f));
        }
    }
}
