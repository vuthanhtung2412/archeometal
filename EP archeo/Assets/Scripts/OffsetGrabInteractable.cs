using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OffsetGrabInteractable:XRGrabInteractable
{
    private Vector3 interactorPosition = Vector3.zero;
    private Quaternion interactorRotation = Quaternion.identity;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        StoreInteractor(args);
        MatchAttachmentPoints(args);
    }
    private void StoreInteractor(SelectEnterEventArgs args)
    {
        interactorPosition = args.interactor.attachTransform.localPosition;
        interactorRotation = args.interactor.attachTransform.localRotation;
    }
    private void MatchAttachmentPoints(SelectEnterEventArgs args)
    {
        bool hasAttach = attachTransform != null;
        args.interactor.attachTransform.localPosition = hasAttach ? attachTransform.position : transform.position;
        args.interactor.attachTransform.localRotation = hasAttach ? attachTransform.rotation : transform.rotation;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        ResetAttachMentPoint(args);
        ClearInteractor(args);
    }
    private void ResetAttachMentPoint(SelectExitEventArgs args)
    {
        args.interactor.attachTransform.localPosition = interactorPosition;
        args.interactor.attachTransform.localRotation = interactorRotation;
    }
    private void ClearInteractor(SelectExitEventArgs args)
    {
        interactorPosition = Vector3.zero;
        interactorRotation = Quaternion.identity;
    }
}
