#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
    public class UnityObjectUtil
    {
        public static void Destroy(Object o)
        {
#if UNITY_EDITOR
            if (o.IsAsset())
            {
                AssetDatabase.DeleteAsset(o.GetAssetPath());
                return;
            }
#endif

            if (Application.isPlaying)
                Object.Destroy(o);
            else
            {
                Object.DestroyImmediate(o);
            }
        }
    }
}