using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		public static void GUIShowPointList(int left, int bottom, int right, int top, List<Vector2Int> pointList)
		{
			float offsetX = 210;
			float offsetY = 100;
			float rectWidth = 80;
			float rectHeight = 80;
			GUIStyle fontStyle = new GUIStyle();
			fontStyle.fontSize = 30; //字体大小
			for (int i = left; i <= right; i++)
			{
				for (int j = top; j >= bottom; j--)
				{
					Vector2Int v = new Vector2Int(i, j);
					string x = v.x.ToString();
					string y = v.y.ToString();

					Rect rect = new Rect(offsetX + (i - left) * rectWidth, offsetY + (top - j) * rectHeight, rectWidth,
					  rectHeight);

					fontStyle.normal.textColor = pointList.Contains(v) ? Color.red : Color.white;
					GUI.Label(rect, string.Format("[{0},{1}]", x, y), fontStyle);
				}
			}
		}

		public static void GUIShowPointList(int left, int bottom, int right, int top, Vector2Int point)
		{
			GUIShowPointList(left, bottom, right, top, new List<Vector2Int>() { point });
		}



	}
}