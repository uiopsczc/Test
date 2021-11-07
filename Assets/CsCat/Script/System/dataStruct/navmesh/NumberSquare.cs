using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
    //专门用于写数字
    public class NumberSquare
    {
        public Vector3 middleMiddle;
        public float radius;
        public Vector3 leftTop;
        public Vector3 middleTop;
        public Vector3 rightTop;
        public Vector3 leftMiddle;
        public Vector3 rightMiddle;
        public Vector3 leftBottom;
        public Vector3 middleBottom;
        public Vector3 rightBottom;
        private float factor = 0.8f;


        public NumberSquare(Vector3 middleMiddle, float radius = 0.5f)
        {
            this.middleMiddle = middleMiddle;
            this.radius = radius;

            this.leftTop = middleMiddle + factor * radius * new Vector3(-1, 0, 1);
            this.middleTop = middleMiddle + factor * radius * new Vector3(0, 0, 1);
            this.rightTop = middleMiddle + factor * radius * new Vector3(1, 0, 1);

            this.leftMiddle = middleMiddle + factor * radius * new Vector3(-1, 0, 0);
            this.rightMiddle = middleMiddle + factor * radius * new Vector3(1, 0, 0);

            this.leftBottom = middleMiddle + factor * radius * new Vector3(-1, 0, -1);
            this.middleBottom = middleMiddle + factor * radius * new Vector3(0, 0, -1);
            this.rightBottom = middleMiddle + factor * radius * new Vector3(1, 0, -1);
        }


        public List<Vector3> GetPointList(char c)
        {
            List<Vector3> pointList = new List<Vector3>();
            switch (c)
            {
                case '0':
                    pointList.Add(leftTop);
                    pointList.Add(rightTop);
                    pointList.Add(rightBottom);
                    pointList.Add(leftBottom);
                    pointList.Add(leftTop);
                    break;
                case '1':
                    pointList.Add(middleTop);
                    pointList.Add(middleBottom);
                    break;
                case '2':
                    pointList.Add(leftTop);
                    pointList.Add(rightTop);
                    pointList.Add(rightMiddle);
                    pointList.Add(leftMiddle);
                    pointList.Add(leftBottom);
                    pointList.Add(rightBottom);
                    break;
                case '3':
                    pointList.Add(leftTop);
                    pointList.Add(rightTop);
                    pointList.Add(rightMiddle);
                    pointList.Add(leftMiddle);
                    pointList.Add(rightMiddle);
                    pointList.Add(rightBottom);
                    pointList.Add(leftBottom);
                    break;
                case '4':
                    pointList.Add(middleTop);
                    pointList.Add(leftMiddle);
                    pointList.Add(middleMiddle);
                    pointList.Add(middleTop);
                    pointList.Add(middleBottom);
                    pointList.Add(middleMiddle);
                    pointList.Add(rightMiddle);
                    break;
                case '5':
                    pointList.Add(rightTop);
                    pointList.Add(leftTop);
                    pointList.Add(leftMiddle);
                    pointList.Add(rightMiddle);
                    pointList.Add(rightBottom);
                    pointList.Add(leftBottom);
                    break;
                case '6':
                    pointList.Add(rightTop);
                    pointList.Add(leftTop);
                    pointList.Add(leftMiddle);
                    pointList.Add(rightMiddle);
                    pointList.Add(rightBottom);
                    pointList.Add(leftBottom);
                    pointList.Add(leftMiddle);
                    break;
                case '7':
                    pointList.Add(leftTop);
                    pointList.Add(rightTop);
                    pointList.Add(middleBottom);
                    break;
                case '8':
                    pointList.Add(leftTop);
                    pointList.Add(rightTop);
                    pointList.Add(rightMiddle);
                    pointList.Add(leftMiddle);
                    pointList.Add(leftTop);
                    pointList.Add(leftBottom);
                    pointList.Add(rightBottom);
                    pointList.Add(rightMiddle);
                    break;
                case '9':
                    pointList.Add(rightMiddle);
                    pointList.Add(leftMiddle);
                    pointList.Add(leftTop);
                    pointList.Add(rightTop);
                    pointList.Add(rightBottom);
                    pointList.Add(leftBottom);
                    break;
                case '.':
                    pointList.Add(middleBottom + factor * radius * 0.1f * new Vector3(-1, 0, 0));
                    pointList.Add(middleBottom + factor * radius * 0.1f * new Vector3(1, 0, 0));
                    pointList.Add(middleBottom);
                    pointList.Add(middleBottom + factor * radius * 0.1f * new Vector3(0, 0, 1));
                    pointList.Add(middleBottom + factor * radius * 0.1f * new Vector3(0, 0, -1));
                    break;
                case '-':
                    pointList.Add(leftMiddle + factor * radius * 0.1f * new Vector3(1, 0, 0));
                    pointList.Add(rightMiddle + factor * radius * 0.1f * new Vector3(-1, 0, 0));
                    break;
            }

            return pointList;
        }
    }
}