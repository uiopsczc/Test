using UnityEngine;

namespace CsCat
{
	public static class Vector2IntConst
	{
		public static Vector2Int Max = new Vector2Int(int.MaxValue, int.MaxValue);

		public static Vector2Int Min = new Vector2Int(int.MinValue, int.MinValue);

		public static Vector2Int Default_Max = Max;

		public static Vector2Int Default_Min = Min;

		public static Vector2Int Default = Default_Max;

		public static Vector2Int LeftTop = new Vector2Int(-1, 1);
		public static Vector2Int Top = new Vector2Int(0, 1);
		public static Vector2Int RightTop = new Vector2Int(1, 1);
		public static Vector2Int Left = new Vector2Int(-1, 0);
		public static Vector2Int Center = new Vector2Int(0, 0);
		public static Vector2Int Right = new Vector2Int(0, 1);
		public static Vector2Int LeftBottom = new Vector2Int(-1, -1);
		public static Vector2Int Bottom = new Vector2Int(0, -1);
		public static Vector2Int RightBottom = new Vector2Int(1, -1);
	}
}