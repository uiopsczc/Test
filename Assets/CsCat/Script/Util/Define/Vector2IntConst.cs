using UnityEngine;

namespace CsCat
{
  public static class Vector2IntConst
  {
    public static Vector2Int Max = new Vector2Int(int.MaxValue, int.MaxValue);

    public static Vector2Int Min = new Vector2Int(int.MinValue, int.MinValue);

    public static Vector2Int Default_Max = Max;

    public static Vector2Int Default_Min = Min;

    public static Vector2Int Default = Default_Max;
  }
}