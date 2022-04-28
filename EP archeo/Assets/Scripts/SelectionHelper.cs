using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionHelper : MonoBehaviour
{
	
	public GameObject attachedObject;
	
    public void changeVisibility(bool val) {
		attachedObject.SetActive(val);
	}
	
	public void changeTransparency(float val) {
		Material[] materials = attachedObject.GetComponent<Renderer>().materials;
        for (int i = 0 ; i < materials.Length ; i++)
        {
            Color colorFaded = materials[i].color;
            colorFaded.a = Mathf.Clamp(val, 0, 1);
            //Debug.Log(colorFaded.a);
            attachedObject.GetComponent<Renderer>().materials[i].color = colorFaded;
			attachedObject.GetComponent<Renderer>().materials[i].renderQueue=(int)UnityEngine.Rendering.RenderQueue.Transparent;
            //Debug.Log(this.gameObject.GetComponent<Renderer>().materials[i].color);
        }
	}
}
