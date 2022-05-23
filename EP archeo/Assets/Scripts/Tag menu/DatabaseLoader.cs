using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bdd_ep;
using static SelectionSystem;

public class DatabaseLoader : MonoBehaviour, ArcheoLoader
{
    private List<TagList> internalTagList;
    private ObjectArcheo[] internalObjList;
    private List<ObjectArcheo> taglessObjects;
    public ArcheoBuilder archeoBuilder;

    private bool isEmpty(TagList t)
    {
        if (t.objects.Count != 0) return false;
        foreach (TagList child in t.childs)
            if (!isEmpty(child)) return false;
        return true;
    }

    private TagList addTagRec(Tag initialTag, List<Tag> tagsList) // Recursively add children to tags
    {
        TagList current = new TagList(initialTag._tagName);
        foreach (Tag tag in tagsList)
            if (tag._parentName == initialTag._tagName)
            {
                TagList potential = addTagRec(tag, tagsList);
                if (!isEmpty(potential))
                    current.childs.Add(potential);
            }

        // Add objects
        foreach (int obj_id in Tag.GetObjectsAssociatedWithTag(initialTag._idTag))
            foreach (ObjectArcheo o in internalObjList)
                if (o.id == obj_id)
                {
                    current.objects.Add(o);
                    taglessObjects.Remove(o);
                }
        return current;
    }

    public TagList[] loadTags()
    {
        internalTagList = new List<TagList>();
        internalObjList = ObjectArcheo.getAll();
        taglessObjects = new List<ObjectArcheo>(internalObjList);
        foreach (ObjectArcheo o in internalObjList)
            archeoBuilder.init(o.me);
        List<Tag> databaseTags = Tag.getAllTags();
        internalTagList = new List<TagList>();
        // We add each root tag, and their children recursively
        foreach (Tag currentTag in databaseTags)
        {
            if (currentTag._parentName == "")
            {
                TagList potential = addTagRec(currentTag, databaseTags);
                if (!isEmpty(potential))
                    internalTagList.Add(potential);
            }
        }

        // Finally, we add tagless objects in a separate tag
        if (taglessObjects.Count != 0)
        {
            TagList l = new TagList("(No tag)");
            l.objects = taglessObjects;
            internalTagList.Add(l);
        }
        return internalTagList.ToArray();
    }
}
