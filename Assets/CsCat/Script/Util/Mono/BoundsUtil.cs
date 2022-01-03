using UnityEngine;

namespace CsCat
{
	public class BoundsUtil
	{
		/// <summary>
		/// 世界坐标（前面 左上角）
		/// </summary>
		/// <returns></returns>
		public static Vector3 FrontTopLeft(Bounds bounds)
		{
			return bounds.center + new Vector3(-bounds.size.x / 2, bounds.size.y / 2, bounds.size.z / 2);
		}

		/// <summary>
		/// 世界坐标（前面  右上角）
		/// </summary>
		/// <returns></returns>
		public static Vector3 FrontTopRight(Bounds bounds)
		{
			return bounds.center + new Vector3(bounds.size.x / 2, bounds.size.y / 2, bounds.size.z / 2);
		}

		/// <summary>
		/// 世界坐标（前面  左下角）
		/// </summary>
		/// <returns></returns>
		public static Vector3 FrontBottomLeft(Bounds bounds)
		{
			return bounds.center + new Vector3(-bounds.size.x / 2, -bounds.size.y / 2, bounds.size.z / 2);
		}

		/// <summary>
		/// 世界坐标（前面  左下角）
		/// </summary>
		/// <param name="bounds"></param>
		/// <returns></returns>
		public static Vector3 FrontBottomRight(Bounds bounds)
		{
			return bounds.center + new Vector3(bounds.size.x / 2, -bounds.size.y / 2, bounds.size.z / 2);
		}

		/// <summary>
		/// 世界坐标（后面 左上角）
		/// </summary>
		/// <param name="bounds"></param>
		/// <returns></returns>
		public static Vector3 BackTopLeft(Bounds bounds)
		{
			return bounds.center + new Vector3(-bounds.size.x / 2, bounds.size.y / 2, -bounds.size.z / 2);
		}

		/// <summary>
		/// 世界坐标（后面 右上角）
		/// </summary>
		/// <param name="bounds"></param>
		/// <returns></returns>
		public static Vector3 BackTopRight(Bounds bounds)
		{
			return bounds.center + new Vector3(bounds.size.x / 2, bounds.size.y / 2, -bounds.size.z / 2);
		}

		/// <summary>
		/// 世界坐标（后面 左下角）
		/// </summary>
		/// <param name="bounds"></param>
		/// <returns></returns>
		public static Vector3 BackBottomLeft(Bounds bounds)
		{
			return bounds.center + new Vector3(-bounds.size.x / 2, -bounds.size.y / 2, -bounds.size.z / 2);
		}

		/// <summary>
		/// 世界坐标（后面 右下角）
		/// </summary>
		/// <param name="bounds"></param>
		/// <returns></returns>
		public static Vector3 BackBottomRight(Bounds bounds)
		{
			return bounds.center + new Vector3(bounds.size.x / 2, -bounds.size.y / 2, -bounds.size.z / 2);
		}

		/// <summary>
		/// 所有的世界坐标
		/// </summary>
		/// <returns></returns>
		public static Vector3[] CornerPoints(Bounds bounds)
		{
			Vector3[] pts = new Vector3[8];

			pts[0] = bounds.FrontTopLeft();
			pts[1] = bounds.FrontTopRight();
			pts[2] = bounds.FrontBottomLeft();
			pts[3] = bounds.FrontBottomRight();

			pts[4] = bounds.BackTopLeft();
			pts[5] = bounds.BackTopRight();
			pts[6] = bounds.BackBottomLeft();
			pts[7] = bounds.BackBottomRight();

			return pts;
		}
	}
}