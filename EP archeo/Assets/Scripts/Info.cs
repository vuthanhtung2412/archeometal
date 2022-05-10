using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    public int idObj;
    [SerializeField]
    public List<MetadataTung> metaDatas = new List<MetadataTung>(); 

    [SerializeField]
    public List<TagTung> tags = new List<TagTung>();

    private GameObject dataPoints;

    [SerializeField]
    public List<GameObject> children = new List<GameObject>();

    public GameObject pointPrefabs;
    private void Awake()
    {
        GetTagsAndMetadatas();
    }
    private void Start()
    {
        this.pointPrefabs = Resources.Load<GameObject>("MetadataPoint");
        CreateDataPoints();
    }
    void GetTagsAndMetadatas()
    {
        // To be finished with database de thibault
    }

    // Create dataPoints game object and its children dynamically with collider and metaDatas
    void CreateDataPoints()
    {
        // create Data Points empty game object
        this.dataPoints = new GameObject("Data Points");
        this.dataPoints.transform.SetParent(transform);
        this.dataPoints.transform.localPosition = GetComponent<CapsuleCollider>().center;

        // add metadata point dynamically
        foreach(MetadataTung m in this.metaDatas)
        {
            if(!(m.relativePositionX == 0 && m.relativePositionY == 0 && m.relativePositionZ == 0))
            {
                GameObject tmp = Instantiate(pointPrefabs, Vector3.zero, Quaternion.identity);
                tmp.transform.SetParent(this.dataPoints.transform);
                tmp.name = m.name;
                tmp.transform.localPosition = new Vector3(m.relativePositionX, m.relativePositionY, m.relativePositionZ);
                tmp.GetComponent<MetaPointInteractable>().data = m;
            }  
        }
        this.dataPoints.SetActive(false);
    }

}
