using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointBank : MonoBehaviour
{
    public TextMesh uiPoints;
    public AudioClip sfxBuy;
    public AudioClip sfxDeny;
    public AudioClip sfxSelect;

    public shopItem activeSkin;

    private _Settings mySettings;
    private AudioSource mySound;
    private float timeToUpdate = 0f;

    public void playSound( AudioClip ac )
    {
        if (!mySound) return;
        mySound.clip = ac;
        mySound.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        mySettings = GameObject.Find("_SettingsPermanentObject").GetComponent<_Settings>();
        mySound = GetComponent<AudioSource>();
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
