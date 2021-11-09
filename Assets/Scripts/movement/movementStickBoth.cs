using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class movementStickBoth : MonoBehaviour
{
    public Transform xrHeadPosition;
    public CapsuleCollider myCollider;
    public Rigidbody myRB;

    public UnityEngine.XR.InputDevice leftController;
    public UnityEngine.XR.InputDevice rightController;
    public float updateControllerTimer = 0f;
    public bool leftTrigger = false;
    public bool rightTrigger = false;
    public float jumpCoil = 0f;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Terrain") Physics.IgnoreCollision(gameObject.GetComponent<CapsuleCollider>(), collision.collider, true);
    }

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<CapsuleCollider>();
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        leftController.IsPressed(InputHelpers.Button.Trigger, out leftTrigger);
        rightController.IsPressed(InputHelpers.Button.Trigger, out rightTrigger);
        bool goUp = false;
        leftController.IsPressed(InputHelpers.Button.PrimaryAxis2DUp, out goUp);
        bool goDown = false;
        leftController.IsPressed(InputHelpers.Button.PrimaryAxis2DDown, out goDown);
        bool goLeft = false;
        leftController.IsPressed(InputHelpers.Button.PrimaryAxis2DLeft, out goLeft);
        bool goRight = false;
        leftController.IsPressed(InputHelpers.Button.PrimaryAxis2DRight, out goRight);
        bool turnLeft = false;
        rightController.IsPressed(InputHelpers.Button.PrimaryAxis2DLeft, out turnLeft);
        bool turnRight = false;
        rightController.IsPressed(InputHelpers.Button.PrimaryAxis2DRight, out turnRight);
        bool jump = false;
        rightController.IsPressed(InputHelpers.Button.PrimaryAxis2DUp, out jump);
        bool crouch = false;
        rightController.IsPressed(InputHelpers.Button.PrimaryAxis2DDown, out crouch);
        float speed = 1f;
        if (crouch) speed = 1.5f;
        if (goUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (xrHeadPosition.forward * speed*1.25f), 0.1f);
        }
        if (goDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (xrHeadPosition.forward * -speed), 0.1f);
        }
        if (goLeft)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (xrHeadPosition.right * -speed*0.75f), 0.1f);
        }
        if (goRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + xrHeadPosition.right*speed*0.75f, 0.1f);
        }
        if (turnLeft)
        {
            transform.Rotate(new Vector3(0f,-1f,0f));
        }
        if (turnRight)
        {
            transform.Rotate(new Vector3(0f, 1f, 0f));
        }
        if ( crouch)
        {
            myCollider.center = new Vector3(0f, .75f, 0f);
            jumpCoil += Time.deltaTime;
        } else {
            myCollider.center = new Vector3(0f, .5f, 0f);
            jumpCoil -= Time.deltaTime;
            if (jumpCoil < 0f) jumpCoil = 0f;
        }
        if ( jump & jumpCoil >= 0f)
        {
            RaycastHit hit;
            if ( Physics.Raycast(transform.position,Vector3.down,out hit, 1f))
            {
                if ( hit.transform.tag == "Terrain")
                {
                    float force = 10f;
                    force *= 1f + ((int)jumpCoil).ToString().Length;
                    myRB.AddForce(new Vector3(0f, force, 0f));
                    jumpCoil = -2f;
                }                
            }
        }
        if (jumpCoil < 0f) jumpCoil += Time.deltaTime;
        updateControllerTimer -= Time.deltaTime;
        if (!(updateControllerTimer < 0f)) return;
        updateController();
        updateControllerTimer = 2f;
    }
}
