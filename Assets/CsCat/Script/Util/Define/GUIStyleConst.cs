#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace CsCat
{
  public  static partial class GUIStyleConst
  {
    
    public static GUIStyle Scroll_Style = new GUIStyle("ScrollView");
    
    public static GUIStyle Collider_Vertex_Handle_Style = new GUIStyle("U2D.dragDot")
    {
      normal = {textColor = Color.black},
      contentOffset = Vector2.one * 8f,
    };

    public static GUIStyle VertexCoord_Style = new GUIStyle("Label")
    {
      normal = {textColor = Color.black},
      fontStyle = FontStyle.Bold,
    };

    public static GUIStyle Toolbar_Box_Style = new GUIStyle()
    {
      normal = {textColor = Color.white},
      richText = true,
    };

    
#if UNITY_EDITOR
    public static GUIStyle Visible_Toggle_Style = new GUIStyle(EditorStyles.toggle)
    {
      normal = {background = EditorGUIUtility.FindTexture("animationvisibilitytoggleoff")},
      active = {background = EditorGUIUtility.FindTexture("animationvisibilitytoggleoff")},
      focused = {background = EditorGUIUtility.FindTexture("animationvisibilitytoggleoff")},
      hover = {background = EditorGUIUtility.FindTexture("animationvisibilitytoggleoff")},
      onNormal = {background = EditorGUIUtility.FindTexture("animationvisibilitytoggleon")},
      onActive = {background = EditorGUIUtility.FindTexture("animationvisibilitytoggleon")},
      onFocused = {background = EditorGUIUtility.FindTexture("animationvisibilitytoggleon")},
      onHover = {background = EditorGUIUtility.FindTexture("animationvisibilitytoggleon")},
    };
#endif
  }
}