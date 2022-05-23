using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;

public class _Settings : MonoBehaviour
{
    public bool haveGfxCard = false;
    public AudioMixer masterMixer;
    public GameObject currentHammerSkin;
    public List<GameObject> boughtSkins;
    public List<GameObject> allSkinsForStore;
    public List<GameObject> gfxSkinsForStore;

    [System.Serializable]
    public class world
    {
        public int loadOrder = 1;
        public string title = "nameHEIM";
        [TextArea]
        public string story = "Short tale";
        public VideoClip videoclip;
    }
    public List<world> worlds;
    public int currentWorld = 1;

    [System.Serializable]
    public class highScore
    {
        public int highestScore = 0;
        public string highestScoreworld = "None";
        public int damageDone = 0;
        public int damageTaken = 0;
        public int damageHighest = 0;
        public int jotunsBashed = 0;
        public int timePlayed;
        public int timeStarted;
    }
    public List<highScore> highScores;


    public int storedPoints = 0;
    public int highestScore = 0;
    public string highestScoreworld = "None";
    public int damageDone = 0;
    public int damageTaken = 0;
    public int damageHighest = 0;
    public int jotunsBashed = 0;
    public killList kills;
    public int gamesStarted = 0;
    public int secondsPlayed = 0;

    private float saveTimer = 10f;

    private void loadPlayerPrefs()
    {
        if (PlayerPrefs.GetInt("StoredPoints") > 0) storedPoints = PlayerPrefs.GetInt("StoredPoints");
        if (PlayerPrefs.GetInt("HighestScore") > 0 )
        {
            highestScore = PlayerPrefs.GetInt("HighestScore");
            highestScoreworld = PlayerPrefs.GetString("HighestScoreWorld");
        }
        if (PlayerPrefs.GetInt("DamageDone") > 0) damageDone = PlayerPrefs.GetInt("DamageDone");
        if (PlayerPrefs.GetInt("DamageTaken") > 0) damageTaken = PlayerPrefs.GetInt("DamageTaken");
        if (PlayerPrefs.GetInt("DamageHighest") > 0) damageHighest = PlayerPrefs.GetInt("DamageHighest");
        if (PlayerPrefs.GetInt("JotunsBashed") > 0) jotunsBashed = PlayerPrefs.GetInt("JotunsBashed");
    }
    private void savePlayerPrefs()
    {
        PlayerPrefs.SetInt("StoredPoints", storedPoints);
        PlayerPrefs.SetInt("HighestScore", highestScore);
        PlayerPrefs.SetString("HighestScoreWorld", highestScoreworld);
        PlayerPrefs.SetInt("DamageDone", damageDone);
        PlayerPrefs.SetInt("DamageTaken", damageTaken);
        PlayerPrefs.SetInt("DamageHighest", damageHighest);
        PlayerPrefs.SetInt("JotunsBashed", jotunsBashed);
        PlayerPrefs.Save();
    }
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad( gameObject );
        loadPlayerPrefs();
        // We add physics based skins, if we run on a platform with a graphics card
#if UNITY_STANDALONE_WIN
        allSkinsForStore.AddRange(gfxSkinsForStore);
        haveGfxCard = true;
#endif
    }

    private void Update()
    {
        saveTimer -= Time.deltaTime;
        if ( saveTimer < 0f)
        {
            savePlayerPrefs();
            saveTimer = 10f;
        }
        //masterMixer.SetFloat("VolAmb", -80f);
        //masterMixer.SetFloat("VolDia", -80f);
        //masterMixer.SetFloat("VolMus", -80f);
        //masterMixer.SetFloat("VolSfx", -80f);
    }

}
