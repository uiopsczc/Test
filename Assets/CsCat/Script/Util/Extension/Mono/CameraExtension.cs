using UnityEngine;

namespace CsCat
{
  public static class CameraExtension
  {
    public static bool IsPosInViewPort(this Camera self, Vector3 world_position)
    {
      Vector3 viewport_position = self.WorldToViewportPoint(world_position);
      if (viewport_position.z > 0 && viewport_position.x.IsInRange(0, 1) && viewport_position.y.IsInRange(0, 1))
        return true;
      return false;
    }
  }
}