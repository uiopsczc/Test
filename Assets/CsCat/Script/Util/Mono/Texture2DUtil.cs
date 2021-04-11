using UnityEngine;

namespace CsCat
{
  public static class Texture2DUtil
  {

    public static Sprite CreateSprite(Texture2D texture2D, float? width = null, float? height = null)
    {
      Rect rect = new Rect(0, 0, width.GetValueOrDefault(texture2D.width), height.GetValueOrDefault(texture2D.height));
      return Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f));
    }

    public static Texture2D CreateTextureOfSingleColor(int width, int height, Color color)
    {
      Texture2D texture = new Texture2D(width, height);
      texture.hideFlags = HideFlags.DontSave;
      for (int x = 0; x < texture.width; x++)
      {
        for (int y = 0; y < texture.height; y++)
          texture.SetPixel(x, y, color);
      }

      texture.Apply();
      return texture;
    }
  }
}