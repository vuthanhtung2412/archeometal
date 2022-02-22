using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OffsetGrabInteractable : XRGrabInteractable
{
    public Vector3 interactorPosition = Vector3.zero;
    public Quaternion interactorRotation = Quaternion.identity;

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
        StoreInteractor(interactor);
        MatchAttachmentPoints(interactor);
    }
    private void StoreInteractor(XRBaseInteractor interactor)
    {
        interactorPosition = interactor.attachTransform.localPosition;
        interactorRotation = interactor.attachTransform.localRotaion;
    }
    private void MatchAttachmentPoints(XRBaseInteractor interactor)
    {
        bool hasAttach = attachTransform != null;
        interactor.attachTransform.localPosition = hasAttach ? attachTransform.position : transform.position;
        interactor.attachTransform.localRotaion = hasAttach ? attachTransform.rotation : transform.rotation;
    }

    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);
        ResetAttachMentPoint(interactor);
        ClearInteractor(interactor);
    }
    private void ResetAttachMentPoint(XRBaseInteractor interactor)
    {
        interactor.attachTransform.localPosition = interactorPosition;
        interactor.attachTransform.localRotaion = interactorRotation;
    }
    private void ClearInteractor(XRBaseInteractor interactor)
    {
        interactorPosition = Vector3.zero;
        interactorRotation = Quaternion.identity;
    }
}
