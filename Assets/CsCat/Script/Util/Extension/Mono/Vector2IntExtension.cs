using System;
using UnityEngine;

namespace CsCat
{
	public static class Vector2IntExtension
	{
		public static bool IsDefault(this Vector2Int self, bool isMin = false)
		{
			return isMin ? self == Vector2IntConst.Default_Min : self == Vector2IntConst.Default_Max;
		}

		public static string ToStringOrDefault(this Vector2Int self, string toDefaultString = null,
			Vector2Int defaultValue = default)
		{
			return ObjectUtil.Equals(self, defaultValue) ? toDefaultString : self.ToString();
		}

		public static Vector2Int Abs(this Vector2Int self)
		{
			return new Vector2Int(Math.Abs(self.x), Math.Abs(self.y));
		}


		public static bool IsZero(this Vector2Int self)
		{
			return self.Equals(Vector2Int.zero);
		}


		public static bool IsOne(this Vector2Int self)
		{
			return self.Equals(Vector2Int.one);
		}
	}
}