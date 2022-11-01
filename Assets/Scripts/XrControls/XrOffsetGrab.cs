using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XrOffsetGrab : XRGrabInteractable
{
    public hammerController mainHC;
    public hammerControllerEndlessMode mainHCendless;
    public hammerControlerCircusMode mainHCcircus;

    private Vector3 interactorPosition = Vector3.zero;
    private Quaternion interactorRotation = Quaternion.identity;

    protected override void Grab()
    {
        base.Grab();
        if (mainHC)
        {
            if (mainHC.leftPress)
                attachTransform.position = mainHC.leftHand.transform.position + (mainHC.leftHand.transform.up * 0.15f);
            else
                attachTransform.position = mainHC.rightHand.transform.position + (mainHC.rightHand.transform.up * 0.15f);
        }
        else if (mainHCendless)
        {
            if (mainHCendless.leftPress)
                attachTransform.position = mainHCendless.leftHand.transform.position + (mainHCendless.leftHand.transform.up * 0.15f);
            else
                attachTransform.position = mainHCendless.rightHand.transform.position + (mainHCendless.rightHand.transform.up * 0.15f);
        }
        else if (mainHCcircus)
        {
            if (mainHCcircus.leftPress)
                attachTransform.position = mainHCcircus.leftHand.transform.position + (mainHCcircus.leftHand.transform.up * 0.15f);
            else
                attachTransform.position = mainHCcircus.rightHand.transform.position + (mainHCcircus.rightHand.transform.up * 0.15f);
        }
    }
}
