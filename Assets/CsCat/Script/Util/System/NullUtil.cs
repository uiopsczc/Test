using System;

namespace CsCat
{
  public static class NullUtil
  {
    public const string Null_Default_String = "__null__";

    public static string GetDefaultString()
    {
      return Null_Default_String;
    }
  }
}