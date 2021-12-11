using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBallBounce : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUps;
    
    public int enemyCount = 1;
    private float spawnRange = 35;
    public int waveNumber = 1;
   
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    void Update()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = obj.Length;

        if(enemyCount == 0)
        {
            waveNumber++;

            SpawnEnemyWave(waveNumber);
            Instantiate(powerUps, GenerateSpawnPosition(), powerUps.transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        //float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 12, 43);

        return randomPos;
    }


}
