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

    public Text uiTime;
    public Text uiPoints;

    private float gamestageCountDown = 10f;
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
                }
            break;
            case 2: // game on
                titlescreen.color = new Color(1f, 1f, 1f, titlescreen.color.a - (Time.deltaTime * 0.1f));
                var ts = TimeSpan.FromSeconds((double)gamestageCountDown);
                uiTime.text = string.Format("{0:00}:{1:00}", ts.TotalMinutes, ts.Seconds);
            break;
            case 3: // post score
            break;
            default: // tutorial
            break;
        }
    }
}
