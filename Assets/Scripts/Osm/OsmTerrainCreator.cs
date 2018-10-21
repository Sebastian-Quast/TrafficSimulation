using Osm.OsmDataObjects;
using UnityEngine;
using UnityEngine.Networking.Types;

namespace Osm
{
    [RequireComponent(typeof(OsmLoader))]
    public class OsmTerrainCreator : MonoBehaviour
    {

        public int scaleFactor = 1000;
        public float nodeSize = 0.2f;
        
        async void Start()
        {
            var loader = GetComponent<OsmLoader>();

            var worldData = await loader.LoadData();

            CreateTerrain(worldData);
        }


        private void CreateTerrain(OsmWorldData worldData)
        {
            var worldObject = new GameObject("World");
//            worldData.nodes.ForEach(node =>
//            {
//                var n = GameObject.CreatePrimitive(PrimitiveType.Cube);
//                n.name = node.id.ToString();
//                n.transform.localScale = new Vector3(nodeSize, nodeSize, nodeSize);
//                n.transform.position = new Vector3(node.lon * scaleFactor, 0, node.lat * scaleFactor);
//                n.transform.parent = worldObject.transform;
//            });
            
            worldData.ways.ForEach(way =>
            {
                var nodesIds = way.nodes;
                if(nodesIds.Length < 2) return;
                for (var i = 0; i < nodesIds.Length - 1; i++)
                {
                    var startNode = worldData.nodes.Find(node => node.id == nodesIds[i]);
                    var endNode = worldData.nodes.Find(node => node.id == nodesIds[i + 1]);
                    var start = new Vector3(startNode.lon * scaleFactor, startNode.lat * scaleFactor);
                    var end = new Vector3(endNode.lon * scaleFactor, endNode.lat * scaleFactor);
                    DrawLine(start, end, Color.red);
                }
            });
        }
        
        

        void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            var myLine = new GameObject();
            myLine.transform.position = start;
            myLine.AddComponent<LineRenderer>();
            var lr = myLine.GetComponent<LineRenderer>();
            lr.SetColors(color, color);
            lr.SetWidth(0.1f, 0.1f);
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            //Destroy(myLine, duration);
        }


    }
}