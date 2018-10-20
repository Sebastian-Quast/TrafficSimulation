using System;
using System.Collections.Generic;

[Serializable]
public class OsmNode: OsmData
{
    public float lat;
    public float lon;

    public OsmNode(string type, long id, float lat, float lon): base(type, id)
    {
        this.lat = lat;
        this.lon = lon;
    }
    
    public static OsmNode FromElement(OsmElement element)
    {
        return new OsmNode(
            element.type,
            element.id,
            element.lat,
            element.lon
        );
    }
}