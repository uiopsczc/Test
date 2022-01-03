using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public static class ImageExtension
	{
		/// <summary>
		/// 设置图片的alpha
		/// </summary>
		/// <param name="self"></param>
		/// <param name="alpha"></param>
		public static void SetAlpha(this Image self, float alpha)
		{
			ImageUtil.SetAlpha(self, alpha);
		}

		public static void SetIsGray(this Image self, bool isGray)
		{
			ImageUtil.SetIsGray(self, isGray);
		}

		public static void SetColor(this Image self, Color color, bool isNotUseColorAlpha = false)
		{
			ImageUtil.SetColor(self, color, isNotUseColorAlpha);
		}
	}
}