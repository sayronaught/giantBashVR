using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyClothingRandomEquip : MonoBehaviour
{

    public GameObject[] torsoClothing;
    public GameObject[] requiredLegClothing;
    public GameObject[] randomClothing;

    //Chance at a random clothing piece appearing in % (0-100)
    int randomChance = 35;


    void Start()
    {
        if (torsoClothing.Length > 0)
        {
            GameObject torsoC = torsoClothing[Random.Range(0, torsoClothing.Length)];
            if (torsoC != null)
                torsoC.SetActive(true);

        }

        if (requiredLegClothing.Length > 0)
        {
            foreach (GameObject pants in requiredLegClothing)
            {
                pants.SetActive(false);
            }
            GameObject legC = requiredLegClothing[Random.Range(0, requiredLegClothing.Length)];
            if (legC != null)
                legC.SetActive(true);

        }

        if (randomClothing.Length > 0)
        {
            foreach (GameObject randomC in randomClothing)
            {
                int randomNumber = Random.Range(0,100);
                if(randomChance > randomNumber)
                    randomC.SetActive(true);
            }

        }

    }
}
