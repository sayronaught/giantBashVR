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

    public TMP_Text scoreboard;


    public bool keyboard = false;
    public TouchScreenKeyboard overlayKeyboard;
    public static string inputText = "";

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

    public void newScore(int stage , int diff , int axe , string tag)
    {
        if (tag == null) tag = "Contender" + currentContender;
        scores.Add(new score { totalscore = stage + diff * 6 - 6, stage = stage, diff = diff ,axes = axe ,contender = currentContender ,tag = tag});
        testing = false;
        currentContender++;
        scores.Sort((t1, t2) => t2.totalscore.CompareTo(t1.totalscore));
        boardUpdate();
    }

    public void boardUpdate()
    {

        scoreboard.text = ("name\t\t\tscore\tstage\tlevel");
        for (int i = 0; i < 10 || i == scores.Count; i++)
        {
            scoreboard.text += ("\n" + scores[i].tag + "\t\t" + scores[i].totalscore + "\t" + scores[i].stage + "\t" + scores[i].diff);
        }
    }

    public void finished()
    {
        keyboard = false;
        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        TouchScreenKeyboard.Open("");
        if (overlayKeyboard != null)
            scoreboard.text = overlayKeyboard.text;
    }
    public void Update()
    {
        if (testing) newScore(Random.Range(0,6),Random.Range(1,8),Random.Range(1,999),null);
        testing = false;
       // if (keyboard) finished();
            keyboard = false;
    }
}
