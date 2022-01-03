using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public static class DirectionInfoConst
	{
		public static DirectionInfo LeftTopDirectionInfo =>
			DirectionInfoUtil.GetDirectionInfo(StringConst.String_LeftTop);

		public static DirectionInfo TopDirectionInfo => DirectionInfoUtil.GetDirectionInfo(StringConst.String_Top);

		public static DirectionInfo RightTopDirectionInfo =>
			DirectionInfoUtil.GetDirectionInfo(StringConst.String_RightTop);

		public static DirectionInfo LeftDirectionInfo => DirectionInfoUtil.GetDirectionInfo(StringConst.String_Left);

		public static DirectionInfo CenterDirectionInfo =>
			DirectionInfoUtil.GetDirectionInfo(StringConst.String_Center);

		public static DirectionInfo RightDirectionInfo => DirectionInfoUtil.GetDirectionInfo(StringConst.String_Right);

		public static DirectionInfo LeftBottomDirectionInfo =>
			DirectionInfoUtil.GetDirectionInfo(StringConst.String_LeftBottom);

		public static DirectionInfo BottomDirectionInfo =>
			DirectionInfoUtil.GetDirectionInfo(StringConst.String_Bottom);

		public static DirectionInfo RightBottomDirectionInfo =>
			DirectionInfoUtil.GetDirectionInfo(StringConst.String_RightBottom);

		public static readonly Dictionary<string, DirectionInfo> DirectionInfoDict =
			new Dictionary<string, DirectionInfo>()
			{
				{StringConst.String_LeftBottom, new DirectionInfo(-1, -1, StringConst.String_LeftBottom)},
				{StringConst.String_Bottom, new DirectionInfo(0, -1, StringConst.String_Bottom)},
				{StringConst.String_RightBottom, new DirectionInfo(1, -1, StringConst.String_RightBottom)},

				{StringConst.String_Left, new DirectionInfo(-1, 0, StringConst.String_Left)},
				{StringConst.String_Center, new DirectionInfo(0, 0, StringConst.String_Center)},
				{StringConst.String_Right, new DirectionInfo(1, 0, StringConst.String_Right)},

				{StringConst.String_LeftTop, new DirectionInfo(-1, 1, StringConst.String_LeftTop)},
				{StringConst.String_Top, new DirectionInfo(0, 1, StringConst.String_Top)},
				{StringConst.String_RightTop, new DirectionInfo(1, 1, StringConst.String_RightTop)},
			};

		private static Dictionary<Vector2Int, DirectionInfo> _DirectionInfoDict2;

		public static Dictionary<Vector2Int, DirectionInfo> DirectionInfoDict2
		{
			get
			{
				if (_DirectionInfoDict2 != null) return _DirectionInfoDict2;
				_DirectionInfoDict2 = new Dictionary<Vector2Int, DirectionInfo>();
				foreach (var directionInfo in DirectionInfoDict.Values)
					_DirectionInfoDict2[new Vector2Int(directionInfo.x, directionInfo.y)] = directionInfo;

				return _DirectionInfoDict2;
			}
		}
	}
}