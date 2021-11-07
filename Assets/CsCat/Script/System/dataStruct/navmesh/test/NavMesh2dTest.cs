using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
    [ExecuteInEditMode]
    public class NavMesh2dTest : MonoBehaviour
    {
        public static GameObject t;
        public List<Rectangle> rectangles;
        public List<Polygon> polygons;
        public List<Polygon> innerPolygons;
        public Polygon outerPolygon;
        public List<Line> overLapInnerLines;
        public List<Vector2> overLapInnerPoints;
        public NavMesh2d navMesh;


        private List<Triangle> triangles;
        private List<Cell> cellList;


        private Polygon GetOuterPolygon()
        {
            var outerPolygons = polygons[0];
            return outerPolygons;
        }

        private List<Polygon> GetInnerPolygons()
        {
            var innerPolygons = new List<Polygon>();
            for (var i = 1; i < polygons.Count; i++)
                innerPolygons.Add(polygons[i]);
            return innerPolygons;
        }

        /// <summary>
        ///   innerPolygons中与OuterPolygon重叠的边
        /// </summary>
        /// <returns></returns>
        private List<Line> GetOverLapInnerLines()
        {
            var ret = new List<Line>();
            for (var i = 0; i < innerPolygons.Count; i++)
            {
                var innerPolygon = innerPolygons[i];
                for (var j = 0; j < innerPolygon.lineList.Count; j++)
                {
                    var innnerLine = innerPolygon.lineList[j];
                    for (var k = 0; k < outerPolygon.lineList.Count; k++)
                    {
                        var outerLine = outerPolygon.lineList[k];
                        if (outerLine.Contains(innnerLine))
                        {
                            ret.Add(innnerLine);
                            break;
                        }
                    }
                }
            }

            return ret;
        }

        private List<Vector2> GetOverLapInnerPoints()
        {
            var ret = new List<Vector2>();
            var lines = GetOverLapInnerLines();
            for (var i = 0; i < lines.Count; i++)
            {
                var line1 = lines[i];
                for (var j = 0; j < lines.Count; j++)
                {
                    var line2 = lines[j];
                    if (line1.Equals(line2))
                        continue;
                    if (line1.pointA == line2.pointA || line1.pointA == line2.pointB)
                        ret.Add(line1.pointA);
                    if (line1.pointB == line2.pointA || line1.pointB == line2.pointB)
                        ret.Add(line1.pointB);
                }
            }

            return ret;
        }

        private List<Polygon> GetDelaunayPolygons()
        {
            var ret = new List<Polygon>();
            outerPolygon.SetClockWise(true);
            ret.Add(outerPolygon);
            for (var i = 0; i < innerPolygons.Count; i++)
            {
                var p = innerPolygons[i];
                p.SetClockWise(false);
                ret.Add(p);
            }

            return ret;
        }


        private List<Rectangle> CreateRectangles()
        {
            rectangles = new List<Rectangle>();

            var r0 = new Rectangle(new Vector2(5, 5), new Vector2(10, 10));

            //Rectangle r1 = new Rectangle(new Vector2(5, 5), 2, 2);
            var r1 = new Rectangle(new Vector2(5, 5), new Vector2(2, 2));
            r1.SetClockWise(false);

            //Rectangle r2 = new Rectangle(new Vector2(7, 7f), 2, 2);
            var r2 = new Rectangle(new Vector2(1, 3), new Vector2(2, 2));
            r2.SetClockWise(false);

            //Rectangle r3 = new Rectangle(new Vector2(7, 3), 2, 2);
            var r3 = new Rectangle(new Vector2(8, 3), new Vector2(2, 2));
            r3.SetClockWise(false);

            // Rectangle r4 = new Rectangle(new Vector2(1, 1), 1, 1);
            var r4 = new Rectangle(new Vector2(4, 1), new Vector2(1, 1));
            r4.SetClockWise(false);

            rectangles.Add(r0);
            rectangles.Add(r1);
            rectangles.Add(r2);
            rectangles.Add(r3);
            rectangles.Add(r4);

            return rectangles;
        }


        private void Start()
        {
            t = Resources.Load("NavMeshPoint") as GameObject;
            var delaunay = new Delaunay();

            rectangles = CreateRectangles();


            polygons = rectangles.ToList<Polygon>();

            innerPolygons = GetInnerPolygons();
            outerPolygon = GetOuterPolygon();


            ClipperUtil.Cut(outerPolygon, innerPolygons);
            ClipperUtil.Union(innerPolygons);
            PolygonUtil.Systemize(polygons);


            overLapInnerLines = GetOverLapInnerLines();
            overLapInnerPoints = GetOverLapInnerPoints();


            polygons = GetDelaunayPolygons();
            triangles = delaunay.CreateDelaunay(polygons);
            cellList = delaunay.GetCells();
            navMesh = new NavMesh2d(cellList);
        }


        // Update is called once per frame
        private void Update()
        {
            //DrawDebugUtil.Draw(this.innerPolygons, 0.1f, Color.green);
            //DrawDebugUtil.Draw(rectangles, 0.2f, Color.yellow);
            //DrawDebugUtil.Draw(triangles, 0f, Color.red);
            //DrawUtil.DrawCellIndex(this.cellList, 0f,Color.white,0.4f);
            //DrawUtil.DrawTrianglesSidesIndex(this.triangles, 0,Color.cyan,0.4f);

            NavMesh2dInputHandler.instance.Update();

            DoNavMesh();
        }

        private void DoNavMesh()
        {
            var points = NavMesh2dInputHandler.instance.pointList;
            if (points.Count == 2)
            {
                var startPoint = new Vector2(points[0].x, points[0].z);
                var endPoint = new Vector2(points[1].x, points[1].z);
                var centers = navMesh.FindPath(startPoint, endPoint);
                DrawDebugUtil.Draw(centers, 0, Color.black);
            }
        }
    }
}