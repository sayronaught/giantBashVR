using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class benchmarkFPS : MonoBehaviour
{
    public TextMeshPro fpsHud;
    public int worstFps = 72;

    public Transform rightHandPos;

    public int currentFps;
    private float secondCount = 1f;
    private int framesThisSecond = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*currentFps = (int)(1f / Time.deltaTime);
        if (Time.frameCount % 50 == 0)
            fpsHud.text = currentFps.ToString() + "/" + worstFps.ToString() + " FPS";
        */
        secondCount -= Time.deltaTime;
        framesThisSecond++;
        if (secondCount > 0f) return;
        secondCount = 1f;
        currentFps = framesThisSecond;
        framesThisSecond = 0;
        if (rightHandPos)
            fpsHud.text = currentFps.ToString() + "/72 FPS\n+x" + rightHandPos.localPosition.x.ToString() + "\ny" + rightHandPos.localPosition.y.ToString() + "\nz" + rightHandPos.localPosition.z.ToString();
        else
            fpsHud.text = currentFps.ToString() + "/72 FPS";
    }
}
