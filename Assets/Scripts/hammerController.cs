using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class hammerController : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject hammer;

    public XRController controllerLeft;
    public XRController controllerRight;
    public XRRayInteractor interactorLeft;
    public XRRayInteractor interactorRight;
    public bool rightPress;
    public bool leftPress;

    // Start is called before the first frame update
    void Start()
    {
        controllerLeft = leftHand.GetComponent<XRController>();
        controllerRight = rightHand.GetComponent<XRController>();
        interactorLeft = leftHand.GetComponent<XRRayInteractor>();
        interactorRight = rightHand.GetComponent<XRRayInteractor>();

        
    }
    // Update is called once per frame
    void Update()
    {
        //InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller & InputDeviceCharacteristics.TrackedDevice, _inputDevices);
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
            device.IsPressed(InputHelpers.Button.TriggerPressed, out rightPress);
            device.IsPressed(InputHelpers.Button.TriggerPressed, out leftPress);
            if ( rightPress || leftPress )
            {
                //hammer.transform.position = leftHand.transform.position;
                hammer.transform.position = rightHand.transform.position;
            }
        }
    }
}
