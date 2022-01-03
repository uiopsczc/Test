using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class ImageUtil
	{
		/// <summary>
		/// 设置图片的alpha
		/// </summary>
		/// <param name="image"></param>
		/// <param name="alpha"></param>
		public static void SetAlpha(Image image, float alpha)
		{
			image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
		}

		public static void SetIsGray(Image image, bool isGray)
		{
		}

		public static void SetColor(Image image, Color color, bool isNotUseColorAlpha = false)
		{
			image.color = new Color(color.r, color.g, color.b, isNotUseColorAlpha ? image.color.a : color.a);
		}
	}
}