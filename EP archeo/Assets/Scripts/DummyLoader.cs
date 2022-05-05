using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SelectionSystem;
using bdd_ep;

public class DummyLoader : MonoBehaviour, ArcheoLoader
{
    private List<TagList> internalTagList;
	private ObjectArcheo[] internalObjList;
	
	private bool created = false;
	
	private void createCube(string name, Color color) {
		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
		go.GetComponent<MeshRenderer>().material.color = color;
		go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		go.name = name;
	}
	
	private ObjectArcheo QuickObjectArcheoBuilder(int id, string name, float x, float y, float z) {
		return new ObjectArcheo(id, name, name, new Vector3(x,y,z), Vector3.zero, 1,1,1,  name+" object to test", 0, "");
	}
	
	private void init() {
		if (created) return;
		internalTagList = new List<TagList>();
		TagList tColor = new TagList("Colors");
		TagList tRGB = new TagList("RGB");
		TagList tCMY = new TagList("CMY");
		TagList tBlack = new TagList("Black");
		
		tColor.childs.Add(tRGB);
		tColor.childs.Add(tCMY);
		
		internalTagList.Add(tColor);
		internalTagList.Add(tBlack);
		
		createCube("Red", Color.red);
		createCube("Green", Color.green);
		createCube("Blue", Color.blue);
		createCube("Cyan", Color.cyan);
		createCube("Magenta", Color.magenta);
		createCube("Yellow", Color.yellow);
		createCube("White", Color.white);
		createCube("Black", Color.black);
		
		internalObjList = new ObjectArcheo[8];
		tRGB.objects.Add(internalObjList[0] = QuickObjectArcheoBuilder(1, "Red", 2,2,0));
		tRGB.objects.Add(internalObjList[1] = QuickObjectArcheoBuilder(2, "Green", 3,2,0));
		tRGB.objects.Add(internalObjList[2] = QuickObjectArcheoBuilder(3, "Blue", 4,2,0));
		tCMY.objects.Add(internalObjList[3] = QuickObjectArcheoBuilder(4, "Cyan", 2,3,0));
		tCMY.objects.Add(internalObjList[4] = QuickObjectArcheoBuilder(5, "Magenta", 3,3,0));
		tCMY.objects.Add(internalObjList[5] = QuickObjectArcheoBuilder(6, "Yellow", 4,3,0));
		tColor.objects.Add(internalObjList[6] = QuickObjectArcheoBuilder(7, "White", 2,1,0));
		tBlack.objects.Add(internalObjList[7] = QuickObjectArcheoBuilder(8, "Black", 3,1,0));
		
		created = true;
	}
	
	public TagList[] loadTags(){
		init();
		return internalTagList.ToArray();
	}
	
	public ObjectArcheo[] loadObjectArcheo() {
		init();
		return internalObjList;
	}
	
}
