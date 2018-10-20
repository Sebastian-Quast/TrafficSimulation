
using System;
using UnityEngine;

namespace Osm.OsmDataObjects
{
    public class OsmRect
    {
        public Vector2 bottomLeft, bottomRight, topLeft, topRight, center;
        public float south, west, north, east;
        
        public OsmRect(float south, float west, float north, float east)
        {
            this.south = south;
            this.west = west;
            this.north = north;
            this.east = east;
            
            bottomLeft = new Vector2(west, south);
            bottomRight = new Vector2(east, south);
            topLeft = new Vector2(west, north);
            topRight = new Vector2(east, north);
            center = new Vector2(GetWidth() / 2, GetHeight() / 2);
        }

        public float GetWidth()
        {
            return Vector2.Distance(topLeft, topRight);
        }

        public float GetHeight()
        {
            return Vector2.Distance(topLeft, bottomLeft);
        }
    }
}