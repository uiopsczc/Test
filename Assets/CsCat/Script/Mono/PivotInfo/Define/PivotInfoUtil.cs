using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
    public static class PivotInfoUtil
    {
        public static PivotInfo GetPivotInfo(float x, float y)
        {
            return PivotInfoConst.PivotInfoDict2[new Vector2(x, y)];
        }

        public static PivotInfo GetPivotInfo(string name)
        {
            return PivotInfoConst.PivotInfoDict[name.ToLower()];
        }
    }
}