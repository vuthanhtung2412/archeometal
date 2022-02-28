using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OffsetGrabInteractable:XRGrabInteractable
{
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        MatchAttachPoints(args.interactor );
    }

    private void MatchAttachPoints(XRBaseInteractor interactor)
    {
        bool isDirect = interactor is XRDirectInteractor;
        attachTransform.localPosition = isDirect ? interactor.attachTransform.position : transform.position;
        attachTransform.localRotation = isDirect ? interactor.attachTransform.rotation : transform.rotation;
    }

    protected override void Grab()
    {
        base.Grab();
        Debug.Log(base.name);
    }
}
