using UnityEngine;

namespace CsCat
{
	public static class Vector4Extension
	{
		public static string ToStringOrDefault(this Vector4 self, string toDefaultString = null,
			Vector4 defaultValue = default)
		{
			return ObjectUtil.Equals(self, defaultValue) ? toDefaultString : self.ToString();
		}
	}
}