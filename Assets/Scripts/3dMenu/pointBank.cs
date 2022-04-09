using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointBank : MonoBehaviour
{
    public TextMesh uiPoints;
    public TextMesh uiHighScore;
    public AudioClip sfxBuy;
    public AudioClip sfxDeny;
    public AudioClip sfxSelect;

    public shopItem activeSkin;

    private _Settings mySettings;
    private AudioSource mySound;
    private float timeToUpdate = 0f;
    public int lastShown = 0;

    public void playSound( AudioClip ac )
    {
        if (!mySound) return;
        mySound.clip = ac;
        mySound.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        var permObj = GameObject.Find("_SettingsPermanentObject");
        if (permObj) mySettings = permObj.GetComponent<_Settings>();
        mySound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToUpdate -= Time.deltaTime;
        if ( mySettings && uiPoints && timeToUpdate < 0f)
        {
            if (lastShown == mySettings.storedPoints) return;
            if (mySettings.storedPoints > 0)
                uiPoints.text = mySettings.storedPoints.ToString() + "\nPoints";
            else
                uiPoints.text = "0\nPoints";
            uiHighScore.text = "Highscore ";
            if (mySettings.highestScore > 0)
            {
                uiHighScore.text += mySettings.highestScore.ToString() + "\nin " + mySettings.highestScoreworld;
            }
            else uiHighScore.text += "None";
            uiHighScore.text += "\nDamage Done\n"+mySettings.damageDone.ToString();
            uiHighScore.text += "\nHighest Damage\n" + mySettings.damageHighest.ToString();
            uiHighScore.text += "\nDamage Taken\n" + mySettings.damageTaken.ToString();
            uiHighScore.text += "\nJotuns Bashed\n" + mySettings.jotunsBashed.ToString();
            timeToUpdate = 1f;
            lastShown = mySettings.storedPoints;
        }
    }
}
