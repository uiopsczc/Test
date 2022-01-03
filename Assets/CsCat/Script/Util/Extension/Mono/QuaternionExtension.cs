using UnityEngine;

namespace CsCat
{
	public static class QuaternionExtension
	{
		public static bool IsDefault(this Quaternion self, bool isMin = false)
		{
			return isMin ? self == QuaternionConst.Default_Min : self == QuaternionConst.Default_Max;
		}

		public static Quaternion Inverse(this Quaternion self)
		{
			return Quaternion.Inverse(self);
		}

		public static Vector3 Forward(this Quaternion self)
		{
			return self * Vector3.forward;
		}

		public static Vector3 Back(this Quaternion self)
		{
			return self * Vector3.back;
		}

		public static Vector3 Up(this Quaternion self)
		{
			return self * Vector3.up;
		}

		public static Vector3 Down(this Quaternion self)
		{
			return self * Vector3.down;
		}

		public static Vector3 Left(this Quaternion self)
		{
			return self * Vector3.left;
		}

		public static Vector3 Right(this Quaternion self)
		{
			return self * Vector3.right;
		}

		public static bool IsZero(this Quaternion self)
		{
			return self.x == 0 && self.y == 0 && self.z == 0;
		}

		public static Quaternion GetNotZero(this Quaternion self, Quaternion? defaultValue = null)
		{
			defaultValue = defaultValue ?? Quaternion.identity;
			return self.IsZero() ? defaultValue.Value : self;
		}
	}
}