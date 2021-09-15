using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bigBossGnot : MonoBehaviour
{
    public bool isAwake = false;
    public float Hitpoints = 281f;
    public RawImage hitPointBar;

    public AudioClip clipRoar;
    public AudioClip[] footSteps;

    private AudioSource myAS;
    private Animator myAnim;

    public void Roar()
    {
        myAS.Play();
    }
    public void wakeUp()
    {
        isAwake = true;
        myAnim.SetBool("wakeup", true);
        myAnim.SetTrigger("wakeup");
    }

    // Start is called before the first frame update
    void Start()
    {
        myAS = GetComponent<AudioSource>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
