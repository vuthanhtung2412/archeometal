using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyHelper : MonoBehaviour
{
	// Changing an object transparency at runtime isn't that straightforward because of the models we are using, when the object is 100% opaque,
	// we can't just let the Material in the "fade" mode, otherwise the result is bad-looking
	
	[Range (0,1)]
	public float opacity = 1f;
	private float oldOpacity = 1f;
	
	private Renderer[] rdrList;
	
    // Start is called before the first frame update
    void Start()
    {
        rdrList = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oldOpacity != opacity) {
			foreach (Renderer rdr in rdrList) {
				Material[] mats = rdr.materials;
				for (int i = 0; i < mats.Length; i++)
					mats[i] = apply(mats[i]);
				rdr.materials = mats;
			}
			oldOpacity = opacity;
		}
    }
	
	private Material apply(Material mat) {
		// See https://docs.unity3d.com/Manual/StandardShaderMaterialParameterRenderingMode.html
		// Section "Changing the Rendering Mode using a script"
		Color color = mat.color;
		color.a = opacity;
		int defaultRenderQueue;
		
		if (color.a == 1) { // Switch to opaque mode
			mat.SetOverrideTag("RenderType", "");
			mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
			mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
			mat.SetInt("_ZWrite", 1);
			mat.DisableKeyword("_ALPHATEST_ON");
			mat.DisableKeyword("_ALPHABLEND_ON");
			mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			defaultRenderQueue = -1;
		} else { // Switch to fade mode
			mat.SetOverrideTag("RenderType", "Transparent");
			mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			mat.SetInt("_ZWrite", 0);
			mat.DisableKeyword("_ALPHATEST_ON");
			mat.EnableKeyword("_ALPHABLEND_ON");
			mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			defaultRenderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
		}
		mat.renderQueue = defaultRenderQueue;
		mat.color = color;
		return mat;
	}
	
}