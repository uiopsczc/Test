#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class EditorIconTexture
  {
    static Dictionary<string, Texture2D> icon_texture_cache_dict = new Dictionary<string, Texture2D>();


    public static int count => icon_texture_cache_dict.Count;

    public static Texture2D Get(string name)
    {
      if (icon_texture_cache_dict.ContainsKey(name))
        return icon_texture_cache_dict[name];

      Texture2D texture = (Texture2D)EditorGUIUtility.Load(name);
      icon_texture_cache_dict[name]=texture;
      return texture;
    }

    public static Texture2D GetCustom(string name)
    {
      if (icon_texture_cache_dict.ContainsKey(name))
        return icon_texture_cache_dict[name];

      Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(string.Format("Assets/Editor/EditorExtensions/EditorTextures/{0}.png", name));
      icon_texture_cache_dict.Add(name, texture);
      return texture;
    }


    public static Texture2D GetSystem(EditorIconTextureType editorIconTextureType)
    {
      string name = EditorIconTextureConst.Icon_Texture_Names[(int)editorIconTextureType];
      if (icon_texture_cache_dict.ContainsKey(name))
        return icon_texture_cache_dict[name];

      Texture2D texture = EditorGUIUtility.FindTexture(name);
      icon_texture_cache_dict.Add(name, texture);
      return texture;
    }
    
  }
}
#endif




