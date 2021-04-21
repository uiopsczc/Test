#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class EditorIconGUIContent
  {
    static Dictionary<string, GUIContent> icon_GUIContent_cache_dict = new Dictionary<string, GUIContent>();

    public static int count { get { return icon_GUIContent_cache_dict.Count; } }

    public static GUIContent custom_GUIContent
    {
      get
      {
        GUIContent content = Get("CustomContent");
        return content;
      }
    }


    public static GUIContent Get(string name, string text, string tips)
    {
      if (icon_GUIContent_cache_dict.ContainsKey(name))
        return icon_GUIContent_cache_dict[name];
      GUIContent gui_content = new GUIContent(text, EditorIconTexture.GetCustom(name), tips);
      icon_GUIContent_cache_dict[name] = gui_content;
      return gui_content;
    }

    public static GUIContent Get(string name, string text)
    {
      if (icon_GUIContent_cache_dict.ContainsKey(name))
        return icon_GUIContent_cache_dict[name];
      GUIContent gui_content = new GUIContent(text, EditorIconTexture.GetCustom(name));
      icon_GUIContent_cache_dict[name] = gui_content;
      return gui_content;
    }

    public static GUIContent Get(string name)
    {
      if (icon_GUIContent_cache_dict.ContainsKey(name))
        return icon_GUIContent_cache_dict[name];
      GUIContent gui_content = new GUIContent(EditorIconTexture.GetCustom(name));
      icon_GUIContent_cache_dict.Add(name, gui_content);
      return gui_content;
    }

    public static GUIContent Get(EditorIconTextureType editorIconTextureType)
    {
      if (icon_GUIContent_cache_dict.ContainsKey(editorIconTextureType.ToString()))
        return icon_GUIContent_cache_dict[editorIconTextureType.ToString()];
      GUIContent gui_content = new GUIContent(EditorIconTexture.GetSystem(editorIconTextureType));
      icon_GUIContent_cache_dict.Add(editorIconTextureType.ToString(), gui_content);
      return gui_content;
    }

    public static GUIContent GetSystem(EditorIconGUIContentType editorIconGUIContentType)
    {
      string name = EditorIconGUIContentConst.Icon_GUIContent_Names[(int)editorIconGUIContentType];
      if (icon_GUIContent_cache_dict.ContainsKey(name))
        return icon_GUIContent_cache_dict[name];

      GUIContent gui_content = EditorGUIUtility.IconContent(name);
      icon_GUIContent_cache_dict[name] = gui_content;
      return gui_content;
    }
  }
}
#endif




