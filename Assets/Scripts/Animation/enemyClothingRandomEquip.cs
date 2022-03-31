using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyClothingRandomEquip : MonoBehaviour
{

    public GameObject[] torsoClothing;
    public GameObject[] requiredLegClothing;
    public GameObject[] randomClothing;


    void Awake()
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
                int i = Random.Range(0,10);
                if(i < 9)
                    randomC.SetActive(true);
            }

        }

    }
}
