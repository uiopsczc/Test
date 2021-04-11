using UnityEngine;

namespace CsCat
{
  public partial class GUILayoutUtil
  {
    private static void SpaceCount(int count, float pixels = 20)
    {
      for (var i = 0; i < count; i++) GUILayout.Space(pixels);
    }

    public static void Space(int count = 1, bool is_horizonal = false, float pixels = 20)
    {
      if (is_horizonal)
        using (BeginHorizontal())
        {
          SpaceCount(count, pixels);
        }
      else
        SpaceCount(count, pixels);
    }
  }
}