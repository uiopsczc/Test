using System;
using UnityEngine;

namespace CsCat
{
  public class ColliderCat
  {
    public ColliderType collider_type;
    public BoxBase box;

    public bool IsIntersect(ColliderCat other_collider, float tolerence = float.Epsilon)
    {
      return box.IsIntersect(other_collider.box, tolerence);
    }

    public void DebugDraw(Vector3 offset)
    {
      box.DebugDraw(offset, ColliderConst.ColliderInfo_Dict[collider_type].color);
    }
  }
}