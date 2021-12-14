using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadDwarf : MonoBehaviour
{
    public AudioSource sfAnvil;
    public AudioSource sfDwarf;
    public TextMesh vikingFact;

    public AudioClip[] anvilSounds;
    public string[] vikingFacts;

    public void eventHitAnvil()
    {
        sfAnvil.clip = anvilSounds[Random.Range(0,anvilSounds.Length)];
        sfAnvil.pitch = Random.Range(0.6f, 0.8f);
        sfAnvil.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        vikingFact.text = "Fact: "+vikingFacts[Random.Range(0, vikingFacts.Length)];
        vikingFact.text = vikingFact.text.Replace("\\n", "\n");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
