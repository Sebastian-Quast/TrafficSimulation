using System;
using System.Collections.Generic;

namespace Osm.OsmResponseObjects
{
    [Serializable]
    public class OsmResponse
    {
        public double version;
        public string generator;
        public Dictionary<string, string> osm3s;
        public OsmElement[] elements;
    }
}