using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparent_tregeux_simplified : MonoBehaviour
{
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
		this.increment(1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increment(float value)
    {
        Color colorFaded = this.gameObject.GetComponent<Renderer>().material.color;
        colorFaded.a = Mathf.Clamp(colorFaded.a + value, 0, 1);
        Debug.Log(colorFaded.a);
		
		material = this.gameObject.GetComponent<Renderer>().material;
		int defaultRenderQueue;
        
        if (colorFaded.a == 1)
        {
            //this.gameObject.GetComponent<Renderer>().material.SetInt("_ALPHABLEND_ON",0);
            //this.gameObject.GetComponent<Renderer>().material.SetFloat("_Mode",0);
            //this.gameObject.GetComponent<Renderer>().material.EnableKeyword("_NORMALMAP");
			
			// See https://docs.unity3d.com/Manual/StandardShaderMaterialParameterRenderingMode.html
			// Section "Changing the Rendering Mode using a script"
			material.SetOverrideTag("RenderType", "");
			material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
			material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
			material.SetInt("_ZWrite", 1);
			material.DisableKeyword("_ALPHATEST_ON");
			material.DisableKeyword("_ALPHABLEND_ON");
			material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			defaultRenderQueue = -1;
        }
        else{
            //this.gameObject.GetComponent<Renderer>().material.SetInt("_NORMALMAP",0);
            //this.gameObject.GetComponent<Renderer>().material.SetFloat("_Mode",2);
            //this.gameObject.GetComponent<Renderer>().material.EnableKeyword("_ALPHABLEND_ON");
            //this.gameObject.GetComponent<Renderer>().material.color = colorFaded;
			material.SetOverrideTag("RenderType", "Transparent");
			material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			material.SetInt("_ZWrite", 0);
			material.DisableKeyword("_ALPHATEST_ON");
			material.EnableKeyword("_ALPHABLEND_ON");
			material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			defaultRenderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }
		
        material.renderQueue = defaultRenderQueue;
        this.gameObject.GetComponent<Renderer>().material.color = colorFaded;

    }
}
