using System;

[Serializable]
public struct TagTung
{
    public string parentName;
    public string name;

    public TagTung(string pn, string n)
    {
        parentName = pn;
        name = n; 
    }
}
