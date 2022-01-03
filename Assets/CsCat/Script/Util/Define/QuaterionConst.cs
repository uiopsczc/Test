using UnityEngine;

namespace CsCat
{
	public static class QuaternionConst
	{
		public static Quaternion Max = new Quaternion(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue);

		public static Quaternion Min = new Quaternion(float.MinValue, float.MinValue, float.MinValue, float.MaxValue);

		public static Quaternion Default_Max = Max;

		public static Quaternion Default_Min = Min;

		public static Quaternion Default = Default_Max;
	}
}