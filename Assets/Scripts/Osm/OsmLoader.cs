using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class OsmLoader : MonoBehaviour
{
    private readonly HttpClient client = new HttpClient();

    public double south = 50.80802294398169;
    public double west = 7.2391319274902335;
    public double north = 50.82605366331441;
    public double east = 7.284407615661621;

    void Start()
    {
        DownloadData(south, west, north, east);
    }

    private async Task DownloadData(double s, double w, double n, double e)
    {
        var responseMessage = await client.PostAsync("https://overpass-api.de/api/interpreter",
            GetAreaRequestBody("way", "highway", s, w, n, e));
        var response = await ParseResponse(responseMessage);
        response.ways.ForEach(way => Debug.Log(way.id));
    }

    private async Task<OsmDataSet> ParseResponse(HttpResponseMessage response)
    {
        var responseString = await response.Content.ReadAsStringAsync();
        var osmResponse = JsonUtility.FromJson<OsmResponse>(responseString);
        return ToDataSet(osmResponse);
    }

    private OsmDataSet ToDataSet(OsmResponse response)
    {
        var nodes = new List<OsmNode>();
        var ways = new List<OsmWay>();
        var others = new List<OsmElement>();

        foreach (var element in response.elements)
        {
            switch (element.type)
            {
                case "way":
                    ways.Add(OsmWay.FromElement(element));
                    break;
                case "node":
                    nodes.Add(OsmNode.FromElement(element));
                    break;
                default:
                    others.Add(element);
                    break;
            }
        }
        
        return new OsmDataSet(response.version, response.generator, response.osm3s, ways, nodes, others);
    }

    private FormUrlEncodedContent GetAreaRequestBody(string requestObject, string wayType, double s, double w, double n,
        double e)
    {
        var newString = string.Format(
            "[out:json][timeout:25]; ( {0}['{1}']({2},{3},{4},{5}); ); out;",
            requestObject,
            wayType,
            s.ToString(CultureInfo.InvariantCulture),
            w.ToString(CultureInfo.InvariantCulture),
            n.ToString(CultureInfo.InvariantCulture),
            e.ToString(CultureInfo.InvariantCulture)
        );
        return new FormUrlEncodedContent(new Dictionary<string, string> {{"data", newString}});
    }
}