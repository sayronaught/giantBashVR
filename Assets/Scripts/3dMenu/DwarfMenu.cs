using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfMenu : MonoBehaviour
{
    public AudioSource sfAnvil;
    public AudioSource sfDwarf;
    public UnityEngine.Video.VideoPlayer myVideo;

    public AudioClip[] anvilSounds;

    public bool isTalking;

    private Animator myAnim;
    
    public void eventHitAnvil()
    {
        sfAnvil.clip = anvilSounds[Random.Range(0, anvilSounds.Length)];
        sfAnvil.pitch = Random.Range(0.6f, 0.8f);
        sfAnvil.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTalking)
        {
            myAnim.SetBool("Idle", true);
            myVideo.gameObject.SetActive(true);
        } else { 
            myAnim.SetBool("Idle", false);
            myVideo.gameObject.SetActive(false);
        }
    }
}
