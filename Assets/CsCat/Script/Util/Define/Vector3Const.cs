using UnityEngine;

namespace CsCat
{
	public static class Vector3Const
	{
		public static Vector3 Max = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

		public static Vector3 Min = new Vector3(float.MinValue, float.MinValue, float.MinValue);

		public static Vector3 Default_Max = Max;

		public static Vector3 Default_Min = Min;

		public static Vector3 Default = Default_Max;

		public static Vector3 Half = new Vector3(0.5f, 0.5f, 0.5f);
	}
}