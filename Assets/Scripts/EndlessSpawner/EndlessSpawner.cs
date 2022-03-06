using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EndlessSpawner : MonoBehaviour
{

    public EndlessPlayerScript PlayerScript;

    public Transform[] beginSpawnPoints;
    public Transform[] spawnPoints;
    int randomSpawnPoint;
    int randomSpawnMob;

    public GameObject[] enemyNormalPrefab;
    public GameObject[] enemyBossPrefab;
    public GameObject[] enemyShamanPrefab;

    int numEnemies = 10;

    float time;
    float toughnessModifier = 1f;


    //Lave 10 spawnPoints

    //Array til at randomize mellem 10 spawnPoints - starter med at der er 2 spawnpoints, efter 15 sekunder spawner der 3 og s� 4, 5 osv...

    //Timer, som spawner enemies hvert X sekund, multiplier over tid, m�ske med + .1 sek
    //Spawner bossSlug efter l�ngere tid, men samme koncept som ovenst�ende

    // Start is called before the first frame update
    void Start()
    {
        spawnBeginningEnemies();
        EnemySpawner();
        EnemyShamanSpawner();
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

    private void spawnSingleEnemy( GameObject prefab , Transform spawnpoint )
    {
        var spawn = Instantiate(prefab, spawnpoint.position, Quaternion.identity);
        var ai = spawn.GetComponent<dynamicEnemy>();
        ai.spawnSetDifficulty(toughnessModifier);
        ai.setWaypoints(spawnpoint);
        ai.playerScript = PlayerScript;
    }

    private void spawnBeginningEnemies()
    {
        foreach(Transform spawn in beginSpawnPoints)
        {
            spawnSingleEnemy(enemyNormalPrefab[0], spawn);
        }
    }

    private async void EnemyBossSpawner()
    {
        while (!Application.isEditor || Application.isPlaying)
        {
            //Debug.Log("enemy waiting: " + waitTimer(1000, 5000, 0.01f).ToString());
            await Task.Delay(waitTimer(5000, 750000, 0.003f));
            toughnessModifier += 0.05f;
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            randomSpawnMob = Random.Range(0, enemyBossPrefab.Length);
            spawnSingleEnemy(enemyBossPrefab[randomSpawnMob], spawnPoints[randomSpawnPoint]);
        }
    }

    private async void EnemyShamanSpawner()
    {
        while (!Application.isEditor || Application.isPlaying)
        {
            await Task.Delay(waitTimer(2000, 50000, 0.002f));
            toughnessModifier += 0.025f;
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            randomSpawnMob = Random.Range(0, enemyShamanPrefab.Length);
            spawnSingleEnemy(enemyShamanPrefab[randomSpawnMob], spawnPoints[randomSpawnPoint]);
        }
    }

    private async void EnemySpawner()
    {
        while (!Application.isEditor || Application.isPlaying)
        {
            await Task.Delay(waitTimer(1000, 10000, 0.001f));
            toughnessModifier += 0.01f;
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            randomSpawnMob = Random.Range(0, enemyNormalPrefab.Length);
            spawnSingleEnemy(enemyNormalPrefab[randomSpawnMob], spawnPoints[randomSpawnPoint]);
        }
    }
}
