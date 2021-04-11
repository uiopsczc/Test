using UnityEngine;

namespace CsCat
{
  public static class Vector3IntConst
  {
    public static Vector3Int Max = new Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue);

    public static Vector3Int Min = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);

    public static Vector3Int Default_Max = Max;

    public static Vector3Int Default_Min = Min;

    public static Vector3Int Default = Default_Max;
  }
}