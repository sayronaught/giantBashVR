using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XrOffsetGrab : XRGrabInteractable
{
    public hammerController mainHC;
    public hammerControllerEndlessMode mainHXendless;

    private Vector3 interactorPosition = Vector3.zero;
    private Quaternion interactorRotation = Quaternion.identity;

    protected override void Grab()
    {
        base.Grab();
        if ( mainHC )
            attachTransform.position = mainHC.rightHand.transform.position + (mainHC.rightHand.transform.up * 0.15f);
        else
            attachTransform.position = mainHXendless.rightHand.transform.position + (mainHXendless.rightHand.transform.up * 0.15f);

    }
}
