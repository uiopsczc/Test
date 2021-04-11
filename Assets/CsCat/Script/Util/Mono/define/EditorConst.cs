#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public static class EditorConst
  {
    public static float Single_Line_Height = EditorGUIUtility.singleLineHeight;

    public static Texture2D White_Texture { get { return EditorGUIUtility.whiteTexture; } }

    
  }
}
#endif