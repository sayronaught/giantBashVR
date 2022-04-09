using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject uiPointPopupPrefab;
    public Text debugText;
    public bigBossGnot bigBossScript;
    public uiEffects uiEffScript;
    public GameObject uiVinder;
    public GameObject uiTaber;

    public GameObject prefabGate;
    public GameObject[] prefabTargets;
    public GameObject[] smurfs;

    public List<targetScript> targetList;

    public float gameTimeGameOn = 300f;
    public float gameTimeResetWait = 60f;

    public float gamestageCountDown = 10f;
    private float targetSpawnTimer = 0f;
    public int gameStage = 0; // 0 tutorial, 1 approach, 2 game on, 3 boss level, 4 post score
    private int gamePoints = 0;
    private GameObject currentGate;
    private _Settings mySettings;

    public int maxSpeed = 0;

    private TimeSpan ts;

    public bool debugAddPoints = false;

    public void addSmurf()
    {
        int smurf = UnityEngine.Random.Range(0, smurfs.Length - 1);
        //Debug.Log(smurf);
        var randomSmurf = smurfs[smurf];
        if (randomSmurf) randomSmurf.gameObject.SetActive(true);
    }
    public void releaseTheSmurfs()
    {
        for ( int i = 0; i < smurfs.Length;i++)
        {
            if (smurfs[i]) smurfs[i].gameObject.SetActive(true);
        }
    }
    public void smashedGate()
    {
        gameStage = 1;
        gamePoints = 0;
    }
    public void addPoints(int points)
    {
        gamePoints += points;
        uiPoints.text = gamePoints.ToString();
        uiEffScript.transform.localScale = new Vector3(0.03f,0.03f,0.1f);
        uiEffScript.scaleTo = true;
        debugAddPoints = false;
        var pointPopup = Instantiate(uiPointPopupPrefab,uiEffScript.transform);
        if (mySettings)
        {
            mySettings.storedPoints += points;
            if (mySettings.highestScore < gamePoints)
            {
                mySettings.highestScore = gamePoints;
                mySettings.highestScoreworld = "Trainingheim";
            }
        }
    }
    private void destroyTargets()
    {
        foreach (targetScript target in targetList)
        {
            target.isHit = true;
            target.disappearTimer = 0f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        targetList = new List<targetScript>();
        thePlayer.transform.position = posTutorial.position;
        mySettings = GameObject.Find("_SettingsPermanentObject").GetComponent<_Settings>();
    }
    private void spawnTarget()
    {
        var randomLocation = targetLocations.transform.GetChild(UnityEngine.Random.Range(0, targetLocations.transform.childCount));
        if (randomLocation.childCount != 0) return;
        var targetPref = UnityEngine.Random.Range(0, prefabTargets.Length);
        var newTarget = Instantiate(prefabTargets[targetPref], Vector3.zero, Quaternion.identity) as GameObject;
        newTarget.transform.SetParent(randomLocation);
        newTarget.transform.localRotation= Quaternion.identity;
        newTarget.GetComponent<targetScript>().mainGC = this;
        newTarget.transform.localPosition = Vector3.zero;
        targetList.Add(newTarget.GetComponent<targetScript>());
        if (UnityEngine.Random.Range(0, 30) == 1) addSmurf();
    }
    // Update is called once per frame
    private void Update()
    {
        if (debugAddPoints) addPoints(1);
        gamestageCountDown -= Time.deltaTime;
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
                    for (var i = 0; i < 5; i++) spawnTarget();
                }
                break;
            case 2: // game on - shoot targets
                titlescreen.color = new Color(1f, 1f, 1f, titlescreen.color.a - (Time.deltaTime * 0.1f));
                var timeInSecondsInt = (int)gamestageCountDown;  //We don't care about fractions of a second, so easy to drop them by just converting to an int
                var minutes = timeInSecondsInt / 60;  //Get total minutes
                var seconds = timeInSecondsInt - (minutes * 60);  //Get seconds for display alongside minutes
                uiTime.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");  //Create the string representation, where both seconds and minutes are at minimum 2 digits
                targetSpawnTimer -= Time.deltaTime;
                if ( targetSpawnTimer < 0f)
                {
                    spawnTarget();
                    targetSpawnTimer = 1f;
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
                var timeInSecondsInt2 = (int)gamestageCountDown;  //We don't care about fractions of a second, so easy to drop them by just converting to an int
                var minutes2 = timeInSecondsInt2 / 60;  //Get total minutes
                var seconds2 = timeInSecondsInt2 - (minutes2 * 60);  //Get seconds for display alongside minutes
                uiTime.text = minutes2.ToString("D2") + ":" + seconds2.ToString("D2");  //Create the string representation, where both seconds and minutes are at minimum 2 digits
                targetSpawnTimer -= Time.deltaTime;
                thePlayer.transform.position = bigBossScript.bossStage == 0 ? Vector3.MoveTowards(thePlayer.transform.position, posAdvance.position, Time.deltaTime * 0.5f) : Vector3.MoveTowards(thePlayer.transform.position, posGameOn.position, Time.deltaTime * 0.15f);
                if (gamestageCountDown < 0f)
                {
                    gamestageCountDown = gameTimeResetWait;
                    gameStage = 4;
                    uiPointsGold.SetActive(true);
                    uiPointsSilver.SetActive(false);
                    uiTime.text = "-";
                    uiTimeGold.SetActive(false);
                    uiTimeSilver.SetActive(true);
                    //hammerGameObject.SetActive(false);
                    fxApplause.Play();
                    uiTaber.SetActive(true);
                }
                break;
            case 4: // post score
                if (gamestageCountDown < 0f)
                {
                    SceneManager.LoadScene(0);
                    //uiTaber.SetActive(true);
                }
                break;
            default: // tutorial
                
            break;
        }
    }
}
