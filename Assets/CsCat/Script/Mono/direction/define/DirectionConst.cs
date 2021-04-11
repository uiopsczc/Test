using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public static class DirectionConst
  {
    private static Dictionary<string, DirectionInfo> direction_dict = new Dictionary<string, DirectionInfo>()
    {
      {"left_bottom", new DirectionInfo(-1, -1, "left_bottom")},
      {"bottom", new DirectionInfo(0, -1, "bottom")},
      {"right_bottom", new DirectionInfo(1, -1, "right_bottom")},

      {"left", new DirectionInfo(-1, 0, "left")},
      {"center", new DirectionInfo(0, 0, "center")},
      {"right", new DirectionInfo(1, 0, "right")},

      {"left_top", new DirectionInfo(-1, 1, "left_top")},
      {"top", new DirectionInfo(0, 1, "top")},
      {"right_top", new DirectionInfo(1, 1, "right_top")},
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




