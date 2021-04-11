using UnityEngine;

namespace CsCat
{
  public partial class GUILayoutUtil
  {
    public static GUILayoutBeginHorizontalScope BeginHorizontal()
    {
      return new GUILayoutBeginHorizontalScope();
    }

    public static GUILayoutBeginHorizontalScope BeginHorizontal(params GUILayoutOption[] layoutOptions)
    {
      return new GUILayoutBeginHorizontalScope(layoutOptions);
    }

    public static GUILayoutBeginHorizontalScope BeginHorizontal(GUIStyle guiStyle,
      params GUILayoutOption[] layoutOptions)
    {
      return new GUILayoutBeginHorizontalScope(guiStyle, layoutOptions);
    }
  }
}