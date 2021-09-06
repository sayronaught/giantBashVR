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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gamestageCountDown -= Time.deltaTime;
        if ( gamestageCountDown < 0f )
        {
            titlescreen.color = new Color(1f,1f,1f, titlescreen.color.a-(Time.deltaTime*0.1f));
        }
        switch ( gameStage )
        {
            case 1: // approach
            break;
            case 2: // game on
            break;
            case 3: // post score
            break;
            default: // tutorial
            break;
        }
    }
}
