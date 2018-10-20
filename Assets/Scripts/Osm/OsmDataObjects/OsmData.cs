using System;

[Serializable]
public class OsmData
{
    public string type;
    public long id;

    public OsmData(string type, long id)
    {
        this.type = type;
        this.id = id;
    }
}