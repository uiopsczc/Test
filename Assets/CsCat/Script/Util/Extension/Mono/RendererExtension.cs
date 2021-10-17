using UnityEngine;

namespace CsCat
{
    public static class RendererExtension
    {
        public static Material Material(this Renderer self)
        {
            return Application.isPlaying ? self.material : self.sharedMaterial;
        }
    }
}