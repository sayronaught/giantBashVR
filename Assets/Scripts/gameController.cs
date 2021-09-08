using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    public Image titlescreen;
    public GameObject thePlayer;
    public GameObject theHammer;
    public Transform posTutorial;
    public Transform posGameOn;
    public AudioSource musicTutorial;
    public AudioSource musicGameOn;
    public GameObject targetLocations;
    public GameObject hammerGameObject;

    public Text uiTime;
    public Text uiPoints;
    public Text debugText;

    public GameObject prefabGate;
    public GameObject[] prefabTargets;

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
        //var newTarget = Instantiate(prefabGate, Vector3.zero, Quaternion.identity) as GameObject;
        //newTarget.GetComponent<tutorialGate>().mainGC = this;
        //newTarget.transform.localPosition = new Vector3(-6.5f, 1.26f, 2.83f);
    }
    void spawnTarget()
    {
        Transform randomLocation = targetLocations.transform.GetChild(UnityEngine.Random.Range(0, targetLocations.transform.childCount));
        if (randomLocation.childCount == 0)
        {
            var newTarget = Instantiate(prefabTargets[0], Vector3.zero, Quaternion.identity) as GameObject;
            newTarget.transform.SetParent(randomLocation);
            newTarget.GetComponent<targetScript>().mainGC = this;
            newTarget.transform.localPosition = Vector3.zero;
            targetSpawnTimer = 10f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //debugText.text = "Debug:spawn 1:" + ;
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
                    for (int i = 0; i < 5; i++) spawnTarget();
                }
            break;
            case 2: // game on
                titlescreen.color = new Color(1f, 1f, 1f, titlescreen.color.a - (Time.deltaTime * 0.1f));
                var ts = TimeSpan.FromSeconds((double)gamestageCountDown);
                uiTime.text = string.Format("{0:00}:{1:00}", ts.TotalMinutes, ts.Seconds);
                targetSpawnTimer -= Time.deltaTime;
                if ( targetSpawnTimer < 0f)
                {
                    spawnTarget();
                }
                if (gamestageCountDown < 0f)
                {
                    gamestageCountDown = 60f;
                    gameStage = 3;
                    uiTime.text = "-";
                    hammerGameObject.SetActive(false);
                }
            break;
            case 3: // post score
                if (gamestageCountDown < 0f)
                {
                    gameStage = 0;
                    titlescreen.color = new Color(1f, 1f, 1f, 1f);
                    thePlayer.transform.position = posTutorial.position;
                    theHammer.transform.position = posTutorial.position;
                    gamePoints = 0;
                    uiPoints.text = "-";
                    var newTarget = Instantiate(prefabGate, Vector3.zero, Quaternion.identity) as GameObject;
                    newTarget.GetComponent<tutorialGate>().mainGC = this;
                    newTarget.transform.localPosition = new Vector3(-6.5f, 1.26f, 2.83f);
                    musicGameOn.Stop();
                    musicTutorial.Play();
                    hammerGameObject.SetActive(true);
                }
                break;
            default: // tutorial
                
            break;
        }
    }
}
