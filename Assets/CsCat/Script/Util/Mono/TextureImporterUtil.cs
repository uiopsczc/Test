#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
    public static class TextureImporterUtil
    {
        private static string _GetWidthAndHeight_String = "GetWidthAndHeight";

        private static int[] _Texture_Sizes =
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

        private delegate void GetWidthAndHeightDelegate(TextureImporter textureImporter, ref int width, ref int height);

        private static GetWidthAndHeightDelegate _getWidthAndHeightDelegate;


        //ref: https://forum.unity3d.com/threads/getting-original-size-of-texture-asset-in-pixels.165295/
        public static void GetWidthAndHeight(TextureImporter textureImporter, ref int width, ref int height)
        {
            if (_getWidthAndHeightDelegate == null)
            {
                var method =
                    typeof(TextureImporter).GetMethodInfo2(_GetWidthAndHeight_String,
                        BindingFlags.NonPublic | BindingFlags.Instance);
                _getWidthAndHeightDelegate =
                    Delegate.CreateDelegate(typeof(GetWidthAndHeightDelegate), null, method) as
                        GetWidthAndHeightDelegate;
            }

            _getWidthAndHeightDelegate(textureImporter, ref width, ref height);
        }

        //ref:https://forum.unity3d.com/threads/getting-original-size-of-texture-asset-in-pixels.165295/
        public static void FixTextureSize(TextureImporter textureImporter, Texture2D texture2D)
        {
            int width = 0, height = 0;
            GetWidthAndHeight(textureImporter, ref width, ref height);
            var max = Mathf.Max(width, height);
            int size = 1024; //Default size
            foreach (var textureSize in _Texture_Sizes)
            {
                if (textureSize < max) continue;
                size = textureSize;
                break;
            }

            if (size == int.MaxValue)
                EditorUtility.DisplayDialog("Texture太大",
                    string.Format("{0}太大，图片的长度和宽度需要少于或者等于{1}", texture2D.name,
                        _Texture_Sizes[_Texture_Sizes.Length - 2]), "Ok");
            else
                textureImporter.maxTextureSize = size;
        }
    }
}
#endif