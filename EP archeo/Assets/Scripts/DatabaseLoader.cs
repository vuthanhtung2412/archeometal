using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bdd_ep;
using static SelectionSystem;

public class DatabaseLoader : MonoBehaviour, ArcheoLoader
{
    private List<TagList> internalTagList;
    private ObjectArcheo[] internalObjList;
    public ArcheoBuilder archeoBuilder;

    public TagList[] loadTags()
    {
        internalTagList = new List<TagList>();
        internalObjList = ObjectArcheo.getAll();
        foreach (ObjectArcheo o in internalObjList)
            archeoBuilder.init(o.me);
        List<Tag> databaseTags = Tag.getAllTags();
        TagList l = new TagList();
        l.objects= new List<ObjectArcheo>(internalObjList);
        internalTagList.Add(l);
        return internalTagList.ToArray();
    }
}
