using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionHelper : MonoBehaviour
{
	
	public GameObject attachedObject;
	public DropdownHelper parent;
	private TransparencyHelper th;
	
	public void Start() {
		th = null;
	}
	
    public void changeVisibility(bool val) {
		attachedObject.SetActive(val);
		if (parent != null)
			parent.updateStatus();
	}
	
	public void changeTransparency(float val) {
		if (th == null)
			th = attachedObject.GetComponent<TransparencyHelper>();
		th.opacity = val;
	}
}
