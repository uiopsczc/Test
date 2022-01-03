using UnityEngine;

namespace CsCat
{
	public class RectUtil
	{
		/// <summary>
		/// 缩放Rect
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="scaleFactor"></param>
		/// <param name="pivotPointOffset">中心点偏移，默认(0,0)是在中心</param>
		/// <returns></returns>
		public static Rect ScaleBy(Rect rect, float scaleFactor, Vector2 pivotPointOffset = default)
		{
			Rect result = rect;
			result.x -= pivotPointOffset.x;
			result.y -= pivotPointOffset.y;
			result.xMin *= scaleFactor;
			result.xMax *= scaleFactor;
			result.yMin *= scaleFactor;
			result.yMax *= scaleFactor;
			result.x += pivotPointOffset.x;
			result.y += pivotPointOffset.y;
			return result;
		}
	}
}