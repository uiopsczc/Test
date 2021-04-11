namespace CsCat
{
  //坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
  public class AStarImpl : AStar
  {
    protected AStarMapPath astarMapPath; // 地图数组
    protected int[] can_pass_obstacle_types; // 可通过障碍表
    protected int[] can_pass_terrain_types; // 可通过地形表

    public AStarImpl(AStarMapPath astarMapPath, AStarType astarType, int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types) : base(astarType)
    {
      this.astarMapPath = astarMapPath;
      this.can_pass_obstacle_types = can_pass_obstacle_types;
      this.can_pass_terrain_types = can_pass_terrain_types;
      SetRange(0, 0, astarMapPath.Height() - 1, astarMapPath.Width() - 1);
    }

    protected override bool CanPass(int x, int y)
    {
      if (!AStarUtil.CanPass(astarMapPath.GetFinalGrids(), x, y, can_pass_obstacle_types, can_pass_terrain_types))
        return false;
      return true;
    }

  }
}
