using System;
using UnityEngine;

namespace CsCat
{
	public partial class DrawUtil
	{
		public static bool Is_Debug_Enable = true;

		#region 画箭头 

		/// <summary>
		///   画箭头
		/// </summary>
		/// <param name="position"></param>
		/// <param name="direction"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugArrow(Vector3 position, Vector3 direction, float duration = 0,
			bool isDepthTest = true)
		{
			DebugArrow(position, direction, DrawUtilConst.DebugDraw_DefaultColor, duration, isDepthTest);
		}

		/// <summary>
		///   画箭头
		/// </summary>
		/// <param name="position"></param>
		/// <param name="direction"></param>
		/// <param name="color"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugArrow(Vector3 position, Vector3 direction, Color color, float duration = 0,
			bool isDepthTest = true)
		{
			if (!Is_Debug_Enable)
				return;
			Debug.DrawRay(position, direction, color, duration, isDepthTest);
			DebugCone(position + direction, direction.normalized, 15, color, duration, isDepthTest);
		}

		#endregion

		#region 画bounds 

		/// <summary>
		///   画bounds
		/// </summary>
		/// <param name="bounds"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugBounds(Bounds bounds, float duration = 0, bool isDepthTest = true)
		{
			DebugBounds(bounds, DrawUtilConst.DebugDraw_DefaultColor, duration, isDepthTest);
		}

		/// <summary>
		///   画bounds
		/// </summary>
		/// <param name="bounds"></param>
		/// <param name="color"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>isDepthTest
		public static void DebugBounds(Bounds bounds, Color color, float duration = 0, bool isDepthTest = true)
		{
			if (!Is_Debug_Enable) return;
			var cube = new Cube3D(bounds.center, bounds.size);
			cube.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, isDepthTest); });
		}

		#endregion

		#region 画Capsule 

		public static void DebugCapsule(Vector3 start, Vector3 end, float radius, float duration = 0,
			bool isDepthTest = true)
		{
			DebugCapsule(start, end, radius, DrawUtilConst.DebugDraw_DefaultColor, duration, isDepthTest);
		}

		public static void DebugCapsule(Vector3 start, Vector3 end, float radius, Color color, float duration = 0,
			bool isDepthTest = true)
		{
			if (!Is_Debug_Enable) return;

			DebugCylinder(start, end, radius, color, duration, isDepthTest);
			DebugSphere(start, radius, color, duration, isDepthTest);
			DebugSphere(end, radius, color, duration, isDepthTest);
		}

		#endregion

		#region 画圆 

		/// <summary>
		///   默认Matrix4x4.identity是向上
		/// </summary>
		/// <param name="position"></param>
		/// <param name="radius"></param>
		/// <param name="duration"></param>
		/// <param name="isDepthTest"></param>
		public static void DebugCircle(Vector3 position, float radius,
			float duration = 0, bool isDepthTest = true)
		{
			DebugCircle(position, radius, Matrix4x4.identity, DrawUtilConst.DebugDraw_DefaultColor, duration,
				isDepthTest);
		}

		/// <summary>
		///   默认Matrix4x4.identity是向上
		/// </summary>
		/// <param name="position"></param>
		/// <param name="radius"></param>
		/// <param name="mutiplyMatrix"></param>
		/// <param name="duration"></param>
		/// <param name="isDepthTest"></param>
		public static void DebugCircle(Vector3 position, float radius, Matrix4x4 mutiplyMatrix,
			float duration = 0, bool isDepthTest = true)
		{
			DebugCircle(position, radius, mutiplyMatrix, DrawUtilConst.DebugDraw_DefaultColor, duration,
				isDepthTest);
		}

		/// <summary>
		///   画圆
		///   默认Matrix4x4.identity是向上
		/// </summary>
		/// <param name="position"></param>
		/// <param name="radius"></param>
		/// <param name="mutiplyMatrix"></param>
		/// <param name="color"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugCircle(Vector3 position, float radius, Matrix4x4 mutiplyMatrix, Color color,
			float duration = 0, bool isDepthTest = true)
		{
			if (!Is_Debug_Enable) return;

			var circle = new Circle3D(position, radius, mutiplyMatrix);

			circle.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, isDepthTest); });
		}

		#endregion


		#region 画圆锥 

		public static void DebugCone(Vector3 endPoint, Vector3 forward, float duration = 0,
			bool isDepthTest = true)
		{
			DebugCone(endPoint, forward, 45, DrawUtilConst.DebugDraw_DefaultColor, duration, isDepthTest);
		}

		public static void DebugCone(Vector3 endPoint, Vector3 forward, float angle, float duration = 0,
			bool isDepthTest = true)
		{
			DebugCone(endPoint, forward, angle, DrawUtilConst.DebugDraw_DefaultColor, duration, isDepthTest);
		}

		/// <summary>
		/// </summary>
		/// <param name="endPoint">箭头的终点</param>
		/// <param name="forward">箭头的方向</param>
		/// <param name="angle">角度</param>
		/// <param name="color"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugCone(Vector3 endPoint, Vector3 forward, float angle, Color color, float duration = 0,
			bool isDepthTest = true)
		{
			if (!Is_Debug_Enable) return;

			var slerpRight = Vector3.Slerp(-forward, forward, 0.5f);
			var slerpUp = Vector3.Cross(forward, slerpRight).normalized * forward.magnitude;

			var angleSlerpRight = Vector3.Slerp(forward, slerpRight, angle / 90);
			var angleSlerpLeft = Vector3.Slerp(forward, -slerpRight, angle / 90);

			var angleSlerpUp = Vector3.Slerp(forward, slerpUp, angle / 90);
			var angleSlerpDown = Vector3.Slerp(forward, -slerpUp, angle / 90);

			var plane = new Plane(forward, endPoint - forward);
			var ray = new Ray(endPoint, -angleSlerpRight);
			plane.Raycast(ray, out var distance);

			Debug.DrawRay(endPoint, (-angleSlerpRight).normalized * distance, color, duration, isDepthTest);
			Debug.DrawRay(endPoint, (-angleSlerpLeft).normalized * distance, color, duration, isDepthTest);
			Debug.DrawRay(endPoint, (-angleSlerpUp).normalized * distance, color, duration, isDepthTest);
			Debug.DrawRay(endPoint, (-angleSlerpDown).normalized * distance, color, duration, isDepthTest);

			DebugCircle(endPoint - forward, (forward + -angleSlerpRight.normalized * distance).magnitude,
				Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, forward)), color, duration, isDepthTest);
			DebugCircle(endPoint - forward / 2, (forward + -angleSlerpRight.normalized * distance).magnitude / 2,
				Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, forward)), color, duration, isDepthTest);
		}

		#endregion


		#region 画圆柱体 

		/// <summary>
		///   画圆柱体
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="radius"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugCylinder(Vector3 start, Vector3 end, float radius, float duration = 0,
			bool isDepthTest = true)
		{
			DebugCylinder(start, end, radius, DrawUtilConst.DebugDraw_DefaultColor, duration, isDepthTest);
		}

		/// <summary>
		///   画圆柱体
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="color"></param>
		/// <param name="radius"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugCylinder(Vector3 start, Vector3 end, float radius, Color color, float duration = 0,
			bool isDepthTest = true)
		{
			if (!Is_Debug_Enable) return;

			var upDir = (end - start).normalized;

			var toUpMatrix = Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, upDir));
			//顶部的圆形
			DebugCircle(start, radius, toUpMatrix, color, duration, isDepthTest);
			////底部的圆形
			DebugCircle(end, radius, toUpMatrix, color, duration, isDepthTest);

			DebugLine(start, end, Color.yellow);

			//四个矩形
			var rectCenter = (start + end) / 2;
			var size = new Vector2(radius * 2, (start - end).magnitude);

			var toRightMatrix = Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0));

			DebugCenterRect(rectCenter, size,
				toUpMatrix * toRightMatrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 0)), color, duration,
				isDepthTest);
			DebugCenterRect(rectCenter, size,
				toUpMatrix * toRightMatrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 45)), color, duration,
				isDepthTest);
			DebugCenterRect(rectCenter, size,
				toUpMatrix * toRightMatrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), color, duration,
				isDepthTest);
			DebugCenterRect(rectCenter, size,
				toUpMatrix * toRightMatrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 135)), color, duration,
				isDepthTest);
		}

		#endregion


		#region 画LocalCube 

		/// <summary>
		///   画LocalCube
		/// </summary>
		/// <param name="transform"></param>
		/// <param name="offset"></param>
		/// <param name="size"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugLocalCube(Transform transform, Vector3 offset, Vector3 size, float duration = 0,
			bool isDepthTest = true)
		{
			DebugLocalCube(transform, offset, size, DrawUtilConst.DebugDraw_DefaultColor, duration, isDepthTest);
		}


		/// <summary>
		///   画LocalCube
		/// </summary>
		/// <param name="transform"></param>
		/// <param name="offset"></param>
		/// <param name="size"></param>
		/// <param name="color"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugLocalCube(Transform transform, Vector3 offset, Vector3 size, Color color,
			float duration = 0,
			bool isDepthTest = true)
		{
			if (!Is_Debug_Enable) return;
			var cube = new Cube3D(Vector3.zero, Vector3.one);
			cube.MultiplyMatrix(Matrix4x4.TRS(offset, Quaternion.identity, size));
			cube.PreMultiplyMatrix(transform.localToWorldMatrix);


			cube.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, isDepthTest); });
		}

		/// <summary>
		///   画LocalCube
		/// </summary>
		/// <param name="matrix"></param>
		/// <param name="offset"></param>
		/// <param name="size"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugLocalCube(Matrix4x4 matrix, Vector3 offset, Vector3 size, float duration = 0,
			bool isDepthTest = true)
		{
			DebugLocalCube(matrix, offset, size, DrawUtilConst.DebugDraw_DefaultColor, duration, isDepthTest);
		}

		/// <summary>
		///   画LocalCube
		/// </summary>
		/// <param name="matrix"></param>
		/// <param name="offset"></param>
		/// <param name="size"></param>
		/// <param name="color"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugLocalCube(Matrix4x4 matrix, Vector3 offset, Vector3 size, Color color,
			float duration = 0,
			bool isDepthTest = true)
		{
			if (!Is_Debug_Enable) return;

			var cube = new Cube3D(Vector3.zero, Vector3.one);
			cube.MultiplyMatrix(Matrix4x4.TRS(offset, Quaternion.identity, size));
			cube.PreMultiplyMatrix(matrix);
			cube.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, isDepthTest); });
		}

		#endregion

		#region 画Cube 

		public static void DebugCube(Vector3 min, Vector3 max, Color color,
			float duration = 0,
			bool isDepthTest = true)
		{
			if (!Is_Debug_Enable) return;
			var cube = new Cube3D((min + max) / 2, (max - min).Abs());
			cube.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, isDepthTest); });
		}

		#endregion

		#region 画点 

		/// <summary>
		///   画点
		/// </summary>
		/// <param name="position"></param>
		/// <param name="scale"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugPoint(Vector3 position, float scale = 1, float duration = 0, bool isDepthTest = true)
		{
			DebugPoint(position, DrawUtilConst.DebugDraw_DefaultColor, scale, duration, isDepthTest);
		}

		/// <summary>
		///   画点
		/// </summary>
		/// <param name="position"></param>
		/// <param name="color"></param>
		/// <param name="scale"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugPoint(Vector3 position, Color color, float scale = 1, float duration = 0,
			bool isDepthTest = true)
		{
			if (!Is_Debug_Enable) return;
			Debug.DrawRay(position + Vector3.up * (scale * 0.5f), -Vector3.up * scale, color, duration, isDepthTest);
			Debug.DrawRay(position + Vector3.right * (scale * 0.5f), -Vector3.right * scale, color, duration,
				isDepthTest);
			Debug.DrawRay(position + Vector3.forward * (scale * 0.5f), -Vector3.forward * scale, color, duration,
				isDepthTest);
		}

		#endregion

		#region 画球 

		/// <summary>
		///   画球
		/// </summary>
		/// <param name="position"></param>
		/// <param name="radius"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugSphere(Vector3 position, float radius, float duration = 0, bool isDepthTest = true)
		{
			DebugSphere(position, radius, DrawUtilConst.DebugDraw_DefaultColor, duration, isDepthTest);
		}

		/// <summary>
		///   画球
		/// </summary>
		/// <param name="position"></param>
		/// <param name="color"></param>
		/// <param name="radius"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugSphere(Vector3 position, float radius, Color color, float duration = 0,
			bool isDepthTest = true)
		{
			if (!Is_Debug_Enable) return;

			var sphere = new Sphere3D(position, radius);
			sphere.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, isDepthTest); });
		}

		#endregion

		#region 画线 

		public static void DebugLine(Vector3 start, Vector3 end, float duration = 0, bool isDepthTest = true)
		{
			DebugLine(start, end, DrawUtilConst.DebugDraw_DefaultColor, duration, isDepthTest);
		}

		/// <summary>
		///   画线
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="color"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		public static void DebugLine(Vector3 start, Vector3 end, Color color, float duration = 0,
			bool isDepthTest = true)
		{
			if (!Is_Debug_Enable) return;
			Debug.DrawLine(start, end, color, duration, isDepthTest);

			//LogCat.LogErrorFormat("{0} {1}", start, end);
		}

		#endregion

		#region 画多边形 

		public static void DebugPolygon(params Vector3[] points)
		{
			DebugPolygon(Matrix4x4.identity, DrawUtilConst.DebugDraw_DefaultColor, 0, true, points);
		}


		public static void DebugPolygon(bool isDepthTest, params Vector3[] points)
		{
			DebugPolygon(Matrix4x4.identity, DrawUtilConst.DebugDraw_DefaultColor, 0, isDepthTest, points);
		}

		public static void DebugPolygon(float duration, bool isDepthTest, params Vector3[] points)
		{
			DebugPolygon(Matrix4x4.identity, DrawUtilConst.DebugDraw_DefaultColor, duration, isDepthTest, points);
		}

		public static void DebugPolygon(Color color, float duration, bool isDepthTest, params Vector3[] points)
		{
			DebugPolygon(Matrix4x4.identity, color, duration, isDepthTest, points);
		}

		/// <summary>
		///   画多边形
		/// </summary>
		/// <param name="mutiplyMatrix"></param>
		/// <param name="color"></param>
		/// <param name="duration">持续时间</param>
		/// <param name="isDepthTest"></param>
		/// <param name="points"></param>
		public static void DebugPolygon(Matrix4x4 mutiplyMatrix, Color color, float duration, bool isDepthTest,
			params Vector3[] points)
		{
			if (!Is_Debug_Enable)
				return;


			var polygon = new Polygon3D(points);
			polygon.MultiplyMatrix(mutiplyMatrix);

			polygon.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, isDepthTest); });
		}

		#endregion

		#region 画矩形 

		/// <summary>
		///   默认Matrix4x4.identity是向上
		/// </summary>
		/// <param name="center"></param>
		/// <param name="size"></param>
		/// <param name="duration"></param>
		/// <param name="isDepthTest"></param>
		public static void DebugCenterRect(Vector3 center, Vector2 size, float duration = 0, bool isDepthTest = true)
		{
			DebugCenterRect(center, size, Matrix4x4.identity, DrawUtilConst.DebugDraw_DefaultColor, duration,
				isDepthTest);
		}

		/// <summary>
		///   默认Matrix4x4.identity是向上
		/// </summary>
		/// <param name="center"></param>
		/// <param name="size"></param>
		/// <param name="mutiplyMatrix"></param>
		/// <param name="duration"></param>
		/// <param name="isDepthTest"></param>
		public static void DebugCenterRect(Vector3 center, Vector2 size, Matrix4x4 mutiplyMatrix, float duration = 0,
			bool isDepthTest = true)
		{
			DebugCenterRect(center, size, mutiplyMatrix, DrawUtilConst.DebugDraw_DefaultColor, duration,
				isDepthTest);
		}

		/// <summary>
		///   默认Matrix4x4.identity是向上
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="duration"></param>
		/// <param name="isDepthTest"></param>
		public static void DebugCenterRect(Rect rect, float duration = 0, bool isDepthTest = true)
		{
			DebugCenterRect(rect.center.ToVector3(StringConst.String_x_0_y),
				rect.size.ToVector3(StringConst.String_x_0_y), Matrix4x4.identity,
				DrawUtilConst.DebugDraw_DefaultColor, duration, isDepthTest);
		}

		/// <summary>
		///   默认Matrix4x4.identity是向上
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="mutiplyMatrix"></param>
		/// <param name="duration"></param>
		/// <param name="isDepthTest"></param>
		public static void DebugCenterRect(Rect rect, Matrix4x4 mutiplyMatrix, float duration = 0,
			bool isDepthTest = true)
		{
			DebugCenterRect(rect.center.ToVector3(StringConst.String_x_0_y),
				rect.size.ToVector3(StringConst.String_x_0_y), mutiplyMatrix,
				DrawUtilConst.DebugDraw_DefaultColor, duration, isDepthTest);
		}


		/// <summary>
		///   画中心点在center的矩形
		///   默认Matrix4x4.identity是向上
		/// </summary>
		/// <param name="center"></param>
		/// <param name="size"></param>
		/// <param name="upDir"></param>
		/// <param name="mutiplyMatrix"></param>
		/// <param name="color"></param>
		/// <param name="duration"></param>
		/// <param name="isDepthTest"></param>
		public static void DebugCenterRect(Vector3 center, Vector2 size, Matrix4x4 mutiplyMatrix, Color color,
			float duration = 0, bool isDepthTest = true)
		{
			var rectangle = new Rectangle3D(center, size, mutiplyMatrix);
			rectangle.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, isDepthTest); });
		}

		#endregion
	}
}