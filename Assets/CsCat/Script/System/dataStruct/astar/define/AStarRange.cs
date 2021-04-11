using System;
using UnityEngine;

namespace CsCat
{
  public class AStarRange
  {
    public int left_bottom_x;
    public int left_bottom_y;
    public int right_top_x;
    public int right_top_y;

    public AStarRange(int left_bottom_x, int left_bottom_y, int right_top_x, int right_top_y)
    {
      SetRange(left_bottom_x, left_bottom_y, right_top_x, right_top_y);
    }

    public AStarRange(AStarRange range)
    {
      SetRange(range.left_bottom_x, range.left_bottom_y, range.right_top_x, range.right_top_y);
    }

    public AStarRange(Vector2Int left_bottom, Vector2Int right_top)
    {
      SetRange(left_bottom.x, left_bottom.y, right_top.x, right_top.y);
    }

    public AStarRange(Vector2Int left_bottom, int dx, int dy) : this(left_bottom, left_bottom + new Vector2Int(dx, dy))
    {
    }

    public void SetRange(int left_bottom_x, int left_bottom_y, int right_top_x, int right_top_y)
    {
      this.left_bottom_x = Math.Min(left_bottom_x, right_top_x);
      this.left_bottom_y = Math.Min(left_bottom_y, right_top_y);
      this.right_top_x = Math.Max(left_bottom_x, right_top_x);
      this.right_top_y = Math.Max(left_bottom_y, right_top_y);
    }

    public Vector2Int GetCenter()
    {
      return new Vector2Int((left_bottom_x + right_top_x) / 2, (left_bottom_y + right_top_y) / 2);
    }

    public Vector2Int GetLeftBottom()
    {
      return new Vector2Int(left_bottom_x, left_bottom_y);
    }

    public Vector2Int GetRightTop()
    {
      return new Vector2Int(right_top_x, right_top_y);
    }

    public Vector2Int GetRandomPos(RandomManager randomManager = null)
    {
      randomManager = randomManager ?? Client.instance.randomManager;
      int dx = right_top_x - left_bottom_x;
      int dy = right_top_y - left_bottom_y;
      int x, y;
      if (dx > 0)
        x = left_bottom_x + randomManager.RandomInt(0, Math.Abs(dx) + 1);
      else if (dx < 0)
        x = left_bottom_x - randomManager.RandomInt(0, Math.Abs(dx) + 1);
      else
        x = left_bottom_x;
      if (dy > 0)
        y = left_bottom_y + randomManager.RandomInt(0, Math.Abs(dy) + 1);
      else if (dy < 0)
        y = left_bottom_y - randomManager.RandomInt(0, Math.Abs(dy) + 1);
      else
        y = left_bottom_y;
      return new Vector2Int(x, y);
    }

    public int GetWidth()
    {
      return right_top_x - left_bottom_x;
    }

    public int GetHeight()
    {
      return right_top_y - left_bottom_y;
    }

    public bool IsInRange(Vector2Int pos)
    {
      return IsInRangeX(pos.x) && IsInRangeY(pos.y);
    }

    public bool IsInRangeX(int x)
    {
      RangeCat range_x = new RangeCat(left_bottom_x, right_top_x);
      return range_x.IsContains(x);
    }

    public bool IsInRangeY(int y)
    {
      RangeCat range_y = new RangeCat(left_bottom_y, right_top_y);
      return range_y.IsContains(y);
    }

    public override string ToString()
    {
      return string.Format("[{0},{1}]-[{2},{3}]", left_bottom_x, left_bottom_y, right_top_x, right_top_y);
    }

  }
}