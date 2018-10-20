using System;
using System.Collections.Generic;

namespace Osm.OsmResponseObjects
{
    [Serializable]
    public class OsmElement
    {
        public string type;
        public long id;
        public long[] nodes;
        public Dictionary<string, string> tags;
        public float lat;
        public float lon;

        public OsmElement(string type, long id, long[] nodes, Dictionary<string, string> tags, float lat, float lon)
        {
            this.type = type;
            this.id = id;
            this.nodes = nodes;
            this.tags = tags;
            this.lat = lat;
            this.lon = lon;
        }
    }
}