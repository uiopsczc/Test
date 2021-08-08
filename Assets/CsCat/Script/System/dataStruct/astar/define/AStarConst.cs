using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class AStarConst
  {
    public static int Default_Terrain_Type_Value = 0;
    public static int Default_Obstacle_Type_Value = 0;
    public static int Default_Data_Value = AStarUtil.ToGridType(0, Default_Terrain_Type_Value, Default_Obstacle_Type_Value);

    public static List<AStarObstacleType> AStarObstacleType_List = new List<AStarObstacleType>()
    {
      new AStarObstacleType("������·", 0, default(Color)),
      new AStarObstacleType("�ڵ�", 1, new Color(0, 0, 1, 0.5f)),
      new AStarObstacleType("���ϰ�", 2, new Color(1, 1, 0, 0.5f)),
      new AStarObstacleType("���ϰ�", 3, new Color(0, 1, 1, 0.5f)),
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
      new AStarTerrainType("�հ׵���", 0,1),
      new AStarTerrainType("����1", 1,2),
      new AStarTerrainType("����2", 2,3),
      new AStarTerrainType("����3", 3, 4),
      new AStarTerrainType("����4", 4, 5),
      new AStarTerrainType("Invalid", 31, -1),
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