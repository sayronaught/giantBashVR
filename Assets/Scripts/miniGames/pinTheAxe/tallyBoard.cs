using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tallyBoard : MonoBehaviour
{
    public pinTheAxeController myGM;

    public bool testing = false;
    public int currentContender;

    [System.Serializable]
    public class score
    {
        public int totalscore;
        public int stage;
        public int diff;
        public int axes;
        public int contender;
    }
    public List<score> scores = new List<score>();

    public void newScore(int stage , int diff , int axe)
    {
        scores.Add(new score { totalscore = stage + diff * 6 - 6, stage = stage, diff = diff ,axes = axe ,contender = currentContender});
        testing = false;
        Debug.Log(scores[currentContender].axes +" "+ scores[currentContender].totalscore +" " +currentContender);
        currentContender++;
        scores.Sort();
    }
    public void Update()
    {
        if (testing) newScore(Random.Range(1,7), Random.Range(1, 7), Random.Range(1, 7));
    }
}
