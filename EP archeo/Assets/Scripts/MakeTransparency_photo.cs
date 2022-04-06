using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparency_photo : MonoBehaviour
{
    private Material[] materials;

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
        materials = this.gameObject.GetComponent<Renderer>().materials;
        for (int i = 0 ; i < materials.Length ; i++)
        {
            Color colorFaded = materials[i].color;
            colorFaded.a = Mathf.Clamp(colorFaded.a + value, 0, 1);
            //Debug.Log(colorFaded.a);
            this.gameObject.GetComponent<Renderer>().materials[i].color = colorFaded;
			this.gameObject.GetComponent<Renderer>().materials[i].renderQueue=(int)UnityEngine.Rendering.RenderQueue.Transparent;
            //Debug.Log(this.gameObject.GetComponent<Renderer>().materials[i].color);
        }
    }
}
