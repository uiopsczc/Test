using System;
using UnityEngine;

namespace CsCat
{
  public class AStarNode
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

    public static int Compare(AStarNode data1,AStarNode data2)
    {
      if (data1.f - data2.f >= 0)
        return 1;
      else
        return -1;
    }

  }
}

