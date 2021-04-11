using UnityEngine;

namespace CsCat
{
  public static class Texture2DExtension
  {


    public static Sprite CreateSprite(this Texture2D self, float? width = null, float? height = null)
    {
      return Texture2DUtil.CreateSprite(self, width, height);
    }
  }
}