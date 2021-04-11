using UnityEditor;

namespace CsCat
{
  public class AStarBrush
  {
    public AStarMonoBehaviour astarMonoBehaviour;


    public void DoPaintPressed(int mouse_grid_x, int mouse_grid_y, int value)
    {
      Paint(mouse_grid_x, mouse_grid_y, value);
    }

    public void Paint(int mouse_grid_x, int mouse_grid_y, int value)
    {
#if UNITY_EDITOR
      Undo.RegisterCompleteObjectUndo(astarMonoBehaviour, "UnDo_AStar");
#endif
      astarMonoBehaviour.SetDataValue(mouse_grid_x, mouse_grid_y, value);
    }

    public void DrawBrush(int mouse_grid_x, int mouse_grid_y, bool is_see_obstacleType, int obstacleType,
      bool is_see_terrainType, int terrainType)
    {
      if (is_see_obstacleType)
        AStarEditorUtil.DrawdObstacleTypeRect(astarMonoBehaviour, mouse_grid_x, mouse_grid_y, obstacleType);
      if (is_see_terrainType)
        AStarEditorUtil.DrawdTerrainTypeRect(astarMonoBehaviour, mouse_grid_x, mouse_grid_y, terrainType);
    }

  }
}
