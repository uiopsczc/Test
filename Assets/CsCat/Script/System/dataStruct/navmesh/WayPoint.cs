using UnityEngine;

namespace CsCat
{
    public class WayPoint
    {
        public Vector2 position;
        public Cell cell;


        public WayPoint(Cell cell, Vector2 position)
        {
            this.cell = cell;
            this.position = position;
        }
    }
}