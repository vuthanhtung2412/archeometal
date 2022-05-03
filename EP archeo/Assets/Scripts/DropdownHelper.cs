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
	
	private bool isEnabled = true;
	
	public void apply(bool val) {
		foreach (Toggle tgl in  toggleList)
			tgl.isOn = val;
	}
	
	private Animator anim;
	void Start() {
		anim = GetComponentInChildren<Animator>();
	}
	
	public void onClick() {
		if (isEnabled) {
			for (int i = 1; i < childs.Length; i++) { // We start at 1 so we don't disable ourself
				oldStatuses[i] = childs[i].activeInHierarchy;
				childs[i].SetActive(false);
				anim.CrossFade("Base Layer.DropdownRight", animationDuration);
			}
		} else {
			for (int i = 1; i < childs.Length; i++) {
				childs[i].SetActive(oldStatuses[i]);
				anim.CrossFade("Base Layer.DropdownDown", animationDuration);
			}
		}
		isEnabled = !isEnabled;
	}
	
}
