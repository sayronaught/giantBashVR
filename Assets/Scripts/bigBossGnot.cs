using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bigBossGnot : MonoBehaviour
{
    public bool isAwake = false;
    public float Hitpoints = 281f;
    public RawImage hitPointBar;
    public hammerController mainHC;

    public AudioClip clipRoar;
    public AudioClip[] footSteps;

    private AudioSource myAS;
    private Animator myAnim;

    public void Roar()
    {
        myAS.clip = clipRoar;
        myAS.Play();
    }
    public void wakeUp()
    {
        isAwake = true;
        myAnim.SetBool("wakeup", true);
        myAnim.SetTrigger("wakeup");
    }
    public void takeStep()
    {
        myAS.clip = footSteps[Random.Range(0,footSteps.Length)];
        myAS.Play();
    }
    public void takeDamage(float damage)
    {
        Hitpoints -= damage;
        hitPointBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Hitpoints, 70);
        if (damage > 15f) myAnim.SetTrigger("takedamage");
        myAnim.SetFloat("hitpoints", Hitpoints);
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
