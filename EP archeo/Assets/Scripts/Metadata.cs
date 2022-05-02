using System;

[Serializable]
public struct MetaData
{
    public string name;
    public int idObj;
    public string typeName;
    public string uri;
    public float relativePositionX;
    public float relativePositionY;
    public float relativePositionZ;
    public string desp; // description 
    public string photoURL;

    public MetaData(string n, int id, string tn,string uri, float x, float y, float z, string desp, string photURL)
    {
        name = n;
        idObj = id;
        typeName = tn;
        this.uri = uri;
        relativePositionX = x;
        relativePositionY = y;
        relativePositionZ = z;
        this.desp = desp;
        photoURL = photURL;
    }
}
