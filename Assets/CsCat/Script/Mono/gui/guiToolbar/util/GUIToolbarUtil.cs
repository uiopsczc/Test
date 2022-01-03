#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
	public static partial class GUIToolbarUtil
	{
		private static Texture2D CreateToolbarIconTexture(float[] icon_bits)
		{
			Texture2D icon_texture = new Texture2D(8, 8);
			icon_texture.hideFlags = HideFlags.DontSave;
			icon_texture.wrapMode = TextureWrapMode.Clamp;
			Color[] colors = new Color[icon_bits.Length];
			for (int i = 0; i < colors.Length; ++i)
				colors[(8 - 1 - (i / 8)) * 8 + i % 8] = new Color(1f, 1f, 1f, icon_bits[i]);
			icon_texture.SetPixels(colors);
			icon_texture.Apply();
			return icon_texture;
		}

		public static Texture2D GetToolbarIconTexture(float[] icon_bits)
		{
			int hashcode = icon_bits.GetHashCode();
			if (!GUIToolbarConst.texture_dict.ContainsKey(hashcode))
			{
				Texture2D icon_texture = CreateToolbarIconTexture(icon_bits);
				GUIToolbarConst.texture_dict[hashcode] = icon_texture;
			}

			return GUIToolbarConst.texture_dict[hashcode];
		}

	}
}
#endif