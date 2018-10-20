using System;
using System.Collections.Generic;
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
        var response = await client.PostAsync("https://overpass-api.de/api/interpreter", GetAreaRequestBody("way", "highway", s,w,n,e));
        var responseString = await response.Content.ReadAsStringAsync();
        Debug.Log(responseString);
    }

    private FormUrlEncodedContent GetAreaRequestBody(string requestObject, string wayType, double s, double w, double n, double e)
    {
        var newString = string.Format(
            "[out:json][timeout:25]; ( {0}['{1}']({2},{3},{4},{5}); ); out;", 
            requestObject, 
            wayType, 
            s.ToString(System.Globalization.CultureInfo.InvariantCulture), 
            w.ToString(System.Globalization.CultureInfo.InvariantCulture), 
            n.ToString(System.Globalization.CultureInfo.InvariantCulture), 
            e.ToString(System.Globalization.CultureInfo.InvariantCulture)
            );
        return new FormUrlEncodedContent(new Dictionary<string, string>{{"data", newString}});
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}