using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidDynamicSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    private bool released = false;
    public cart myCart;
    public int waypoint;
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
        var spawn = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], transform.position, Quaternion.identity);
        var ai = spawn.GetComponent<dynamicEnemy>();
        myCart.waypoints[waypoint].obstacles.Add(spawn);
        //ai.spawnSetDifficulty(toughnessModifier);
        //ai.setWaypoints(spawnpoint);
        //if (PlayerScript != null)
        //    ai.playerScript = PlayerScript;
        //else
        //    ai.playerTransform = playerTransform;
        //if (directToPlayer != null) ai.directToPlayer = directToPlayer;
        //ai.myEB = myEB;
        Destroy(gameObject);
        
    }
}
