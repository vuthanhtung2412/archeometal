using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using bdd_ep;

public class SelectionSystem : MonoBehaviour
{
	
	public GameObject listContent;
	public GameObject itemPrefab;
	
	public float originX;
	public float originY;
	public float padding;
	
    // Start is called before the first frame update
    void Start()
    {
		ObjectArcheo o = new ObjectArcheo(1, "a", "Cube_test", new Vector3(0,0,0), new Vector3(0,0,0), 1,1,1, "Description", 0,"");
		add(o);
		for (int i =0; i < 15; i++)
			add(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void add(ObjectArcheo o) {
		GameObject newItem = Instantiate( itemPrefab, listContent.GetComponent<Transform>(), false);
		newItem.GetComponent<RectTransform>().localPosition = new Vector3(originX, originY, 0);
		originY+=padding;
		
		if (o != null) {
			newItem.GetComponent<SelectionHelper>().attachedObject = o.me;
			newItem.GetComponentInChildren<Text>().text=o.id_excavation;
		}
	}
}
