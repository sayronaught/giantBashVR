using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyClothingRandomEquip : MonoBehaviour
{

    public GameObject[] torsoClothing;
    public GameObject[] requiredLegClothing;
    public GameObject[] randomClothing;

    public GameObject[] selectOne01;
    public GameObject[] selectOne02;
    public GameObject[] selectOne03;
    public GameObject[] selectOne04;

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
                    if (randomC != null)
                        randomC.SetActive(true);
            }

        }

        if (selectOne01.Length > 0)
        {
            GameObject selectOne01C = selectOne01[Random.Range(0, selectOne01.Length)];
            if (selectOne01C != null)
                selectOne01C.SetActive(true);

        }
        if (selectOne02.Length > 0)
        {
            GameObject selectOne02C = selectOne02[Random.Range(0, selectOne02.Length)];
            if (selectOne02C != null)
                selectOne02C.SetActive(true);

        }
        if (selectOne03.Length > 0)
        {
            GameObject selectOne03C = selectOne03[Random.Range(0, selectOne03.Length)];
            if (selectOne03C != null)
                selectOne03C.SetActive(true);

        }
        if (selectOne04.Length > 0)
        {
            GameObject selectOne04C = selectOne04[Random.Range(0, selectOne04.Length)];
            if (selectOne04C != null)
                selectOne04C.SetActive(true);

        }

    }
}
