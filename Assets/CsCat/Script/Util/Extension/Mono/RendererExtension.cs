

using UnityEngine;

namespace CsCat
{
  public static class RendererExtension

  {
    public static Material Material(this Renderer self)
    {
      if (Application.isPlaying)
        return self.material;
      else
        return self.sharedMaterial;
    }
  }
}