using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class _Settings : MonoBehaviour
{

    public int storedPoints = 0;
    public int highestScore = 0;
    public int damageDone = 0;
    public int damageTaken = 0;
    public int damageHighest = 0;
    public int jotunsBashed = 0;
    public int jotunsBashesGnot = 0;
    public int JotunsBashedSmurfs = 0;
    public int JotunsBashesSmall = 0;
    public int JotunsBashedShaman = 0;
    public int JotunsBashedBulky = 0;

    public AudioMixer masterMixer;

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
