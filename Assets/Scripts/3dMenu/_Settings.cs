using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class _Settings : MonoBehaviour
{

    public int storedPoints = 0;

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
