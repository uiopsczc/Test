
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public static class AStarEditorConst
  {
    public static List<AStarObstacleType> AStarObstacleType_List = new List<AStarObstacleType>()
    {
      new AStarObstacleType("正常道路", 0, default(Color)),
      new AStarObstacleType("遮挡", 1, new Color(0, 0, 1, 0.5f)),
      new AStarObstacleType("低障碍", 2, new Color(1, 1, 0, 0.5f)),
      new AStarObstacleType("高障碍", 3, new Color(0, 1, 1, 0.5f)),
      new AStarObstacleType("Invalid", 4, new Color(0, 0, 0, 0.5f)),
    };

    private static Dictionary<int, AStarObstacleType> _AStarObstacleType_Dict;

    public static Dictionary<int, AStarObstacleType> AStarObstacleType_Dict
    {
      get
      {
        if (_AStarObstacleType_Dict == null)
        {
          _AStarObstacleType_Dict = new Dictionary<int, AStarObstacleType>();
          foreach (var astarObstacleType in AStarObstacleType_List)
            _AStarObstacleType_Dict[astarObstacleType.value] = astarObstacleType;
        }

        return _AStarObstacleType_Dict;
      }
    }


    public static List<AStarTerrainType> AStarTerrainType_List = new List<AStarTerrainType>()
    {
      new AStarTerrainType("空白地形", 0),
      new AStarTerrainType("地形1", 1),
      new AStarTerrainType("地形2", 2),
      new AStarTerrainType("地形3", 3),
      new AStarTerrainType("地形4", 4),
      new AStarTerrainType("Invalid", 31),
    };

    private static Dictionary<int, AStarTerrainType> _AStarTerrainType_Dict;

    public static Dictionary<int, AStarTerrainType> AStarTerrainType_Dict
    {
      get
      {
        if (_AStarTerrainType_Dict == null)
        {
          _AStarTerrainType_Dict = new Dictionary<int, AStarTerrainType>();
          foreach (var astarTerrainType in AStarTerrainType_List)
            _AStarTerrainType_Dict[astarTerrainType.value] = astarTerrainType;
        }

        return _AStarTerrainType_Dict;
      }
    }

  }
}
