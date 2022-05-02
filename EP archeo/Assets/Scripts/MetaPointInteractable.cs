using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MetaPointInteractable : XRSimpleInteractable
{
    private Material red;
    private Material white;
    private MeshRenderer r;
    private EventCentre EC;

    
    void Start()
    {
        red = Resources.Load<Material>("Materials/red");
        white = Resources.Load<Material>("Materials/white");
        r = GetComponent<MeshRenderer>();
        EC = GameObject.Find("EventCentre").GetComponent<EventCentre>();
        r.material = red;
    }
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        r.material = white;
    }
    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        r.material = red;
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        Debug.Log("Metadata point displayed");
    }
}
