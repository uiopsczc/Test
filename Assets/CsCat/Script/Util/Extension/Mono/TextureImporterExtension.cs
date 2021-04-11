#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public static class TextureImporterExtension
  {
    //ref: https://forum.unity3d.com/threads/getting-original-size-of-texture-asset-in-pixels.165295/
    public static void GetWidthAndHeight(this TextureImporter self, ref int width, ref int height)
    {
      TextureImporterUtil.GetWidthAndHeight(self, ref width, ref height);
    }

    //ref:https://forum.unity3d.com/threads/getting-original-size-of-texture-asset-in-pixels.165295/
    public static void FixTextureSize(this TextureImporter self, Texture2D texture2D)
    {
      TextureImporterUtil.FixTextureSize(self, texture2D);
    }
  }
}
#endif