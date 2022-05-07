using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DropdownHelper : MonoBehaviour
{
	public float animationDuration;
	public Toggle[] toggleList;
	public GameObject[] childs;
	public bool[] oldStatuses;
	
	public DropdownHelper parent;
	
	private bool isEnabled = true;
	
	
	private void applyAlpha(Image i, float alpha) {
		// We can't simply do i.color.a=alpha; beacause C#
		Color c = i.color;
		c.a=alpha;
		i.color = c;
	}
	
	
	private bool applyUpdates = true;
	public void apply(bool val) {
		if (!applyUpdates) return;
		applyUpdates = false;
		foreach (Toggle tgl in  toggleList)
			tgl.isOn = val;
		applyAlpha(partial, 0f);
		if (val)
			applyAlpha(checkmark, 1f);
		if (parent != null)
			parent.updateStatus();
		applyUpdates = true;
	}
	
	
	private Animator anim;
	public Image checkmark;
	public Image partial;
	
	void Start() {
		anim = GetComponentInChildren<Animator>();
		applyAlpha(partial, 0f);
	}
	
	public void onClick() {
		if (isEnabled) {
			for (int i = 0; i < childs.Length; i++) { 
				oldStatuses[i] = childs[i].activeInHierarchy;
				childs[i].SetActive(false);
				anim.CrossFade("Base Layer.DropdownRight", animationDuration);
			}
		} else {
			for (int i = 0; i < childs.Length; i++) {
				childs[i].SetActive(oldStatuses[i]);
				anim.CrossFade("Base Layer.DropdownDown", animationDuration);
			}
		}
		isEnabled = !isEnabled;
	}
	
	public void updateStatus() {
		if (!applyUpdates) return;
		bool allOn = true;
		bool allOff = true;
		foreach (Toggle tgl in  toggleList) {
			if (tgl.isOn)
				allOff = false; // At least one item is enabled
			else
				allOn = false; // At least one item is disabled
			
			if ((!allOff) && (!allOn))
				break; // At least one item is disabled and one is enabled
		}
		if (allOn && allOff)
			return; // This shouldn't happen (it would mean that there is no child)
		
		// Normally, we should use SetValueWithoutNotify but it doesn't exist in the UnityEngine.UI version
		// Instead, we will do it... By hand
		applyUpdates = false;
		
		if (allOff) {
			GetComponentInChildren<Toggle>().isOn = false;
			applyAlpha(partial, 0f);
		} else if (allOn) {
			GetComponentInChildren<Toggle>().isOn = true;
			applyAlpha(partial, 0f);
			applyAlpha(checkmark, 1f);
		} else {
			GetComponentInChildren<Toggle>().isOn = false; // So parent dropdowns see this as a disabled item, to correctly display as partial
			applyAlpha(partial, 1f);
			applyAlpha(checkmark, 0f);
		}
		
		if (parent != null)
			parent.updateStatus();
			
		if ((!allOff) && (!allOn))
			GetComponentInChildren<Toggle>().isOn = true; // So when we click on the toggle, it unselect all children
		applyUpdates = true;
	}
}
