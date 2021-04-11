using System;
using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
  [Serializable]
  public class Sphere3d : Shape3d
  {
    public float radius;


    public Sphere3d(Vector3 world_offset, float radius)
    {
      AddWorldOffset(world_offset);
      this.radius = radius;
    }

    #region  operator

    public static Sphere3d operator +(Sphere3d sphere, Vector3 vector)
    {
      Sphere3d clone = CloneUtil.Clone_Deep(sphere);
      clone.AddWorldOffset(vector);
      return clone;
    }

    public static Sphere3d operator -(Sphere3d sphere, Vector3 vector)
    {
      Sphere3d clone = CloneUtil.Clone_Deep(sphere);
      clone.AddWorldOffset(-vector);
      return clone;
    }



    #endregion

    public override List<KeyValuePair<Vector3, Vector3>> GetDrawLineList()
    {

      List<KeyValuePair<Vector3, Vector3>> result = new List<KeyValuePair<Vector3, Vector3>>();

      result.AddRange(new Circle3d(world_offset, radius).MultiplyMatrix(matrix)
        .MultiplyMatrix(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0))).GetDrawLineList());
      result.AddRange(new Circle3d(world_offset, radius).MultiplyMatrix(matrix)
        .MultiplyMatrix(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0))).GetDrawLineList());
      result.AddRange(new Circle3d(world_offset, radius).MultiplyMatrix(matrix)
        .MultiplyMatrix(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90))).GetDrawLineList());

      float eachDegree = 4;
      int segmentNum = (int) Mathf.Ceil(360 / eachDegree);
      for (int i = 0; i <= segmentNum; i++)
      {
        Circle3d circle = (Circle3d) (new Circle3d(world_offset, radius).MultiplyMatrix(matrix)
          .MultiplyMatrix(Matrix4x4.Rotate(Quaternion.Euler(0, 0, i * eachDegree))));
        result.AddRange(circle.GetDrawLineList());
      }

      return result;
    }


  }
}

