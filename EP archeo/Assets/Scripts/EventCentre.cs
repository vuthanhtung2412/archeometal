using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class EventCentre : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject studyObj = null;
    public int mode = 0;
    private GameObject mesh = null;
    private GameObject photogrametry = null;
    public GameObject dataPoints = null;

    private GameObject clippingPlane;
    
    [Range(1, 3)]
    public float scale ;
    public Vector3 initScale;
    public Slider sizeSlider;
    public GameObject canvas;
    public TrackedPoseDriver track;

    // info for obj
    public Text n;
    public Text id;
    public Text descp;
    public Image img;

    // for observe mode
    private bool inObserve = false;

    void Start()
    {
        this.clippingPlane = GameObject.Find("Clipping Planes");
        this.scale = 1.0f;
    }
    // Update is called once per frame
    void Update()
    {
            
    }

    public void MeshEtPhot()
    {
        Debug.Log("Mesh+Phot");
        this.mesh.SetActive(true);
        this.photogrametry.SetActive(true);
    }
    public void Mesh()
    {
        Debug.Log("Mesh");
        this.mesh.SetActive(true);
        this.photogrametry.SetActive(false);
    }
    public void Phot()
    {
        Debug.Log("Phot");
        this.mesh.SetActive(false);
        this.photogrametry.SetActive(true);
    }
    public void DisplayClippingPlanes()
    {
        Debug.Log("Displayed Clipping planes");
        if (this.clippingPlane.GetComponent<GlobalClippingManager>().planeNb == 0)
        {
            this.clippingPlane.GetComponent<GlobalClippingManager>().planeNb = 3;
            this.clippingPlane.GetComponent<GlobalClippingManager>().cameraNb = 3;
        }
        else
        {
            this.clippingPlane.GetComponent<GlobalClippingManager>().planeNb = 0;
            this.clippingPlane.GetComponent<GlobalClippingManager>().cameraNb = 0;
        }
        canvas.SetActive(!canvas.activeSelf);
    }

    public void MetadataPoints()
    {

    }
    public void ChangeScale(float delta)
    {
        //this.scale = 1.0f + 2.0f * sizeSlider.value;
		this.scale += delta;
        this.studyObj.transform.localScale = this.initScale * this.scale;
    }

    public void UpdateStudyObj(GameObject obj)
    {
        Debug.Log("out");
        // update if we grab another obj
        if (studyObj == null || !studyObj.Equals(obj))
        {
            Debug.Log("in");
            // hide metadata point of previously selected obj
            if (dataPoints != null)
                this.dataPoints.SetActive(false);
            // turn off observe mode
            this.inObserve = false;
            // update the studyObj
            this.studyObj = obj;
            // Update initial scale
            this.initScale = studyObj.transform.localScale;
            // update mesh
            this.mesh = this.studyObj.transform.GetChild(0).gameObject;
            // update photogrametry
            this.photogrametry = this.studyObj.transform.GetChild(1).gameObject;
            // update metadata points
            this.dataPoints = this.studyObj.transform.Find("Data Points").gameObject;
            this.dataPoints.SetActive(this.inObserve);
            // update global metadata displayed
            Info info = this.studyObj.GetComponent<Info>();
            if (info != null)
            {
                // update info in the canvas
                id.text = "ID of obj : " + info.idObj.ToString();
                foreach (MetadataTung m in info.metaDatas)
                {
                    if (m.relativePositionX == 0 && m.relativePositionY == 0 && m.relativePositionZ == 0)
                    {
                        n.text = "Name of the piece of metadata : " + m.name;
                        descp.text = "Description" + m.desp;
                        // Update foto
                        img.sprite = Resources.Load<Sprite>(m.photoURL);
                        img.type = Image.Type.Simple;
                        img.preserveAspect = true;
                    }
                }
            }
        }
    }
    public void ObserveMetadataPoint()
    {
        if(studyObj != null)
        {
            Debug.Log("meta data point mode");
            this.inObserve = !this.inObserve;
            this.dataPoints.SetActive(this.inObserve);

            // return to global metadata when exit observe mode
            if (!this.inObserve)
            {
                Info info = this.studyObj.GetComponent<Info>();
                if (info != null)
                {
                    // update info in the canvas
                    id.text = "ID of obj : " + info.idObj.ToString();
                    foreach (MetadataTung m in info.metaDatas)
                    {
                        if (m.relativePositionX == 0 && m.relativePositionY == 0 && m.relativePositionZ == 0)
                        {
                            n.text = "Name of the piece of metadata : " + m.name;
                            descp.text = "Description" + m.desp;
                            // Update foto
                            img.sprite = Resources.Load<Sprite>(m.photoURL);
                            img.type = Image.Type.Simple;
                            img.preserveAspect = true;
                        }
                    }
                }
            }
        }
    }

    // display info about the metadata point when the point is selected
    public void displayMetadataPoint(MetadataTung m)
    {
        if (studyObj != null & this.inObserve)
        {
            n.text = "Name of the piece of metadata : " + m.name;
            descp.text = "Description" + m.desp;
            // Update foto
            img.sprite = Resources.Load<Sprite>(m.photoURL);
            img.type = Image.Type.Simple;
            img.preserveAspect = true;
        }
    }
}
