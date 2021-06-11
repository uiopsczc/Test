#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class EditorUtilityCat
  {
    public static void DisplayDialog(string message,string copy_content=null)
    {
      EditorUtility.DisplayDialog("", message, "确定");
      Debug.Log(message);
      if (copy_content.IsNullOrWhiteSpace())
        GUIUtility.systemCopyBuffer = copy_content;
    }
  }
}
#endif