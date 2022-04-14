using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hitToReset : MonoBehaviour
{
    public gameController mainGC;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Hammer")) return;
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
        SceneManager.LoadScene(0);
    }
}
