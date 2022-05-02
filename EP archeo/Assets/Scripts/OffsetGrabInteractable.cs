using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OffsetGrabInteractable:XRGrabInteractable
{
    /*
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
    */

    private Vector3 initialAttachLocalPos;
    private Quaternion initialAttachLocalRot;
    private EventCentre EC;
    void Start()
    {
        // create attachment point
        if (!attachTransform)
        {
            GameObject attach = new GameObject("Attach");
            attach.transform.SetParent(transform, false);
            attachTransform = attach.transform;
        }
        initialAttachLocalPos = attachTransform.localPosition;
        initialAttachLocalRot = attachTransform.localRotation;
        EC = GameObject.Find("EventCentre").GetComponent<EventCentre>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if(args.interactorObject is XRDirectInteractor)
        {
            attachTransform.position = args.interactorObject.transform.position;
            attachTransform.rotation = args.interactorObject.transform.rotation;
        }
        else
        {
            attachTransform.localPosition = initialAttachLocalPos;
            attachTransform.localRotation = initialAttachLocalRot; 
        }

        base.OnSelectEntering(args);
    }


    protected override void Grab()
    {
        base.Grab();
        Debug.Log(base.name);
        EC.UpdateStudyObj(base.gameObject);
    }
}
