using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
	public class DrawDebugUtil
	{
		public static void Draw(List<Vector2> pointList, float h, Color color)
		{
			Vector3 tStart = new Vector3(pointList[0].x, h, pointList[0].y);
			for (int i = 1; i < pointList.Count; i++)
			{
				Vector3 tEnd = new Vector3(pointList[i].x, h, pointList[i].y);
				Debug.DrawLine(tStart, tEnd, color);
				tStart = tEnd;
			}
		}

		public static void Draw(List<Vector3> pointList, Color color)
		{
			Vector3 tStart = pointList[0];
			for (var i = 1; i < pointList.Count; i++)
			{
				Vector3 tEnd = pointList[i];
				Debug.DrawLine(tStart, tEnd, color);
				tStart = tEnd;
			}
		}

		public static void DrawNumber(string s, Vector3 center, Color color, float size = 1)
		{
			char[] chars = s.ToCharArray();
			for (int i = 0; i < chars.Length; i++)
			{
				var c = chars[i];

				Vector3 centerChar = Vector3.zero;
				int offset = i - chars.Length / 2;
				centerChar = chars.Length % 2 == 1
					? new Vector3((center.x + offset * size), center.y, center.z)
					: new Vector3((center.x + (offset + 0.5f) * size), center.y, center.z);

				DrawNumber(c, centerChar, color, size);
			}
		}


		public static void DrawNumber(char @char, Vector3 center, Color color, float size = 1)
		{
			NumberSquare square = new NumberSquare(center, size / 2);
			List<Vector3> points = square.GetPointList(@char);
			Draw(points, color);
		}

		public static void DrawCellIndex(List<Cell> cellList, float h, Color color, float numberSize = 1f)
		{
			foreach (var cell in cellList)
			{
				//cellIndex
				DrawDebugUtil.DrawNumber(cell.index.ToString(), new Vector3(cell.center.x, h, cell.center.y), color,
					numberSize);
			}
		}

		public static void DrawTrianglesSidesIndex(List<Triangle> triangleList, float h, Color color,
			float numberSize = 1f)
		{
			foreach (Triangle triangle in triangleList)
			{
				foreach (Line side in triangle.lineList)
				{
					Vector2 normal = side.GetNormal();
					Vector2 p = side.center;
					p = p - normal * 0.4f; //0.4：离法线方向*0.4的地方写index数字
					DrawDebugUtil.DrawNumber(triangle.lineList.IndexOf(side).ToString(), new Vector3(p.x, h, p.y),
						color,
						numberSize);
				}
			}
		}
	}
}