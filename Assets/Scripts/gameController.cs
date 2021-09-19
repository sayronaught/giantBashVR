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
    public Transform posAdvance;
    public AudioSource musicTutorial;
    public AudioSource musicBossLevel;
    public AudioSource fxHorn;
    public AudioSource fxApplause;
    public GameObject targetLocations;
    public GameObject hammerGameObject;

    public Text uiTime;
    public Text uiPoints;
    public GameObject uiTimeSilver;
    public GameObject uiTimeGold;
    public GameObject uiPointsSilver;
    public GameObject uiPointsGold;
    public GameObject uiBossBar;
    public Text debugText;
    public bigBossGnot bigBossScript;

    public GameObject prefabGate;
    public GameObject[] prefabTargets;

    public List<targetScript> targetList;

    public float gameTimeGameOn = 300f;
    public float gameTimeResetWait = 60f;

    public float gamestageCountDown = 10f;
    private float targetSpawnTimer = 0f;
    public int gameStage = 0; // 0 tutorial, 1 approach, 2 game on, 3 boss level, 4 post score
    private int gamePoints = 0;
    private GameObject currentGate;

    public int maxSpeed = 0;

    private TimeSpan ts;

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
    public void spawnGate()
    {
        var newGate = Instantiate(prefabGate, Vector3.zero, Quaternion.identity) as GameObject;
        newGate.GetComponent<tutorialGate>().mainGC = this;
        newGate.transform.localPosition = new Vector3(-6.76f, 1.45f, 2.32f);
        currentGate = newGate;
    }
    public void destroyTargets()
    {
        foreach (targetScript target in targetList)
        {
            target.isHit = true;
            target.disappearTimer = 0f;
        }
    }
    public void bigReset()
    { // the big reset button/function that can be called from any controller/ui at any time
        gameStage = 0;
        titlescreen.color = new Color(1f, 1f, 1f, 1f);
        thePlayer.transform.position = posTutorial.position;
        hammerGameObject.transform.position = posTutorial.position;
        gamePoints = 0;
        uiPoints.text = "-";
        uiPointsSilver.SetActive(true);
        uiPointsGold.SetActive(false);
        uiBossBar.SetActive(false);
        bigBossScript.throwTarget1GO.SetActive(true);
        bigBossScript.throwTarget2GO.SetActive(true);
        bigBossScript.Reset();
        if (!currentGate) spawnGate();
        musicTutorial.Play();
        hammerGameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        targetList = new List<targetScript>();
        thePlayer.transform.position = posTutorial.position;
    }
    void spawnTarget()
    {
        Transform randomLocation = targetLocations.transform.GetChild(UnityEngine.Random.Range(0, targetLocations.transform.childCount));
        if (randomLocation.childCount == 0)
        {
            int targetpref = UnityEngine.Random.Range(0, prefabTargets.Length);
            var newTarget = Instantiate(prefabTargets[targetpref], Vector3.zero, Quaternion.identity) as GameObject;
            newTarget.transform.SetParent(randomLocation);
            newTarget.GetComponent<targetScript>().mainGC = this;
            newTarget.transform.localPosition = Vector3.zero;
            targetList.Add(newTarget.GetComponent<targetScript>());
        }
    }
    // Update is called once per frame
    void Update()
    {
        //debugText.text = "Debug: music/state timer " + gamestageCountDown.ToString();
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
                    gamestageCountDown = gameTimeGameOn;
                    fxHorn.Play();
                    for (int i = 0; i < 5; i++) spawnTarget();
                }
            break;
            case 2: // game on
                titlescreen.color = new Color(1f, 1f, 1f, titlescreen.color.a - (Time.deltaTime * 0.1f));
                //ts = TimeSpan.FromSeconds((double)gamestageCountDown);
                //uiTime.text = string.Format("{0:00}:{1:00}", ts.TotalMinutes, ts.Seconds);
                //uiTime.text = TimeSpan.FromSeconds((double)gamestageCountDown).ToString("mm:ss");
                int timeInSecondsInt = (int)gamestageCountDown;  //We don't care about fractions of a second, so easy to drop them by just converting to an int
                int minutes = timeInSecondsInt / 60;  //Get total minutes
                int seconds = timeInSecondsInt - (minutes * 60);  //Get seconds for display alongside minutes
                uiTime.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");  //Create the string representation, where both seconds and minutes are at minimum 2 digits
                targetSpawnTimer -= Time.deltaTime;
                if ( targetSpawnTimer < 0f)
                {
                    spawnTarget();
                    targetSpawnTimer = 2f;
                }
                if (gamestageCountDown < 0f)
                {
                    gameStage = 3;
                    gamestageCountDown = 180f;
                    musicTutorial.Stop();
                    musicBossLevel.Play();
                    uiTimeGold.SetActive(true);
                    uiTimeSilver.SetActive(false);
                    destroyTargets();
                    bigBossScript.wakeUp();
                    uiBossBar.SetActive(true);
                }
            break;
            case 3: // boss level 
                debugText.text = "Debug: boss hitpoints " + bigBossScript.Hitpoints.ToString()+"\n HS "+maxSpeed.ToString();
                //ts = TimeSpan.FromSeconds((double)gamestageCountDown);
                //uiTime.text = string.Format("{0:00}:{1:00}", ts.TotalMinutes, ts.Seconds);
                //uiTime.text = TimeSpan.FromSeconds((double)gamestageCountDown).ToString("mm:ss");
                int timeInSecondsInt2 = (int)gamestageCountDown;  //We don't care about fractions of a second, so easy to drop them by just converting to an int
                int minutes2 = timeInSecondsInt2 / 60;  //Get total minutes
                int seconds2 = timeInSecondsInt2 - (minutes2 * 60);  //Get seconds for display alongside minutes
                uiTime.text = minutes2.ToString("D2") + ":" + seconds2.ToString("D2");  //Create the string representation, where both seconds and minutes are at minimum 2 digits
                targetSpawnTimer -= Time.deltaTime;
                if ( bigBossScript.bossStage == 0 )
                { // waking up
                    thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, posAdvance.position, Time.deltaTime * 0.5f);
                } else { // isawake
                    thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, posGameOn.position, Time.deltaTime * 0.15f);
                }
                if (gamestageCountDown < 0f)
                {
                    gamestageCountDown = gameTimeResetWait;
                    gameStage = 4;
                    uiPointsGold.SetActive(true);
                    uiPointsSilver.SetActive(false);
                    uiTime.text = "-";
                    uiTimeGold.SetActive(false);
                    uiTimeSilver.SetActive(true);
                    hammerGameObject.SetActive(false);
                    fxApplause.Play();
                }
            break;
            case 4: // post score
                if (gamestageCountDown < 0f)
                {
                    bigReset();
                }
                break;
            default: // tutorial
                
            break;
        }
    }
}
