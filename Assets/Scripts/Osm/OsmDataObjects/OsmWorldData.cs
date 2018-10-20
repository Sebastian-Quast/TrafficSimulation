using System;
using System.Collections.Generic;
using Osm.OsmResponseObjects;

namespace Osm.OsmDataObjects
{
    [Serializable]
    public class OsmWorldData
    {
        public double version;
        public string generator;
        public OsmRect rect;
        public Dictionary<string, string> osm3s;
        public List<OsmWay> ways;
        public List<OsmNode> nodes;
        public List<OsmElement> others;

        public OsmWorldData(double version, string generator, OsmRect rect, Dictionary<string, string> osm3S, List<OsmWay> ways, List<OsmNode> nodes, List<OsmElement> others)
        {
            this.rect = rect;
            this.version = version;
            this.generator = generator;
            osm3s = osm3S;
            this.ways = ways;
            this.nodes = nodes;
            this.others = others;
        }
    }
}