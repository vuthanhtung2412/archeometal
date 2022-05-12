using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheoBuilder : MonoBehaviour
{
	// Apply all the needed scripts
    public void init(GameObject o) {
		o.AddComponent<TransparencyHelper>();
		o.AddComponent<ClippableObject>();
        Rigidbody rb = o.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        OffsetGrabInteractable grab = o.AddComponent<OffsetGrabInteractable>();
    }
}
