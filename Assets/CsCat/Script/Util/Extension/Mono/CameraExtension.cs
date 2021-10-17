using UnityEngine;

namespace CsCat
{
    public static class CameraExtension
    {
        public static bool IsPosInViewPort(this Camera self, Vector3 worldPosition)
        {
            Vector3 viewportPosition = self.WorldToViewportPoint(worldPosition);
            return viewportPosition.z > 0 && viewportPosition.x.IsInRange(0, 1) && viewportPosition.y.IsInRange(0, 1);
        }
    }
}