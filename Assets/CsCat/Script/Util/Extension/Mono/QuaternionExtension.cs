using UnityEngine;

namespace CsCat
{
  public static class QuaternionExtension
  {
    public static bool IsDefault(this Quaternion self, bool is_min = false)
    {
      if (is_min)
        return self == QuaternionConst.Default_Min;
      else
        return self == QuaternionConst.Default_Max;
    }

    public static Quaternion Inverse(this Quaternion self)
    {
      return Quaternion.Inverse(self);
    }

    public static Vector3 Forward(this Quaternion self)
    {
      return self * Vector3.forward;
    }

    public static Vector3 Back(this Quaternion self)
    {
      return self * Vector3.back;
    }

    public static Vector3 Up(this Quaternion self)
    {
      return self * Vector3.up;
    }

    public static Vector3 Down(this Quaternion self)
    {
      return self * Vector3.down;
    }

    public static Vector3 Left(this Quaternion self)
    {
      return self * Vector3.left;
    }

    public static Vector3 Right(this Quaternion self)
    {
      return self * Vector3.right;
    }

    public static bool IsZero(this Quaternion self)
    {
      if (self.x == 0 && self.y == 0 && self.z == 0)
        return true;
      return false;
    }

    public static Quaternion GetNotZero(this Quaternion self, Quaternion? default_value = null)
    {
      default_value = default_value ?? Quaternion.identity;
      if (self.IsZero())
        return default_value.Value;
      return self;
    }

  }
}