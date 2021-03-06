﻿using System.Collections.Generic;
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
        public float south = 50.9198094295233f;
        public float west = 7.198963165283203f;
        public float north = 50.97464290902178f;
        public float east = 7.38006591796875f;

        public async Task<OsmWorldData> LoadData()
        {
            return await GetData(south, west, north, east);
        }

        private async Task<OsmWorldData> GetData(float s, float w, float n, float e)
        {
            var responseMessage = await client.PostAsync(
                "https://overpass-api.de/api/interpreter",
                GetAreaRequestBody("way", "highway", s, w, n, e)
            );
            return await ParseResponse(responseMessage, s, w, n, e);
        }

        private async Task<OsmWorldData> ParseResponse(HttpResponseMessage response, float s, float w, float n, float e)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var osmResponse = JsonUtility.FromJson<OsmResponse>(responseString);
            return ToWorld(osmResponse, s, w, n, e);
        }

        private OsmWorldData ToWorld(OsmResponse response, float s, float w, float n, float e)
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

            return new OsmWorldData(response.version, response.generator, new OsmRect(s, w, n, e), response.osm3s, ways,
                nodes, others);
        }

        private FormUrlEncodedContent GetAreaRequestBody(string requestObject, string wayType, float s, float w,
            float n,
            float e)
        {
            var newString =
                $"[out:json][timeout:25];" +
                $" ( {requestObject}['{wayType}'](" +
                $"{s.ToString(CultureInfo.InvariantCulture)}," +
                $"{w.ToString(CultureInfo.InvariantCulture)}," +
                $"{n.ToString(CultureInfo.InvariantCulture)}," +
                $"{e.ToString(CultureInfo.InvariantCulture)}); " +
                $"); " +
                $"out body; >; out skel qt;";
            return new FormUrlEncodedContent(new Dictionary<string, string> {{"data", newString}});
        }
    }
}