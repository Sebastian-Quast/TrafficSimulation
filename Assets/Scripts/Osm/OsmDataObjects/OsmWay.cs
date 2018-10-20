using System;
using System.Collections.Generic;
using Osm.OsmResponseObjects;

namespace Osm.OsmDataObjects
{
    [Serializable]
    public class OsmWay : OsmData
    {
        public long[] nodes;
        public Dictionary<string, string> tags;

        public OsmWay(string type, long id, long[] nodes, Dictionary<string, string> tags) : base(type, id)
        {
            this.nodes = nodes;
            this.tags = tags;
        }

        public static OsmWay FromElement(OsmElement element)
        {
            return new OsmWay(
                element.type,
                element.id,
                element.nodes,
                element.tags
            );
        }
    }
}