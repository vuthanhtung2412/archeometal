using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject StudyObj;
    public int mode = 0;
    public GameObject mesh;
    public GameObject photogrametry;
    public GameObject clippingPlane;
    [Range(1, 3)]
    public float scale;
    public Vector3 initScale;
    void Start()
    {
        this.mesh = this.StudyObj.transform.GetChild(0).gameObject;
        this.photogrametry = this.StudyObj.transform.GetChild(1).gameObject;
        this.clippingPlane = GameObject.Find("Clipping Planes");
        this.initScale = StudyObj.transform.localScale;
        this.scale = 1.0f;
        Debug.Log(this.StudyObj.name);
    }
    // Update is called once per frame
    void Update()
    {
        this.StudyObj.transform.localScale = this.initScale * this.scale;
    }

    public void Mode0()
    {
        this.mesh.SetActive(true);
        this.photogrametry.SetActive(true);
    }
    public void Mode1()
    {
        this.mesh.SetActive(true);
        this.photogrametry.SetActive(false);
    }
    public void Mode2()
    {
        this.mesh.SetActive(false);
        this.photogrametry.SetActive(true);
    }
    public void DisplayClippingPlanes()
    {
        if (this.clippingPlane.GetComponent<GlobalClippingManager>().planeNb == 0)
        {
            this.clippingPlane.GetComponent<GlobalClippingManager>().planeNb = 3;
            this.clippingPlane.GetComponent<GlobalClippingManager>().cameraNb = 3;
        }
        if (this.clippingPlane.GetComponent<GlobalClippingManager>().planeNb == 3)
        {
            this.clippingPlane.GetComponent<GlobalClippingManager>().planeNb = 0;
            this.clippingPlane.GetComponent<GlobalClippingManager>().cameraNb = 0;
        }
    }
}
