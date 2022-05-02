using System;

[Serializable]
public struct Tag
{
    public string parentName;
    public string name;

    public Tag(string pn, string n)
    {
        parentName = pn;
        name = n; 
    }
}
