using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class EventCentre : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject StudyObj;
    public int mode = 0;
    public GameObject mesh;
    public GameObject photogrametry;
    public GameObject clippingPlane;
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
        this.StudyObj.transform.localScale = this.initScale * this.scale;
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
    }

    public void UpdateStudyObj(GameObject obj)
    {
        Debug.Log("From Event Centre" + obj.name);
        Info info = obj.GetComponent<Info>();
        if(info != null)
        {
            id.text = info.idObj.ToString();
            foreach (MetaData m in info.metaDatas)
            {
                if (m.relativePositionX == 0 && m.relativePositionY == 0 && m.relativePositionZ == 0)
                {
                    n.text = m.name;
                    descp.text = m.desp;
                    // Update foto
                    img.sprite = Resources.Load<Sprite>(m.photoURL);
                    img.type = Image.Type.Simple;
                    img.preserveAspect = true;
                    break;
                }
            }
        } 
    }

    public void ObserveMetadataPoint()
    {
        Debug.Log("meta data point mode");
    }
}
