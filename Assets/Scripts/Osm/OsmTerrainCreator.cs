using System;
using System.Linq;
using System.Threading.Tasks;
using Osm.OsmDataObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Osm
{
    [RequireComponent(typeof(OsmLoader))]
    public class OsmTerrainCreator : MonoBehaviour
    {
        async void Start()
        {
            var loader = GetComponent<OsmLoader>();

            var worldData = await loader.LoadData();

            CreateTerrain(worldData);

            // Pole for orientation
//            var pole = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
//            pole.name = "Pole";
//            pole.transform.localScale = new Vector3(1, 10, 1);
//            pole.transform.position = Vector3.zero;
//            var poleRenderer = pole.GetComponent<Renderer>();
//            poleRenderer.material.SetColor("_Color", Color.green);
//            poleRenderer.material.shader = Shader.Find("_Color");
//            Instantiate(pole);
            // End Pole
        }


        private void CreateTerrain(OsmWorldData worldData)
        {
            var worldObject = new GameObject("World");
            worldData.nodes.ForEach(node =>
            {
                var n = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                n.name = node.id.ToString();
                n.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                n.transform.position = new Vector3(node.lon * 1000, 0, node.lat * 1000);
                n.transform.parent = worldObject.transform;
            });
            
//            var mesh = new Mesh();
//            mesh.name = "World";
//            mesh.vertices = new[]{new Vector3(-1, 0, -1), new Vector3(-1, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 0, -1)};
//            mesh.uv = new[]{new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0)};
//            mesh.triangles = new[] {0, 1, 2, 0, 2, 3};
//            mesh.RecalculateNormals();
//            var obj = new GameObject("New_Plane_Fom_Script");
//            obj.mes

//            var terrainData = new TerrainData
//            {
//                size = new Vector3(worldData.rect.GetWidth(), 600, worldData.rect.GetHeight()), heightmapResolution = 512, baseMapResolution = 1024
//            };
//            terrainData.SetDetailResolution(1024, 1);
//
//            var heightmapWidth = terrainData.heightmapWidth;
//            var heightmapHeight = terrainData.heightmapHeight;
//
//            var terrainGameObject = new GameObject("World");
//            var terrainCollider = terrainGameObject.AddComponent<TerrainCollider>();
//            var terrain = terrainGameObject.AddComponent<Terrain>();
//            terrainCollider.terrainData = terrainData;
//            terrain.terrainData = terrainData;
//            terrain.transform.position = Vector3.zero - new Vector3(worldData.rect.center.x, 0, worldData.rect.center.y);
//
//            return terrainGameObject;
            //Instantiate(terrain);
        }
    }
}