using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace CsCat
{
    public class ResolutionUtil
    {
        public static Vector2 GetResolution()
        {
#if UNITY_EDITOR
            return new Vector2(Handles.GetMainGameViewSize().x, Handles.GetMainGameViewSize().y);
#else
    return new Vector2(Screen.width, Screen.height);
#endif
        }
    }
}