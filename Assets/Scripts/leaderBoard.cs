using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class leaderBoard : MonoBehaviour
{

    // api : http://127.0.0.1:3000/scoreboard
    // Start is called before the first frame update
    [Serializable]
    public struct oneScore
    {
        int id;
        string name;
        int score;
        string createdAt;
    }
    [Serializable]
    public struct scoreBoard
    {
        public oneScore[] scores;
    }

    public scoreBoard sb;
    public bool gotIt = false;
    public TextMesh scoreText;

    void Start()
    {
        Debug.Log("debug log working");
        //getScore();
        StartCoroutine(getScore());
    }

    // Update is called once per frame
    void Update()
    {
        //myObject = JsonUtility.FromJson<MyClass>(json);
    }
    IEnumerator getScore()
    {
        Debug.Log("starting to get scores");
        string url = "http://127.0.0.1:3000/scoreboard";
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            Processjson(www.text);
        } else {
            Debug.Log("ERROR: " + www.error);
        }
    }
    private void Processjson(string jsonString)
    {
        Debug.Log("got scores");
        Debug.Log(jsonString);
        //sb = JsonUtility.FromJson<scoreBoard>(jsonString);
        sb = JsonUtility.FromJson<scoreBoard>("{\"scores\":" + jsonString + "}");
        gotIt = true;
        scoreText.text = ".: Leaderboard :.\n";
        Debug.Log(sb);
        for (int i = 0; i < sb.scores.Length; i++)
        {
            scoreText.text += sb.scores[i]+"\n";
            Debug.Log(sb.scores[i]);
            //parsejson.but_title.Add(jsonvale["buttons"][i]["title"].ToString());
            //parsejson.but_image.Add(jsonvale["buttons"][i]["image"].ToString());
        }
    }
}
