using System;
using UnityEngine;

namespace CsCat
{
  public class ColliderTypeInfo
  {
    public ColliderType colliderType;
    public Color color;

    public ColliderTypeInfo(ColliderType colliderType, Color color)
    {
      this.colliderType = colliderType;
      this.color = color;
    }

  }
}