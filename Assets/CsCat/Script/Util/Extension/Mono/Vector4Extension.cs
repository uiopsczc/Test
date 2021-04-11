using UnityEngine;

namespace CsCat
{
  public static class Vector4Extension
  {

    public static string ToStringOrDefault(this Vector4 self, string to_default_string = null,
      Vector4 default_value = default(Vector4))
    {
      if (ObjectUtil.Equals(self, default_value))
        return to_default_string;
      return self.ToString();
    }
  }
}