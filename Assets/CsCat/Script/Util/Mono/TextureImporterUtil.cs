#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public static class TextureImporterUtil
  {
    private delegate void GetWidthAndHeightDelegate(TextureImporter textureImporter, ref int width, ref int height);

    private static GetWidthAndHeightDelegate getWidthAndHeightDelegate;

    private static int[] texture_sizes = new int[]
    {
      32,
      64,
      128,
      256,
      512,
      1024,
      2048,
      4096,
      8192,
      int.MaxValue,
    };

    //ref: https://forum.unity3d.com/threads/getting-original-size-of-texture-asset-in-pixels.165295/
    public static void GetWidthAndHeight(TextureImporter textureImporter, ref int width, ref int height)
    {
      if (getWidthAndHeightDelegate == null)
      {
        var method =
          typeof(TextureImporter).GetMethodInfo2("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);
        getWidthAndHeightDelegate =
          Delegate.CreateDelegate(typeof(GetWidthAndHeightDelegate), null, method) as GetWidthAndHeightDelegate;
      }

      getWidthAndHeightDelegate(textureImporter, ref width, ref height);
    }

    //ref:https://forum.unity3d.com/threads/getting-original-size-of-texture-asset-in-pixels.165295/
    public static void FixTextureSize(TextureImporter textureImporter, Texture2D texture2D)
    {
      int width = 0, height = 0, max;
      GetWidthAndHeight(textureImporter, ref width, ref height);
      max = Mathf.Max(width, height);
      int size = 1024; //Default size
      for (int i = 0; i < texture_sizes.Length; i++)
      {
        if (texture_sizes[i] >= max)
        {
          size = texture_sizes[i];
          break;
        }
      }

      if (size == int.MaxValue)
        EditorUtility.DisplayDialog("Texture太大",
          string.Format("{0}太大，图片的长度和宽度需要少于或者等于{1}", texture2D.name, texture_sizes[texture_sizes.Length - 2]), "Ok");
      else
        textureImporter.maxTextureSize = size;
    }
  }
}
#endif