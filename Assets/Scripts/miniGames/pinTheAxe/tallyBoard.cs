using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class tallyBoard : MonoBehaviour
{
    public pinTheAxeController myGM;

    public bool testing = false;
    public bool wipe = false;
    public int currentContender = 1;

    public TMP_Text tags;
    public TMP_Text levels;
    public GameObject scoreboardOBJ;

    public string[] names;

    [System.Serializable]
    public class score
    {
        public int totalscore;
        public int stage;
        public int diff;
        public int axes;
        public int contender;
        public string tag;
    }

    public List<score> scores = new List<score>();
    public string Json;
    public void toJson10() //fuse the top 10 scores
    {
        Json = null; //wipes Json
        for (int i = 0; i < 10; i++) //loop 10
        {
            if (i < scores.Count) //stop index overflow
            {
                Json += JsonUtility.ToJson(scores[i]); //takes first 10 scores and asigns them to Json as a string
            }
        }
    }

    public void fromJson10() //split the top 10 scores
    {
        if (PlayerPrefs.GetString("pinTheAxeHighScore") != "") // checks if there is a string to load
        {
            Json = PlayerPrefs.GetString("pinTheAxeHighScore"); //grabs string Json from playerPrefs and assigns it to Json
            string[] temp = Json.Split('}'); //splits the Json string into an array and deleting a } on each
            scores.Clear(); //clears and readies scores for load
            for (var i = 0; i < 10; i++) //loop 10
            {
                if (i < temp.Length -1) //allows all exept the last string wich only holds a }
                {
                    scores.Add(JsonUtility.FromJson<score>(temp[i] + "}")); //adds a } to the end of the converts the new strings to a score 
                }
            }
        }
    }

    public void loadScore()
    {
        if (PlayerPrefs.GetInt("pinTheAxeCurrentContender") > 0)  //checks for a int to load
        {
            currentContender = PlayerPrefs.GetInt("pinTheAxeCurrentContender"); //loads int
        }
    }

    public void newScore(int stage , int diff , int axe , string tag)
    {
        loadScore(); //simple load
        fromJson10(); //complex load

        if (tag == null) //assigns random name if null
        {
            if (diff == 1) tag = names[Random.Range(0, 2)] + currentContender;
            if (diff >= 2 && diff <= 4) tag = names[Random.Range(3, 7)] + currentContender;
            if (diff >= 5 && diff <= 6) tag = names[Random.Range(8, 13)] + currentContender;
            if (diff == 7) tag = names[Random.Range(14, 16)] + currentContender;
            if (diff == 8) tag = names[Random.Range(17,18)] + currentContender;

        }
        scores.Add(new score { totalscore = stage + diff * 6 - 6, stage = stage, diff = diff ,axes = axe ,contender = currentContender ,tag = tag});
        //adds new earned score
        testing = false; //debug test
        currentContender++; //increases current contender int 
        scores.Sort((t1, t2) => t2.totalscore.CompareTo(t1.totalscore)); //sorts scores from high to low
        saveScore(); //saves new scores
    }

    public void saveScore() //attemps to save new scores
    {
        toJson10(); //starts Json work around
        PlayerPrefs.SetString("pinTheAxeHighScore", Json); //assings scores to "pinTheAxeHighScore"
        PlayerPrefs.SetInt("pinTheAxeCurrentContender", currentContender); //assigns current contender int to "pinTheAxeCurrentContender"
        PlayerPrefs.Save(); //saves
        
        boardUpdate(); //runs scoreboard update
    }

    public void boardUpdate() //handles GUI scoreboard
    {
        tags.text = ""; //wipes leftover names
        levels.text = ""; //whipes leftover scores
        for (int i = 0; i < 10; i++) //loops 10 times
        {
            if (i < scores.Count) //stop index overflow
            {
                tags.text += scores[i].tag + "\n"; //add the name and moves the track to next line
                levels.text += scores[i].totalscore + "\t\t" + scores[i].stage + "\t\t" + scores[i].diff +"\n"; //add the score and moves the track to next line
            }
        }
    }

    public void Update() //debuging feature
    {
        if (testing) newScore(Random.Range(0,6),Random.Range(1,8),Random.Range(1,999),null);
        testing = false;
        if (wipe) //starts wipe
        {
            PlayerPrefs.SetString("pinTheAxeHighScore", ""); //debug reset
            PlayerPrefs.SetInt("pinTheAxeCurrentContender", 1); //debug reset
            PlayerPrefs.Save(); //saves reset
            scores.Clear();
            currentContender = 1;
            wipe = false;
        }
    }
}
