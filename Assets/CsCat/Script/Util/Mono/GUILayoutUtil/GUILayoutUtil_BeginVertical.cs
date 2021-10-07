using UnityEngine;

namespace CsCat
{
    public partial class GUILayoutUtil
    {
        public static GUILayoutBeginVerticalScope BeginVertical()
        {
            return new GUILayoutBeginVerticalScope();
        }

        public static GUILayoutBeginVerticalScope BeginVertical(params GUILayoutOption[] layoutOptions)
        {
            return new GUILayoutBeginVerticalScope(layoutOptions);
        }

        public static GUILayoutBeginVerticalScope BeginVertical(GUIStyle guiStyle,
            params GUILayoutOption[] layoutOptions)
        {
            return new GUILayoutBeginVerticalScope(guiStyle, layoutOptions);
        }
    }
}