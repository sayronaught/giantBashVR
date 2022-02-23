using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class benchmarkFPS : MonoBehaviour
{
    public TextMeshPro fpsHud;
    public int worstFps = 72;

    public Transform rightHandPos;

    public int currentFps;
    private float secondCount = 1f;
    private int framesThisSecond = 0;

    public UnityEngine.XR.InputDevice leftController;
    public UnityEngine.XR.InputDevice rightController;
    public float updateControllerTimer = 0f;
    private void updateController()
    {
        if (Application.isEditor) return;
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        leftController = leftHandDevices[0];
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        rightController = rightHandDevices[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateControllerTimer -= Time.deltaTime;
        if (updateControllerTimer < 0f)
        {
            updateController();
            updateControllerTimer = 2f;
        }
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
        bool debug = false;
        rightController.IsPressed(InputHelpers.Button.SecondaryButton, out debug);
        bool reset = false;
        rightController.IsPressed(InputHelpers.Button.PrimaryButton, out reset);
        if ( debug )
        {// wants debug on or off
            if ( fpsHud.gameObject.activeSelf )
            {
                fpsHud.gameObject.SetActive(false);
            } else {
                fpsHud.gameObject.SetActive(true);
            }
            
        }
        if ( reset )
        {
            //Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(0);
        }
    }
}
