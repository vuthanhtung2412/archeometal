using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    public int idObj;
    [SerializeField]
    public List<MetaData> metaDatas = new List<MetaData>(); 

    [SerializeField]
    public List<Tag> tags = new List<Tag>();

    private void Awake()
    {
        GetTagsAndMetadatas();
    }
    void GetTagsAndMetadatas()
    {
        // To be finished with database de thibault
    }

}
