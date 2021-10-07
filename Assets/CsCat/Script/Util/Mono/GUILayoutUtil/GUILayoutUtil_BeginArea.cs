using UnityEngine;

namespace CsCat
{
    public partial class GUILayoutUtil
    {
        public static GUILayoutBeginAreaScope BeginArea(Rect area)
        {
            return new GUILayoutBeginAreaScope(area);
        }

        public static GUILayoutBeginAreaScope BeginArea(Rect area, string content)
        {
            return new GUILayoutBeginAreaScope(area, content);
        }

        public static GUILayoutBeginAreaScope BeginArea(Rect area, string content, string style)
        {
            return new GUILayoutBeginAreaScope(area, content, style);
        }
    }
}