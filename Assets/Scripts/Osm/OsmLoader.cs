using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Osm.OsmDataObjects;
using Osm.OsmResponseObjects;
using UnityEngine;

namespace Osm
{
    public class OsmLoader : MonoBehaviour
    {
        private readonly HttpClient client = new HttpClient();

        public float south = 50.80802294398169f;
        public float west = 7.2391319274902335f;
        public float north = 50.82605366331441f;
        public float east = 7.284407615661621f;

        public async Task<OsmWorldData> LoadData()
        {
            return await GetData(south, west, north, east);
        }

        private async Task<OsmWorldData> GetData(float s, float w, float n, float e)
        {
            var responseMessage = await client.PostAsync("https://overpass-api.de/api/interpreter",
                GetAreaRequestBody("way", "highway", s, w, n, e));
            return await ParseResponse(responseMessage, s, w, n, e);
        }

        private async Task<OsmWorldData> ParseResponse(HttpResponseMessage response, float s, float w, float n, float e)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var osmResponse = JsonUtility.FromJson<OsmResponse>(responseString);
            return ToDataSet(osmResponse, s, w, n, e);
        }

        private OsmWorldData ToDataSet(OsmResponse response, float s, float w, float n, float e)
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
        
            return new OsmWorldData(response.version, response.generator, new OsmRect(s, w, n, e), response.osm3s, ways, nodes, others);
        }

        private FormUrlEncodedContent GetAreaRequestBody(string requestObject, string wayType, float s, float w, float n,
            float e)
        {
            var newString = string.Format(
                "[out:json][timeout:25]; ( {0}['{1}']({2},{3},{4},{5}); ); out body; >; out skel qt;",
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
}