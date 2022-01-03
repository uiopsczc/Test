using System;

namespace CsCat
{
	public static class ShortExtension
	{
		#region bytes

		/// <summary>
		///   将数字转化为bytes
		/// </summary>
		public static byte[] ToBytes(this short self, bool isNetOrder = false)
		{
			return ByteUtil.ToBytes(self & 0xFFFF, 2, isNetOrder);
		}

		#endregion

		public static short Minimum(this short self, short minimum)
		{
			return Math.Max(self, minimum);
		}

		public static short Maximum(this short self, short maximum)
		{
			return Math.Min(self, maximum);
		}
	}
}