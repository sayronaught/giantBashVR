using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamicEnemy : MonoBehaviour
{
    [System.Serializable]
    public class statList
    {
        public float mass = 1f;
        public float strength = 1f;
        public float speed = 1f;
        public float jumpForce = 1000f;
        public float damageReduction = 1f;
        public float maxHealth = 100f;
    }
    public statList stats;

    [System.Serializable]
    public class animList
    {
        public string[] idles;
        public string hurt;
        public string death;
        public string attackMeleeLight;
        public string attackMeleeheavy;
        public string attackRangeLight;
        public string attackRangeHeavy;
        public string[] conversations;
        public string[] taunts;
        public string[] surrenders;
    }
    public animList anims;
    public bool animIdle = true;

    [System.Serializable]
    public class soundList
    {
        public AudioClip[] steps;
        public AudioClip[] idleChatter;
        public AudioClip[] hurt;
        public AudioClip[] death;
        public AudioClip[] attackLight;
        public AudioClip[] attackHeavy;
        public AudioClip[] taunts;
        public AudioClip[] surrenders;
    }
    public soundList sounds;

    public float Hitpoints = 100f;

    private Animator myAnim;
    private AudioSource mySound;

    public void animEventBackToIdle()
    {
        animIdle = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Hitpoints = stats.maxHealth;
        myAnim = GetComponent<Animator>();
        mySound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
