using UnityEngine;

namespace CsCat
{
  public static class Vector2Const
  {
    public static Vector2 Max = new Vector2(float.MaxValue, float.MaxValue);

    public static Vector2 Min = new Vector2(float.MinValue, float.MinValue);

    public static Vector2 Default_Max = Max;

    public static Vector2 Default_Min = Min;

    public static Vector2 Default = Default_Max;
  }
}