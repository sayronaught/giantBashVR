using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidDynamicSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public cart myCart;
    public int waypoint;
    public int spawnAmount = 1;
    public EndlessSpawner myHandler;

    private EndlessPlayerScript PlayerScript = null;
    private Transform playerTransform = null;
    private Transform directToPlayer = null;
    private EffectBank myEB = null;

    private Vector3 spawnZone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void release()
    {
        for (int i = 1; i <= spawnAmount; i++)
        {
            PlayerScript = myHandler.PlayerScript;
            playerTransform = myHandler.playerTransform;
            directToPlayer = myHandler.directToPlayer;
            myEB = myHandler.myEB;
            spawnZone = new Vector3(transform.position.x + Random.Range(transform.lossyScale.x - 0.5f , -transform.lossyScale.x + 0.5f ), transform.position.y + Random.Range(transform.lossyScale.y , -transform.lossyScale.y ), transform.position.z + Random.Range(transform.lossyScale.z - 0.5f , -transform.lossyScale.z + 0.5f ));
            var spawn = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], spawnZone, Quaternion.identity);
            var ai = spawn.GetComponent<dynamicEnemy>();
            myCart.waypoints[waypoint].obstacles.Add(spawn);
            //ai.spawnSetDifficulty(toughnessModifier);
            ai.setWaypoints(transform);
            if (PlayerScript != null)
                ai.playerScript = PlayerScript;
            else
                ai.playerTransform = playerTransform;
            if (directToPlayer != null) ai.directToPlayer = directToPlayer;
            ai.myEB = myEB;
        }
        Destroy(gameObject);
        
        
    }
}
