#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
  public static class GUILayoutToggleAreaScopeTest
  {
    private static GUIToggleTween toggleTween = new GUIToggleTween();
    public static void Test()
    {
      using (new GUILayoutToggleAreaScope(toggleTween, "Chen"))
      {
        for (int i=0;i<20;i++)
        GUILayout.Label("cccc"+i);
      }
      GUILayout.Label("Good");
    }
  }
}
#endif