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
	
	private ArcheoLoader archeoLoader;
	
	public float originX;
	public float originY;
	public float h_padding;
	public float v_padding;
	
	private RectTransform contentRect;
	
	// Used to store the tags tree and its associated objects. Generated from the database
	public class TagList {
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
	private void addFromTag(TagList tag, ItemElement parent) {
		// Store the current index in the orderedItems list for later
		int currentI = orderedItems.Count;
		// Add the dropdown at the current level
		DropdownHelper parentDropdown = (parent==null)? null: parent.listEntry.GetComponent<DropdownHelper>();
		ItemElement me = buildDropDown(tag.tag,parentDropdown);
		DropdownHelper meDropdown = me.listEntry.GetComponent<DropdownHelper>();
		orderedItems.Add(me);
		currentLevel++;
		// We first add child levels
		foreach (TagList child in tag.childs)
			addFromTag(child, me);
		// Then archeo objects
		foreach (ObjectArcheo obj in tag.objects)
			orderedItems.Add(buildItem(obj,meDropdown));
		// Then we decrease back the current level
		currentLevel--;
		
		// Now we set the Toggle[] array by looking at the list again
		int endList = orderedItems.Count;
		List<Toggle> tglList = new List<Toggle>();
		List<GameObject> goList = new List<GameObject>();
		for (int i = currentI+1; i < endList; i++) { // currentI + 1 so we don't add the parent to the child list
			tglList.Add(orderedItems[i].listEntry.GetComponentInChildren<Toggle>());
			goList.Add(orderedItems[i].listEntry);
		}
		
		// And we set it in the dropdown helper
		DropdownHelper dH = orderedItems[currentI].listEntry.GetComponent<DropdownHelper>();
		dH.toggleList = tglList.ToArray();
		dH.childs = goList.ToArray(); 
		dH.oldStatuses = new bool[goList.Count];
	}
	
	// Build dropdowns
	private ItemElement buildDropDown(string name, DropdownHelper parent) {
		ItemElement ie = new ItemElement(
			currentLevel,
			Instantiate(dropdownPrefab, listContent.GetComponent<Transform>(), false),
			null);
		ie.listEntry.GetComponentInChildren<Text>().text = name;
		ie.listEntry.GetComponent<DropdownHelper>().parent = parent;
		ie.listEntry.GetComponent<DropdownHelper>().selectionSystem = this;
		return ie;
	}
	
	// Build item in a list
	private ItemElement buildItem(ObjectArcheo obj, DropdownHelper parent) {
		ItemElement ie = new ItemElement(
			currentLevel,
			Instantiate(itemPrefab, listContent.GetComponent<Transform>(), false),
			obj);
		ie.listEntry.GetComponentInChildren<Text>().text=obj.id_excavation;
		ie.listEntry.GetComponent<SelectionHelper>().attachedObject = obj.me;
		ie.listEntry.GetComponent<SelectionHelper>().parent = parent;
		return ie;
	}
	
	// Resets the position of each item entry in the list
	public void updateList() {
		int i = 0;
		foreach (ItemElement item in orderedItems) {
			if (item.listEntry.activeInHierarchy) {
				item.listEntry.GetComponent<RectTransform>().localPosition = new Vector3((float)item.level * h_padding + originY, (float)i * v_padding + originX, 0);
				i++;
			}
		}
		// Set the size of the Content RectTransform to have proper scrolling
		contentRect.sizeDelta = new Vector2(0f, Mathf.Abs((float)(i+1) * v_padding));
		//contentRect.ForceUpdateRectTransforms();
	}
	
    // Start is called before the first frame update
    void Start()
    {
		contentRect = listContent.GetComponent<RectTransform>();
		orderedItems = new List<ItemElement>();

		archeoLoader = GetComponent<ArcheoLoader>();
		tags = new List<TagList>(archeoLoader.loadTags());
		
		foreach (TagList tag in tags)
			addFromTag(tag,null);

		updateList();
		//ObjectArcheo o = new ObjectArcheo(1, "Test", "Cube_test", new Vector3(0,0,0), Vector3.zero, 1,1,1, "Description", 0,"");
		//add(o);


    }

    // Update is called once per frame
    void Update()
    {
		// Uncomment to update at each frame (for example to set up the positions in the editor while running)
		// However, it is normally not necessary since the updateList() is called when needed
		// updateList();
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

public interface ArcheoLoader {
	/*
	 * Interface used to initialize the list content
	*/
	
	// Give all the existing tags
	SelectionSystem.TagList[] loadTags();
	
}
