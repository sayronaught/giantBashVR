using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class _Settings : MonoBehaviour
{
    public AudioMixer masterMixer;
    public GameObject currentHammerSkin;

    public int storedPoints = 0;
    public int highestScore = 0;
    public int damageDone = 0;
    public int damageTaken = 0;
    public int damageHighest = 0;
    public int jotunsBashed = 0;
    public killList kills;
    public int gamesStarted = 0;
    public int secondsPlayed = 0;

    

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad( gameObject );
    }

    private void Update()
    {
        //masterMixer.SetFloat("VolAmb", -80f);
        //masterMixer.SetFloat("VolDia", -80f);
        //masterMixer.SetFloat("VolMus", -80f);
        //masterMixer.SetFloat("VolSfx", -80f);
    }

}
