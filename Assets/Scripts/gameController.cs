using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    public Image titlescreen;

    private float gamestageCountDown = 10f;

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
    }
}
