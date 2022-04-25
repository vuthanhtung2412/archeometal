using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		for (int i =0; i < 15; i++)
			add();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void add() {
		GameObject newItem = Instantiate( itemPrefab, listContent.GetComponent<Transform>(), false);
		newItem.GetComponent<RectTransform>().localPosition = new Vector3(originX, originY, 0);
		originY+=padding;
	}
}
