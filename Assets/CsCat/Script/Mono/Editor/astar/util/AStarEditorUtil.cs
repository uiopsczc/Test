
using UnityEngine;
namespace CsCat
{
  public static class AStarEditorUtil
  {

    public static void DrawdObstacleTypeRect(AStarMonoBehaviour astarMonoBehaviour, int grid_x, int grid_y,
      int obstacleType)
    {
      Color obstacleTypeColor = AStarEditorConst.AStarObstacleType_Dict[obstacleType].color;
      Rect cell_rect = new Rect(0, 0, astarMonoBehaviour.cell_size.x, astarMonoBehaviour.cell_size.y);
      cell_rect.position = astarMonoBehaviour.GetPosition(grid_x, grid_y);
      DrawUtil.HandlesDrawSolidRectangleWithOutline(cell_rect, obstacleTypeColor, Color.green,
        astarMonoBehaviour.transform);
    }

    public static void DrawdTerrainTypeRect(AStarMonoBehaviour astarMonoBehaviour, int grid_x, int grid_y,
      int terrainType)
    {
      Rect cell_rect = new Rect(0, 0, astarMonoBehaviour.cell_size.x, astarMonoBehaviour.cell_size.y);
      cell_rect.position = astarMonoBehaviour.GetPosition(grid_x, grid_y);
      DrawUtil.HandlesDrawSolidRectangleWithOutline(cell_rect, default(Color), Color.green,
        astarMonoBehaviour.transform);
      if (terrainType != 0)
        DrawUtil.HandlesDrawString(astarMonoBehaviour.transform.TransformPoint(cell_rect.center),
          terrainType.ToString());
    }
  }
}
