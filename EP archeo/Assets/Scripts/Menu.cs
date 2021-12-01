using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject StudyObj;
    public int mode = 0;
    public GameObject mesh;
    public GameObject photogrametry;
    public GameObject clippingPlane;
    [Range(0, 3)]
    public int planeNb;
    [Range(0, 3)]
    public int cameraNb;
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
    }
    // Update is called once per frame
    void Update()
    {
        if (mode == 0)
        {
            this.mesh.SetActive(true);
            this.photogrametry.SetActive(true);
        }
        if ( mode == 1 )
        {
            this.mesh.SetActive(true);
            this.photogrametry.SetActive(false);
        }
        if ( mode == 2)
        {
            this.mesh.SetActive(false);
            this.photogrametry.SetActive(true);
        }
        this.clippingPlane.GetComponent<GlobalClippingManager>().planeNb = this.planeNb;
        this.clippingPlane.GetComponent<GlobalClippingManager>().cameraNb = this.cameraNb;
        this.StudyObj.transform.localScale = this.initScale * this.scale;
    }
}
