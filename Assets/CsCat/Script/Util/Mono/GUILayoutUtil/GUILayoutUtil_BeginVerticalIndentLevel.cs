using UnityEngine;

namespace CsCat
{
  public partial class GUILayoutUtil
  {
    public static GUILayoutBeginVerticalIndentLevelScope BeginVerticalIndentLevel(int add = 1)
    {
      return new GUILayoutBeginVerticalIndentLevelScope(add);
    }

    public static GUILayoutBeginVerticalIndentLevelScope BeginVerticalIndentLevel(int add = 1, params GUILayoutOption[] layoutOptions)
    {
      return new GUILayoutBeginVerticalIndentLevelScope(add, layoutOptions);
    }

    public static GUILayoutBeginVerticalIndentLevelScope BeginVerticalIndentLevel(GUIStyle guiStyle, int add = 1, params GUILayoutOption[] layoutOptions)
    {
      return new GUILayoutBeginVerticalIndentLevelScope(guiStyle, add, layoutOptions);
    }
  }
}