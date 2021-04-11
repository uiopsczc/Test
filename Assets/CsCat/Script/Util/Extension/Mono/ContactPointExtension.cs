using UnityEngine;

namespace CsCat
{
  public static class ContactPointExtension
  {
    public static float PercentYOfThisCollider(this ContactPoint self)
    {

      Vector3 point = self.point;
      float yDistance = self.thisCollider.bounds.extents.y * 2;
      float result = (point.y - self.thisCollider.bounds.FrontBottomLeft().y) / yDistance;
      return result;
    }

    public static float PercentYOfOtherCollider(this ContactPoint self)
    {
      Vector3 point = self.point;
      float yDistance = self.otherCollider.bounds.extents.y * 2;
      float result = (point.y - self.otherCollider.bounds.FrontBottomLeft().y) / yDistance;
      return result;
    }


  }
}