using System;
using UnityEngine;

namespace CsCat
{
  public static class UnitySerializeObjectType
  {
    #region field

    public static Type Vector2Type = typeof(Vector2);
    public static Type Vector3Type = typeof(Vector3);
    public static Type Vector4Type = typeof(Vector4);
    public static Type QuaternionType = typeof(Quaternion);
    public static Type BoundsType = typeof(Bounds);
    public static Type ColorType = typeof(Color);
    public static Type RectType = typeof(Rect);

    #endregion


    #region static method

    public static bool IsSerializeType(Type type)
    {
      return type == UnitySerializeObjectType.Vector2Type || type == UnitySerializeObjectType.Vector3Type ||
             type == UnitySerializeObjectType.Vector4Type || type == UnitySerializeObjectType.QuaternionType ||
             type == UnitySerializeObjectType.BoundsType || type == UnitySerializeObjectType.ColorType ||
             type == UnitySerializeObjectType.RectType;
    }

    #endregion

  }
}