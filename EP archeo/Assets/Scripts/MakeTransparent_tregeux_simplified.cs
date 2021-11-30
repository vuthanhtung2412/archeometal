using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparent_tregeux_simplified : MonoBehaviour
{
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
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
        /*
        if (colorFaded.a == 1)
        {
            //this.gameObject.GetComponent<Renderer>().material.SetInt("_ALPHABLEND_ON",0);
            this.gameObject.GetComponent<Renderer>().material.SetFloat("_Mode",0);
            this.gameObject.GetComponent<Renderer>().material.EnableKeyword("_NORMALMAP");
        }
        else{
            //this.gameObject.GetComponent<Renderer>().material.SetInt("_NORMALMAP",0);
            this.gameObject.GetComponent<Renderer>().material.SetFloat("_Mode",2);
            this.gameObject.GetComponent<Renderer>().material.EnableKeyword("_ALPHABLEND_ON");
            this.gameObject.GetComponent<Renderer>().material.color = colorFaded;
        }
        */
        this.gameObject.GetComponent<Renderer>().material.color = colorFaded;

    }
}
