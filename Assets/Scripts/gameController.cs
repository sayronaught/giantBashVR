using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    public Image titlescreen;
    public GameObject thePlayer;
    public Transform posTutorial;
    public Transform posGameOn;
    public AudioSource musicTutorial;
    public AudioSource musicGameOn;
    public GameObject targetLocations;

    public Text uiTime;
    public Text uiPoints;

    private float gamestageCountDown = 10f;
    private float targetSpawnTimer = 0f;
    private int gameStage = 0; // 0 tutorial, 1 approach, 2 game on, 3 post score
    private int gamePoints = 0;

    public void smashedGate()
    {
        gameStage = 1;
        gamePoints = 0;
    }
    public void addPoints(int points)
    {
        gamePoints += points;
        uiPoints.text = gamePoints.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        thePlayer.transform.position = posTutorial.position;
    }

    // Update is called once per frame
    void Update()
    {
        gamestageCountDown -= Time.deltaTime;
        if ( gamestageCountDown < 0f )
        {
            
        }
        switch ( gameStage )
        {
            case 1: // approach
                thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, posGameOn.position, Time.deltaTime * 0.5f);
                if ( Vector3.Distance(thePlayer.transform.position,posGameOn.position) < 0.3f )
                {
                    thePlayer.transform.position = posGameOn.position;
                    gameStage = 2;
                    gamestageCountDown = 300f;
                    musicGameOn.Play();
                    musicTutorial.Stop();
                }
            break;
            case 2: // game on
                titlescreen.color = new Color(1f, 1f, 1f, titlescreen.color.a - (Time.deltaTime * 0.1f));
                var ts = TimeSpan.FromSeconds((double)gamestageCountDown);
                uiTime.text = string.Format("{0:00}:{1:00}", ts.TotalMinutes, ts.Seconds);
                targetSpawnTimer -= Time.deltaTime;
                if ( targetSpawnTimer < 0f)
                {
                    Transform[] targets = gameObject.GetComponentsInChildren<Transform>();
                    Transform randomLocation = targets[UnityEngine.Random.Range(0, targets.Length)];
                    if (randomLocation.childCount == 0)
                    {
                        var newTarget = Instantiate(Resources.Load("Target"),randomLocation.position,Quaternion.identity) as GameObject;
                        newTarget.transform.SetParent(randomLocation);
                        newTarget.transform.position = Vector3.zero;
                        newTarget.GetComponent<targetScript>().mainGC = this;
                        targetSpawnTimer = 3f;
                    }
                }
            break;
            case 3: // post score
            break;
            default: // tutorial
            break;
        }
    }
}
