
using UnityEngine;

namespace CsCat
{
	public class AStarObstacleType
	{
		public string name;
		public int value;
		public Color color;
		private Texture2D image;

		public AStarObstacleType(string name, int value, Color color)
		{
			this.name = name;
			this.value = value;
			this.color = color;
		}

		public Texture2D GetColorImage()
		{
			if (image == null)
				image = Texture2DUtil.CreateTextureOfSingleColor(16, 16, color);
			return image;
		}

	}
}
