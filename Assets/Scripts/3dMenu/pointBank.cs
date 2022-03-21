using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointBank : MonoBehaviour
{
    public TextMesh uiPoints;

    private _Settings mySettings;
    private float timeToUpdate = 0f;
    // Start is called before the first frame update
    void Start()
    {
        mySettings = GameObject.Find("_SettingsPermanentObject").GetComponent<_Settings>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToUpdate -= Time.deltaTime;
        if ( mySettings && uiPoints && timeToUpdate <0f)
        {
            if (mySettings.storedPoints > 0)
                uiPoints.text = mySettings.storedPoints.ToString()+"\nPoints";
            timeToUpdate = 1f;
        }
    }
}
