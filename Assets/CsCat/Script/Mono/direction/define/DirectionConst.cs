using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public static class DirectionConst
  {
	  public const string LeftTop = "LeftTop";
	  public const string Top = "Top";
	  public const string RightTop = "RightTop";
	  public const string Left = "Left";
	  public const string Center = "Center";
	  public const string Right = "Right";
	  public const string LeftBottom = "LeftBottom";
	  public const string Bottom = "Bottom";
	  public const string RightBottom = "RightBottom";

	  public static DirectionInfo LeftTopDirectionInfo => GetDirectionInfo(LeftTop);
	  public static DirectionInfo TopDirectionInfo => GetDirectionInfo(Top);
	  public static DirectionInfo RightTopDirectionInfo => GetDirectionInfo(RightTop);
	  public static DirectionInfo LeftDirectionInfo => GetDirectionInfo(Left);
	  public static DirectionInfo CenterDirectionInfo => GetDirectionInfo(Center);
	  public static DirectionInfo RightDirectionInfo => GetDirectionInfo(Right);
	  public static DirectionInfo LeftBottomDirectionInfo => GetDirectionInfo(LeftBottom);
	  public static DirectionInfo BottomDirectionInfo => GetDirectionInfo(Bottom);
	  public static DirectionInfo RightBottomDirectionInfo => GetDirectionInfo(RightBottom);

		private static Dictionary<string, DirectionInfo> direction_dict = new Dictionary<string, DirectionInfo>()
    {
      {LeftBottom, new DirectionInfo(-1, -1, LeftBottom)},
      {Bottom, new DirectionInfo(0, -1, Bottom)},
      {RightBottom, new DirectionInfo(1, -1, RightBottom)},

      {Left, new DirectionInfo(-1, 0, Left)},
      {Center, new DirectionInfo(0, 0, Center)},
      {Right, new DirectionInfo(1, 0, Right)},

      {LeftTop, new DirectionInfo(-1, 1, LeftTop)},
      {Top, new DirectionInfo(0, 1, Top)},
      {RightTop, new DirectionInfo(1, 1, RightTop)},
    };

    private static Dictionary<Vector2Int, DirectionInfo> _direction_dict2;

    public static Dictionary<Vector2Int, DirectionInfo> direction_dict2
    {
      get
      {
        if (_direction_dict2 == null)
        {
          _direction_dict2 = new Dictionary<Vector2Int, DirectionInfo>();
          foreach (var directionInfo in direction_dict.Values)
            _direction_dict2[new Vector2Int(directionInfo.x, directionInfo.y)] = directionInfo;
        }

        return _direction_dict2;
      }
    }

    public static DirectionInfo GetDirectionInfo(int x, int y)
    {
      return direction_dict2[new Vector2Int(x, y)];
    }

    public static DirectionInfo GetDirectionInfo(string name)
    {
      return direction_dict[name.ToLower()];
    }
  }
}




