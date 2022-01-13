#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
	public static partial class GUIToolbarUtil
	{
		private static Texture2D CreateToolbarIconTexture(float[] icon_bits)
		{
			Texture2D iconTexture = new Texture2D(8, 8);
			iconTexture.hideFlags = HideFlags.DontSave;
			iconTexture.wrapMode = TextureWrapMode.Clamp;
			Color[] colors = new Color[icon_bits.Length];
			for (int i = 0; i < colors.Length; ++i)
				colors[(8 - 1 - (i / 8)) * 8 + i % 8] = new Color(1f, 1f, 1f, icon_bits[i]);
			iconTexture.SetPixels(colors);
			iconTexture.Apply();
			return iconTexture;
		}

		public static Texture2D GetToolbarIconTexture(float[] icon_bits)
		{
			int hashcode = icon_bits.GetHashCode();
			if (!GUIToolbarConst.Texture_Dict.ContainsKey(hashcode))
			{
				Texture2D iconTexture = CreateToolbarIconTexture(icon_bits);
				GUIToolbarConst.Texture_Dict[hashcode] = iconTexture;
			}

			return GUIToolbarConst.Texture_Dict[hashcode];
		}

	}
}
#endif