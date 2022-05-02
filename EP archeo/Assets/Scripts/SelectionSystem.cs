using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using bdd_ep;

public class SelectionSystem : MonoBehaviour
{
	
	public GameObject listContent;
	public GameObject itemPrefab;
	public GameObject dropdownPrefab;
	
	public float originX;
	public float originY;
	public float h_padding;
	public float v_padding;
	
	// Used to store the tags tree and its associated objects. Generated from the database
	private class TagList {
		public string tag;
		public List<TagList> childs;
		public List<ObjectArcheo> objects;
		public TagList() {
			childs = new List<TagList>();
			objects = new List<ObjectArcheo>();
		}
		public TagList(string name) : this() { // this() calls the TagList() constructor
			tag = name;
		}
	}
	private List<TagList> tags;
	
	// Used to represent a "line" in the user interface. Generated from the TagList using a recursive function below
	private class ItemElement {
		public int level;
		public GameObject listEntry;
		public ObjectArcheo attachedObject; // Null for dropdowns since they are not related to an archeo object
		public ItemElement (int l, GameObject lE, ObjectArcheo aO) {
			level = l;
			listEntry = lE;
			attachedObject = aO;
		}
	}
	private List<ItemElement> orderedItems;
	
	// Recursively add elements in a list, like this:
	/*
		Level 0
			Level 1
				Level 2
					Items in level 2
				Level 2
					Other items
				Items in level 1
			Level 1
				Other items
			Items in level 0
		Level 0
			... And so on
	*/
	// We use the fact that, in C#, lists keeps their order like a stack
	private int currentLevel=0;
	private void addFromTag(TagList tag) {
		// Add the dropdown at the current level
		orderedItems.Add(buildDropDown(tag.tag));
		currentLevel++;
		// We first add child levels
		foreach (TagList child in tag.childs)
			addFromTag(child);
		// Then archeo objects
		foreach (ObjectArcheo obj in tag.objects)
			orderedItems.Add(buildItem(obj));
		// Then we decrease back the current level
		currentLevel--;
	}
	
	// Build dropdowns
	private ItemElement buildDropDown(string name) {
		ItemElement ie = new ItemElement(
			currentLevel,
			Instantiate(dropdownPrefab, listContent.GetComponent<Transform>(), false),
			null);
		ie.listEntry.GetComponentInChildren<Text>().text = name;
		return ie;
	}
	
	// Build item in a list
	private ItemElement buildItem(ObjectArcheo obj) {
		ItemElement ie = new ItemElement(
			currentLevel,
			Instantiate(itemPrefab, listContent.GetComponent<Transform>(), false),
			obj);
		ie.listEntry.GetComponentInChildren<Text>().text=obj.id_excavation;
		ie.listEntry.GetComponent<SelectionHelper>().attachedObject = obj.me;
		return ie;
	}
	
	// Resets the position of each item entry in the list
	private void updateList() {
		int i = 0;
		foreach (ItemElement item in orderedItems) {
			item.listEntry.GetComponent<RectTransform>().localPosition = new Vector3((float)item.level * h_padding + originY, (float)i * v_padding + originX, 0);
			i++;
		}
	}
	
    // Start is called before the first frame update
    void Start()
    {
		tags = new List<TagList>();
		orderedItems = new List<ItemElement>();
		TagList tA = new TagList("A");
		TagList tA1 = new TagList("A1");
		TagList tA2 = new TagList("A2");
		TagList tB = new TagList("B");
		
		ObjectArcheo oA = new ObjectArcheo(2, "A", "A", new Vector3(2,2,0), Vector3.zero, 1, 1, 1, "Racine de A", 0, "");
		ObjectArcheo oA1 = new ObjectArcheo(3, "A1", "A1", new Vector3(2,2.7f,0), Vector3.zero, 1, 1, 1, "1er enfant de A", 0, "");
		ObjectArcheo oA2 = new ObjectArcheo(4, "A2", "A2", new Vector3(3,2,0), Vector3.zero, 1, 1, 1, "2eme de A", 0, "");
		ObjectArcheo oB = new ObjectArcheo(5, "B", "B", new Vector3(3,2.7f,0), Vector3.zero, 1, 1, 1, "Une autre racine", 0, "");
		
		tA.childs.Add(tA1);
		tA.childs.Add(tA2);
		tA.objects.Add(oA);
		tA1.objects.Add(oA1);
		tA2.objects.Add(oA2);
		tB.objects.Add(oB);
		
		
		addFromTag(tA);
		addFromTag(tB);
		
		updateList();
		
		//ObjectArcheo o = new ObjectArcheo(1, "Test", "Cube_test", new Vector3(0,0,0), Vector3.zero, 1,1,1, "Description", 0,"");
		//add(o);


    }

    // Update is called once per frame
    void Update()
    {
		updateList();
    }
	
	void add(ObjectArcheo o) {
		GameObject newItem = Instantiate( itemPrefab, listContent.GetComponent<Transform>(), false);
		newItem.GetComponent<RectTransform>().localPosition = new Vector3(originX, originY, 0);
		originY+=h_padding;
		
		if (o != null) {
			newItem.GetComponent<SelectionHelper>().attachedObject = o.me;
			newItem.GetComponentInChildren<Text>().text=o.id_excavation;
		}
	}
	
}
