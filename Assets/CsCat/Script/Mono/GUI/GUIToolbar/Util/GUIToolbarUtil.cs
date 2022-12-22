#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
	public static partial class GUIToolbarUtil
	{
		private static Texture2D CreateToolbarIconTexture(float[] iconBits)
		{
			Texture2D iconTexture = new Texture2D(8, 8);
			iconTexture.hideFlags = HideFlags.DontSave;
			iconTexture.wrapMode = TextureWrapMode.Clamp;
			Color[] colors = new Color[iconBits.Length];
			for (int i = 0; i < colors.Length; ++i)
				colors[(8 - 1 - (i / 8)) * 8 + i % 8] = new Color(1f, 1f, 1f, iconBits[i]);
			iconTexture.SetPixels(colors);
			iconTexture.Apply();
			return iconTexture;
		}

		public static Texture2D GetToolbarIconTexture(float[] iconBits)
		{
			int hashcode = iconBits.GetHashCode();
			if (!GUIToolbarConst.Texture_Dict.ContainsKey(hashcode))
			{
				Texture2D iconTexture = CreateToolbarIconTexture(iconBits);
				GUIToolbarConst.Texture_Dict[hashcode] = iconTexture;
			}

			return GUIToolbarConst.Texture_Dict[hashcode];
		}

	}
}
#endif