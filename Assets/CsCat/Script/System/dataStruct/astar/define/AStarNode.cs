using System;
using UnityEngine;

namespace CsCat
{
  public class AStarNode : IComparable<AStarNode>
  {
    public Vector2Int pos;
    public AStarNode parent;
    public float g; //当前消耗值
    public float h; //预估还需的消耗值
    public float f; //当前消耗值 + 预估还需的消耗值


    public AStarNode(int x, int y)
    {
      this.pos = new Vector2Int(x, y);
    }



    public override bool Equals(object obj)
    {
      if (!(obj is AStarNode))
        return false;
      AStarNode other = obj as AStarNode;
      return other.pos.Equals(this.pos);
    }

    public int CompareTo(AStarNode other)
    {
      if (f - other.f >= 0)
        return 1;
      else
        return -1;
    }

  }
}

