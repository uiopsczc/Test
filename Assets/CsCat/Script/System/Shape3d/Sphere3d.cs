using System;
using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
    [Serializable]
    public class Sphere3D : Shape3D
    {
        public float radius;


        public Sphere3D(Vector3 worldOffset, float radius)
        {
            AddWorldOffset(worldOffset);
            this.radius = radius;
        }

        #region  operator

        public static Sphere3D operator +(Sphere3D sphere, Vector3 vector)
        {
            Sphere3D clone = CloneUtil.CloneDeep(sphere);
            clone.AddWorldOffset(vector);
            return clone;
        }

        public static Sphere3D operator -(Sphere3D sphere, Vector3 vector)
        {
            Sphere3D clone = CloneUtil.CloneDeep(sphere);
            clone.AddWorldOffset(-vector);
            return clone;
        }

        #endregion

        public override List<KeyValuePair<Vector3, Vector3>> GetDrawLineList()
        {
            List<KeyValuePair<Vector3, Vector3>> result = new List<KeyValuePair<Vector3, Vector3>>();

            result.AddRange(new Circle3D(worldOffset, radius).MultiplyMatrix(matrix)
                .MultiplyMatrix(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0))).GetDrawLineList());
            result.AddRange(new Circle3D(worldOffset, radius).MultiplyMatrix(matrix)
                .MultiplyMatrix(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0))).GetDrawLineList());
            result.AddRange(new Circle3D(worldOffset, radius).MultiplyMatrix(matrix)
                .MultiplyMatrix(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90))).GetDrawLineList());

            float eachDegree = 4;
            int segmentNum = (int) Mathf.Ceil(360 / eachDegree);
            for (int i = 0; i <= segmentNum; i++)
            {
                Circle3D circle = (Circle3D) (new Circle3D(worldOffset, radius).MultiplyMatrix(matrix)
                    .MultiplyMatrix(Matrix4x4.Rotate(Quaternion.Euler(0, 0, i * eachDegree))));
                result.AddRange(circle.GetDrawLineList());
            }

            return result;
        }
    }
}