using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MetaPointInteractable : XRSimpleInteractable
{
    private Material red;
    private Material glowingOrange;
    private Material glowingWhite;
    private MeshRenderer r;
    private EventCentre EC;

    //Metadata
    public MetadataTung data;

    void Start()
    {
        red = Resources.Load<Material>("Materials/red");
        glowingOrange = Resources.Load<Material>("Materials/glowingOrange");
        glowingWhite = Resources.Load<Material>("Materials/glowingWhite");
        r = GetComponent<MeshRenderer>();
        EC = GameObject.Find("EventCentre").GetComponent<EventCentre>();
        r.material = red;
        base.interactionManager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
    }
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        r.material = glowingOrange;
    }
    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        r.material = red;
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        r.material = glowingWhite;
        Debug.Log("Metadata point displayed");
        this.EC.displayMetadataPoint(data);
    }
}
