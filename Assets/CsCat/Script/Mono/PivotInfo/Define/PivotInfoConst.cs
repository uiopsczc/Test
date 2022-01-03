using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public static class PivotInfoConst
	{
		public static PivotInfo LeftTopPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.String_LeftTop);
		public static PivotInfo TopPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.String_Top);
		public static PivotInfo RightTopPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.String_RightTop);
		public static PivotInfo LeftPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.String_Left);
		public static PivotInfo CenterPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.String_Center);
		public static PivotInfo RightPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.String_Right);
		public static PivotInfo LeftBottomPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.String_LeftBottom);
		public static PivotInfo BottomPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.String_Bottom);
		public static PivotInfo RightBottomPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.String_RightBottom);

		public static readonly Dictionary<string, PivotInfo> PivotInfoDict = new Dictionary<string, PivotInfo>()
		{
			{StringConst.String_LeftBottom, new PivotInfo(0, 0, StringConst.String_LeftBottom)},
			{StringConst.String_Bottom, new PivotInfo(0.5f, 0, StringConst.String_Bottom)},
			{StringConst.String_RightBottom, new PivotInfo(1, 0, StringConst.String_RightBottom)},

			{StringConst.String_Left, new PivotInfo(0, 0.5f, StringConst.String_Left)},
			{StringConst.String_Center, new PivotInfo(0.5f, 0.5f, StringConst.String_Center)},
			{StringConst.String_Right, new PivotInfo(1, 0.5f, StringConst.String_Right)},

			{StringConst.String_LeftTop, new PivotInfo(0, 1, StringConst.String_LeftTop)},
			{StringConst.String_Top, new PivotInfo(0.5f, 1, StringConst.String_Top)},
			{StringConst.String_RightTop, new PivotInfo(1, 1, StringConst.String_RightTop)},
		};

		private static Dictionary<Vector2, PivotInfo> _PivotInfoDict2;

		public static Dictionary<Vector2, PivotInfo> PivotInfoDict2
		{
			get
			{
				if (_PivotInfoDict2 != null) return _PivotInfoDict2;
				_PivotInfoDict2 = new Dictionary<Vector2, PivotInfo>();
				foreach (var pivotInfo in PivotInfoDict.Values)
					_PivotInfoDict2[new Vector2(pivotInfo.x, pivotInfo.y)] = pivotInfo;

				return _PivotInfoDict2;
			}
		}
	}
}