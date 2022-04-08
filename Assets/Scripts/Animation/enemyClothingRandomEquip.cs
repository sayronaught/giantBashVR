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


    public GameObject[] deleteOnStartup;

    //Chance at a random clothing piece appearing in % (0-100)
    int randomChance = 35;

    private dynamicEnemy myDE;

    void Start()
    {
        myDE = GetComponent<dynamicEnemy>();
        if (torsoClothing.Length > 0)
        {
            GameObject torsoC = torsoClothing[Random.Range(0, torsoClothing.Length)];
            if (torsoC != null)
            {
                torsoC.SetActive(true);
                if(myDE) myDE.dropableArmor.Add(torsoC);
            }
            foreach (GameObject torso in torsoClothing)
            {
                if (torso == null)
                    continue;
                if (torso.activeSelf == false)
                {
                    Destroy(torso);
                }
            }

        }

        if (requiredLegClothing.Length > 0)
        {
            foreach (GameObject pants in requiredLegClothing)
            {
                pants.SetActive(false);
            }
            GameObject legC = requiredLegClothing[Random.Range(0, requiredLegClothing.Length)];
            if (legC != null)
            {
                legC.SetActive(true);
                if (myDE) myDE.dropableArmor.Add(legC);
            }
                

            foreach (GameObject legs in requiredLegClothing)
            {
                if (legs == null)
                    continue;
                if (legs.activeSelf == false)
                {
                    Destroy(legs);
                }
            }

        }

        if (randomClothing.Length > 0)
        {
            foreach (GameObject randomC in randomClothing)
            {
                int randomNumber = Random.Range(0,100);
                if(randomChance > randomNumber)
                {
                    if (randomC != null)
                    {
                        randomC.SetActive(true);
                        if (myDE) myDE.dropableArmor.Add(randomC);
                    }
                }
                else
                {
                    if (randomC != null)
                        Destroy(randomC);

                }
            }

        }

        if (selectOne01.Length > 0)
        {
            GameObject selectOne01C = selectOne01[Random.Range(0, selectOne01.Length)];
            if (selectOne01C != null)
            {
                selectOne01C.SetActive(true);
                if (myDE) myDE.dropableArmor.Add(selectOne01C);
            }
                
            foreach (GameObject s1 in selectOne01)
            {
                if (s1 == null)
                    continue;
                if (s1.activeSelf == false)
                {
                    Destroy(s1);
                }
            }

        }
        if (selectOne02.Length > 0)
        {
            GameObject selectOne02C = selectOne02[Random.Range(0, selectOne02.Length)];
            if (selectOne02C != null)
            {
                selectOne02C.SetActive(true);
                if(myDE) myDE.dropableArmor.Add(selectOne02C);
            }

            foreach (GameObject s2 in selectOne02)
            {
                if (s2 == null)
                    continue;
                if (s2.activeSelf == false)
                {
                     Destroy(s2);
                }
            }

        }
        if (selectOne03.Length > 0)
        {
            GameObject selectOne03C = selectOne03[Random.Range(0, selectOne03.Length)];
            if (selectOne03C != null)
            {
                selectOne03C.SetActive(true);
                if (myDE) myDE.dropableArmor.Add(selectOne03C);
            }

            foreach (GameObject s3 in selectOne03)
            {
                if (s3 == null)
                    continue;
                if (s3.activeSelf == false)
                {
                    Destroy(s3);
                }
            }

        }
        if (selectOne04.Length > 0)
        {
            GameObject selectOne04C = selectOne04[Random.Range(0, selectOne04.Length)];
            if (selectOne04C != null)
            {
                selectOne04C.SetActive(true);
                if (myDE) myDE.dropableArmor.Add(selectOne04C);

            }

            foreach (GameObject s4 in selectOne04)
            {
                if (s4 == null)
                    continue;
                if (s4.activeSelf == false)
                {
                    Destroy(s4);
                }
            }

        }

        if (deleteOnStartup.Length > 0)
        {
            foreach (GameObject deleteStartup in deleteOnStartup)
            {
                if (deleteStartup == null)
                    continue;
                if (deleteStartup.activeSelf == false)
                {
                    Destroy(deleteStartup);
                }
            }

        }
    }
}
