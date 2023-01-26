using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class tallyBoard : MonoBehaviour
{
    public pinTheAxeController myGM;

    public bool testing = false;
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
        Json = null;
        for (int i = 0; i < 10; i++)
        {
            if (i < scores.Count)
            {
                Json += JsonUtility.ToJson(scores[i]);
            }
        }
    }

    public void fromJson10() //split the top 10 scores
    {
        Json = PlayerPrefs.GetString("pinTheAxeHighScore");
        string[] temp = Json.Split('{');
        scores.Clear();
        for (var i = 0; i < 10; i++)
        {
            if (i + 1 < temp.Length)
            {
                scores.Add(JsonUtility.FromJson<score>("{" + temp[i + 1]));
            }
        }

    }

    public void loadScore()
    {
        if (PlayerPrefs.GetInt("pinTheAxeCurrentContender") > 0)
        {
            currentContender = PlayerPrefs.GetInt("pinTheAxeCurrentContender");
        }
    }

    public void newScore(int stage , int diff , int axe , string tag)
    {
        loadScore();
        fromJson10();

        if (tag == null)
        {
            if (diff == 1) tag = names[Random.Range(0, 2)] + currentContender;
            if (diff >= 2 && diff <= 4) tag = names[Random.Range(3, 7)] + currentContender;
            if (diff >= 5 && diff <= 6) tag = names[Random.Range(8, 13)] + currentContender;
            if (diff == 7) tag = names[Random.Range(14, 16)] + currentContender;
            if (diff == 8) tag = names[Random.Range(17,18)] + currentContender;

        }
        scores.Add(new score { totalscore = stage + diff * 6 - 6, stage = stage, diff = diff ,axes = axe ,contender = currentContender ,tag = tag});
        testing = false;
        currentContender++;
        scores.Sort((t1, t2) => t2.totalscore.CompareTo(t1.totalscore));
        saveScore();
    }

    public void saveScore()
    {
        toJson10();
        PlayerPrefs.SetString("pinTheAxeHighScore", Json);
        PlayerPrefs.SetInt("pinTheAxeCurrentContender", currentContender);
        PlayerPrefs.Save();
        
        boardUpdate();
    }

    public void boardUpdate()
    {
        tags.text = "";
        levels.text = "";
        for (int i = 0; i < 10; i++)
        {
            if (i < scores.Count)
            {
                tags.text += scores[i].tag + "\n";
                levels.text += scores[i].totalscore + "\t\t" + scores[i].stage + "\t\t" + scores[i].diff +"\n";
            }
        }
    }

    public void Update()
    {
        if (testing) newScore(Random.Range(0,6),Random.Range(1,8),Random.Range(1,999),null);
        testing = false;
    }
}
