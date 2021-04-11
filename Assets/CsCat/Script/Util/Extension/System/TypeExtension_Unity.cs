using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace CsCat
{
  public static partial class TypeExtension
  {
#if UNITY_EDITOR
    public static Texture2D GetMiniTypeThumbnail(this Type self)
    {
      return AssetPreview.GetMiniTypeThumbnail(self);
    }
#endif
  }
}