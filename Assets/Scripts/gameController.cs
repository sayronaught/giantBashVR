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

    private float gamestageCountDown = 10f;
    private int gameStage = 0; // 0 tutorial, 1 approach, 2 game on, 3 post score

    public void smashedGate()
    {
        gameStage = 1;
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
                }
            break;
            case 2: // game on
                titlescreen.color = new Color(1f, 1f, 1f, titlescreen.color.a - (Time.deltaTime * 0.1f));
            break;
            case 3: // post score
            break;
            default: // tutorial
            break;
        }
    }
}
