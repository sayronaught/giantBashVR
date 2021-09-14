using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.UI;

public class hammerController : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public Animator leftAnim;
    public Animator rightAnim;
    public AudioSource leftCatch;
    public AudioSource rightCatch;
    public GameObject hammer;

    public XRController controllerLeft;
    public XRController controllerRight;
    public XRRayInteractor interactorLeft;
    public XRRayInteractor interactorRight;
    public bool rightPress;
    public bool leftPress;

    public Text debugText;

    private float magnetspeed;
    private float magnetmultiplier = 1.1f;
    private float magnetminimum = 2f;
    public bool supercharged = false;

    public List<Vector3> rightHoldPositions;

    public UnityEngine.XR.InputDevice lefty;
    public UnityEngine.XR.InputDevice righty;

    private Rigidbody hammerRB;
    private hammerFX hammerFXScript;
    private XRGrabInteractable hammerGrabScript;
    private XRRayInteractor leftHandRay;
    private XRRayInteractor rightHandRay;
    private float updateControllerTimer = 2f;
    private float distance;
    private float heldLeft = 0f;
    private float heldRight = 0f;
    public float chargeLightning = 0f;
    private Vector3 inverseTransformDummy;

    public void changeLightning(float value)
    {
        chargeLightning = value;
        hammerFXScript.myLightning.emissionRate = value;
        hammerFXScript.myLightningSFX.volume = value * 0.05f;
    }
    public bool beingSummoned()
    {
        if (magnetspeed > magnetminimum) return true;
        return false;
    }
    public bool beingHeld()
    {
        if (heldLeft > 0f || heldRight > 0f) return true;
        return false;
    }
    public float summonSpeed()
    {
        return magnetspeed - magnetminimum;
    }
    void updatecontroller()
    {
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        lefty = leftHandDevices[0];
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        righty = rightHandDevices[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        controllerLeft = leftHand.GetComponent<XRController>();
        controllerRight = rightHand.GetComponent<XRController>();
        interactorLeft = leftHand.GetComponent<XRRayInteractor>();
        interactorRight = rightHand.GetComponent<XRRayInteractor>();
        hammerRB = hammer.GetComponent<Rigidbody>();
        hammerGrabScript = hammer.GetComponent<XRGrabInteractable>();
        leftHandRay = leftHand.GetComponent<XRRayInteractor>();
        rightHandRay = rightHand.GetComponent<XRRayInteractor>();
        hammerFXScript = hammer.GetComponent<hammerFX>();
        updatecontroller();
        rightHoldPositions = new List<Vector3>();
    }
    // Update is called once per frame
    //void Update()
    void FixedUpdate()
    {
        if (!hammer.activeSelf) return;
        lefty.IsPressed(InputHelpers.Button.Trigger, out leftPress);
        righty.IsPressed(InputHelpers.Button.Trigger, out rightPress);
        //InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller & InputDeviceCharacteristics.TrackedDevice, _inputDevices);
        inverseTransformDummy = rightHand.transform.InverseTransformPoint(hammer.transform.position);
        //debugText.text = "Debug:inverse transform right hand\n" + inverseTransformDummy.x.ToString() + " " + inverseTransformDummy.y.ToString() + " " + inverseTransformDummy.z.ToString();
        if (rightPress && heldLeft == 0f)
        { // press
            rightAnim.SetBool("summoning", true);
            distance = Vector3.Distance(hammer.transform.position, rightHand.transform.position + (rightHand.transform.forward * 0.1f));
            inverseTransformDummy = rightHand.transform.InverseTransformPoint(hammer.transform.position);
            if (distance > 0.15f || inverseTransformDummy.z < 0f)
            {
                //hammer.transform.position = rightHand.transform.position;
                hammer.transform.position = Vector3.MoveTowards(hammer.transform.position, rightHand.transform.position + (rightHand.transform.forward * 0.1f), Time.deltaTime * magnetspeed);
                hammerRB.velocity = Vector3.zero;
                hammerRB.angularVelocity = Vector3.zero;
                hammerRB.mass = 1;
                magnetspeed *= magnetmultiplier;
            }
            else
            {
                rightAnim.SetBool("grab", true);
                hammer.transform.position = rightHand.transform.position+(rightHand.transform.up*0.15f);
                hammer.transform.rotation = rightHand.transform.rotation;
                hammer.transform.Rotate(-75, 0, 90);
                hammerRB.velocity = Vector3.zero;
                hammerRB.angularVelocity = Vector3.zero;
                rightHoldPositions.Add(rightHand.transform.position);
                supercharged = false;
                if (heldRight == 0f )
                {
                    rightHandRay.SendHapticImpulse(1f, 0.2f);
                    rightCatch.Play();
                }                    
                heldRight += Time.deltaTime*3f;
                changeLightning(heldRight);
                if (heldRight > 9f) heldRight = 9f;
                if (heldRight > 0.5f)
                {
                    hammerRB.mass = heldRight+2f;
                    rightHandRay.SendHapticImpulse(heldRight * 0.2f, 0.1f);
                    hammerFXScript.myTrail.emissionRate = heldRight;
                    hammer.transform.Rotate(Random.Range(-heldRight, heldRight), Random.Range(-heldRight, heldRight), Random.Range(-heldRight, heldRight));
                }
                hammerGrabScript.throwVelocityScale = 3f + heldRight;
            }
        }
        else if (leftPress && heldRight == 0f)
        {
            leftAnim.SetBool("summoning", true);
            distance = Vector3.Distance(hammer.transform.position, leftHand.transform.position + (leftHand.transform.forward * 0.10f));
            inverseTransformDummy = leftHand.transform.InverseTransformPoint(hammer.transform.position);
            if (distance > 0.15f || inverseTransformDummy.z < 0f)
            {
                //hammer.transform.position = rightHand.transform.position;
                hammer.transform.position = Vector3.MoveTowards(hammer.transform.position, leftHand.transform.position+(leftHand.transform.forward*0.1f), Time.deltaTime * magnetspeed);
                hammerRB.velocity = Vector3.zero;
                hammerRB.angularVelocity = Vector3.zero;
                hammerRB.mass = 1;
                magnetspeed *= magnetmultiplier;
            }
            else
            {
                leftAnim.SetBool("grab", true);
                hammerFXScript.myLightning.Clear();
                //hammer.transform.position = leftHand.transform.position+(leftHand.transform.up*0.15f);
                //hammer.transform.rotation = leftHand.transform.rotation;
                //hammer.transform.Rotate(-75, 0, 90);
                hammerRB.velocity = Vector3.zero;
                hammerRB.angularVelocity = Vector3.zero;
                rightHoldPositions.Add(leftHand.transform.position);
                supercharged = false;
                if (heldLeft == 0f )
                {
                    leftHandRay.SendHapticImpulse(1f, 0.2f);
                    leftCatch.Play();
                }                    
                heldLeft += Time.deltaTime*3f;
                changeLightning(heldLeft);
                if (heldLeft > 9f) heldLeft = 9f;
                if (heldLeft > 0.5f)
                {
                    hammerRB.mass = heldLeft + 2f;
                    leftHandRay.SendHapticImpulse(heldLeft * 0.2f, 0.1f);
                    hammerFXScript.myTrail.emissionRate = heldLeft;
                    hammer.transform.Rotate(Random.Range(-heldLeft, heldLeft), Random.Range(-heldLeft, heldLeft), Random.Range(-heldLeft, heldLeft));
                }
                hammerGrabScript.throwVelocityScale = 3f + heldLeft;
            }
        }
        else
        //        if ( !rightPress && !leftPress )
        { // not pressed
            magnetspeed = magnetminimum;
            heldLeft = 0f;
            heldRight = 0f;
            leftAnim.SetBool("summoning", false);
            leftAnim.SetBool("grab", false);
            rightAnim.SetBool("summoning", false);
            rightAnim.SetBool("grab", false);
            if (rightHoldPositions.Count > 0)
            { // just released, have list of held positions
                //debugText.text = "Debug: hammerpos held " + rightHoldPositions.Count.ToString();
                int framesBack = 100;
                if (framesBack < rightHoldPositions.Count) framesBack = rightHoldPositions.Count;
                Vector3 force = rightHoldPositions[rightHoldPositions.Count - framesBack] - rightHoldPositions[rightHoldPositions.Count];
                //force = Vector3.Normalize(force);
                force = Vector3.forward;
                hammerRB.velocity = Vector3.zero;
                hammerRB.angularVelocity = Vector3.zero;
                hammerRB.AddForce(force * 100f);
                debugText.text += "\n" + "Force " + hammerRB.velocity;
                hammer.transform.position = Vector3.zero;
                rightHoldPositions.Clear();
                rightHoldPositions = new List<Vector3>();
                magnetspeed = magnetminimum;
                //hammerGrabScript.attachTransform = null;
            }
        }
        updateControllerTimer -= Time.deltaTime;
        if (updateControllerTimer < 0f)
        {
            updatecontroller();
            updateControllerTimer = 2f;
        }
    }
}
