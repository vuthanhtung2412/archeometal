using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionHelper : MonoBehaviour
{
	
	public GameObject attachedObject;
	public DropdownHelper parent;
	private Renderer rdr;
	
	public void Start() {
		rdr = null;
	}
	
    public void changeVisibility(bool val) {
		attachedObject.SetActive(val);
		if (parent != null)
			parent.updateStatus();
	}
	
	public void changeTransparency(float val) {
		if (rdr == null)
			rdr = attachedObject.GetComponent<Renderer>();
		Material[] materials = rdr.materials;
        for (int i = 0 ; i < materials.Length ; i++)
        {
            Color colorFaded = materials[i].color;
            colorFaded.a = Mathf.Clamp(val, 0, 1);
            //Debug.Log(colorFaded.a);
            rdr.materials[i].color = colorFaded;
			rdr.materials[i].renderQueue=(int)UnityEngine.Rendering.RenderQueue.Transparent;
            //Debug.Log(this.gameObject.GetComponent<Renderer>().materials[i].color);
        }
	}
}
