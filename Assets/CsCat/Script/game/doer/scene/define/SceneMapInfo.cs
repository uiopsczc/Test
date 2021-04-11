using UnityEngine;

namespace CsCat
{
  public class SceneMapInfo
  {
    public int[][] grids; // 自身路径图
    public int[][] project_grids; // 自身投影图
    public Vector2Int offset_pos; //偏移

    public SceneMapInfo(int[][] grids, int[][] project_grids, int offset_pos_x, int offset_pos_y)
    {
      this.grids = grids;
      this.project_grids = project_grids;
      this.offset_pos = new Vector2Int(offset_pos_x, offset_pos_y);
    }
  }
}