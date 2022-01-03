using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public static class TextExtension
	{
		public static void SetText(this Text self, string content = null, Color? color = null, int? fontSize = null)
		{
			if (content != null)
				self.text = content;
			if (color.HasValue)
				self.color = color.Value;
			if (fontSize.HasValue)
				self.fontSize = fontSize.Value;
		}

		public static void SetIsGray(this Text self, bool isGray)
		{
			TextUtil.SetIsGray(self, isGray);
		}

		public static void SetAlpha(this Text self, float alpha)
		{
			TextUtil.SetAlpha(self, alpha);
		}

		public static void SetColor(this Text self, Color color, bool isNotUseColorAlpha = false)
		{
			TextUtil.SetColor(self, color, isNotUseColorAlpha);
		}
	}
}