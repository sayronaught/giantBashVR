using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class campaignSpawner : MonoBehaviour
{

    public EndlessPlayerScript PlayerScript;
    public Transform playerTransform;
    public Transform directToPlayer;
    public cart cart;
    public int startSpawningWaypoint = 0;
    bool started = false;

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

    public EffectBank myEB;

    private List<Transform> availableSpawns = new List<Transform>();
    //Lave 10 spawnPoints

    //Array til at randomize mellem 10 spawnPoints - starter med at der er 2 spawnpoints, efter 15 sekunder spawner der 3 og s� 4, 5 osv...

    //Timer, som spawner enemies hvert X sekund, multiplier over tid, m�ske med + .1 sek
    //Spawner bossSlug efter l�ngere tid, men samme koncept som ovenst�ende

    // Start is called before the first frame update
    private void Start()
    {
        myEB = GetComponent<EffectBank>();
    }
    void StartEnemySpawn()
    {
        spawnBeginningEnemies();
        EnemySpawner();
        EnemyShamanSpawner();
        EnemyBossSpawner();
        spawnChanger();
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cart.waypointInt >= startSpawningWaypoint && !started) StartEnemySpawn();
        time += Time.deltaTime;


    }
    private async void spawnChanger()
    {
        while (!Application.isEditor || Application.isPlaying)
        {
            await Task.Delay(5000);
            availableSpawns.Clear();
            foreach (Transform sp in spawnPoints)
            {
                if (Vector3.Distance(sp.transform.position, cart.transform.position) >= 20)
                    availableSpawns.Add(sp);
            }
        }
    }

    private int waitTimer(int min, int startTimer, float reduxModifier)
    {
        return Mathf.Max(min, startTimer - (int)(time * reduxModifier));
    }

    private void spawnSingleEnemy(GameObject prefab, Transform spawnpoint)
    {
        var spawn = Instantiate(prefab, spawnpoint.position, Quaternion.identity);
        var ai = spawn.GetComponent<dynamicEnemy>();
        ai.spawnSetDifficulty(toughnessModifier);
        ai.setWaypoints(spawnpoint);
        if (PlayerScript != null)
            ai.playerScript = PlayerScript;
        else
            ai.playerTransform = playerTransform;
        if (directToPlayer != null) ai.directToPlayer = directToPlayer;
        ai.myEB = myEB;
    }

    private void spawnBeginningEnemies()
    {
        foreach (Transform spawn in beginSpawnPoints)
        {
            spawnSingleEnemy(enemyNormalPrefab[0], spawn);
        }
    }

    private async void EnemyBossSpawner()
    {
        while (!Application.isEditor || Application.isPlaying)
        {
            //Debug.Log("enemy waiting: " + waitTimer(1000, 5000, 0.01f).ToString());
            await Task.Delay(waitTimer(5000, 90000, 0.003f));
            //await Task.Delay(waitTimer(5000, 900, 0.03f));
            if (!Application.isPlaying) return;
            toughnessModifier += 0.05f;
            randomSpawnPoint = Random.Range(0, availableSpawns.Count);
            randomSpawnMob = Random.Range(0, enemyBossPrefab.Length);
            spawnSingleEnemy(enemyBossPrefab[randomSpawnMob], spawnPoints[randomSpawnPoint]);
        }
    }

    private async void EnemyShamanSpawner()
    {
        while (!Application.isEditor || Application.isPlaying)
        {
            await Task.Delay(waitTimer(2000, 30000, 0.002f));
            if (!Application.isPlaying) return;
            toughnessModifier += 0.025f;
            randomSpawnPoint = Random.Range(0, availableSpawns.Count);
            randomSpawnMob = Random.Range(0, enemyShamanPrefab.Length);
            spawnSingleEnemy(enemyShamanPrefab[randomSpawnMob], spawnPoints[randomSpawnPoint]);
        }
    }

    private async void EnemySpawner()
    {
        while (!Application.isEditor || Application.isPlaying)
        {
            await Task.Delay(waitTimer(1000, 8000, 0.001f));
            if (!Application.isPlaying) return;
            toughnessModifier += 0.01f;
            randomSpawnPoint = Random.Range(0, availableSpawns.Count);
            randomSpawnMob = Random.Range(0, enemyNormalPrefab.Length);
            spawnSingleEnemy(enemyNormalPrefab[randomSpawnMob], spawnPoints[randomSpawnPoint]);
        }
    }
}
