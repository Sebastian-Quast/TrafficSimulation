using System;
using System.Collections.Generic;

[Serializable]
public class OsmDataSet
{
    public double version;
    public string generator;
    public Dictionary<string, string> osm3s;
    public List<OsmWay> ways;
    public List<OsmNode> nodes;
    public List<OsmElement> others;

    public OsmDataSet(double version, string generator, Dictionary<string, string> osm3S, List<OsmWay> ways, List<OsmNode> nodes, List<OsmElement> others)
    {
        this.version = version;
        this.generator = generator;
        osm3s = osm3S;
        this.ways = ways;
        this.nodes = nodes;
        this.others = others;
    }
}