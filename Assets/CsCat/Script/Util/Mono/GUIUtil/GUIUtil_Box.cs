using System;
using UnityEngine;

namespace CsCat
{
    public partial class GUIUtil
    {
        public static void Box(Rect rect, string content, Color? backgroundColor = null, bool isOutline = false,
            float borderSize = 1)
        {
            using (new GUIBackgroundColorScope(backgroundColor ?? GUI.backgroundColor))
            {
                GUI.Box(rect, content);
            }

            _Box_Border(rect, isOutline, borderSize);
        }

        public static void Box(Rect rect, Texture image, Color? backgroundColor = null, bool isOutline = false,
            float borderSize = 1)
        {
            using (new GUIBackgroundColorScope(backgroundColor ?? GUI.backgroundColor))
            {
                GUI.Box(rect, image);
            }

            _Box_Border(rect, isOutline, borderSize);
        }

        public static void Box(Rect rect, GUIContent content, Color? backgroundColor = null, bool isOutline = false,
            float borderSize = 1)
        {
            using (new GUIBackgroundColorScope(backgroundColor ?? GUI.backgroundColor))
            {
                GUI.Box(rect, content);
            }

            _Box_Border(rect, isOutline, borderSize);
        }

        public static void Box(Rect rect, string content, GUIStyle style, Color? backgroundColor = null,
            bool isOutline = false, float borderSize = 1)
        {
            using (new GUIBackgroundColorScope(backgroundColor ?? GUI.backgroundColor))
            {
                GUI.Box(rect, content, style);
            }

            _Box_Border(rect, isOutline, borderSize);
        }

        public static void Box(Rect rect, Texture image, GUIStyle style, Color? backgroundColor = null,
            bool isOutline = false, float borderSize = 1)
        {
            using (new GUIBackgroundColorScope(backgroundColor ?? GUI.backgroundColor))
            {
                GUI.Box(rect, image, style);
            }

            _Box_Border(rect, isOutline, borderSize);
        }

        public static void Box(Rect rect, GUIContent content, GUIStyle style, Color? backgroundColor = null,
            bool isOutline = false, float borderSize = 1)
        {
            using (new GUIBackgroundColorScope(backgroundColor ?? GUI.backgroundColor))
            {
                GUI.Box(rect, content, style);
            }

            _Box_Border(rect, isOutline, borderSize);
        }

        private static void _Box_Border(Rect rect, bool isOutline = false, float borderSize = 1)
        {
            float x = rect.x;
            float y = rect.y;
            float width = rect.width;
            float height = rect.height;
            using (new GUIColorScope(UnityEngine.Color.black))
            {
                if (isOutline)
                {
                    //上边
                    GUI.Box(new Rect(x - borderSize, y - borderSize, width + 2 * borderSize, borderSize),
                        StringConst.String_Empty);
                    //下边
                    GUI.Box(new Rect(x - borderSize, y + height, width + 2 * borderSize, borderSize),
                        StringConst.String_Empty);
                    //左边
                    GUI.Box(new Rect(x - borderSize, y - borderSize, borderSize, height + 2 * borderSize),
                        StringConst.String_Empty);
                    //右边
                    GUI.Box(new Rect(x + width, y - borderSize, borderSize, height + 2 * borderSize),
                        StringConst.String_Empty);
                }
                else
                {
                    //上边
                    GUI.Box(new Rect(x, y, width, borderSize), StringConst.String_Empty);
                    //下边
                    GUI.Box(new Rect(x, y + height - borderSize, width, borderSize), StringConst.String_Empty);
                    //左边
                    GUI.Box(new Rect(x, y, borderSize, height), StringConst.String_Empty);
                    //右边
                    GUI.Box(new Rect(x + width - borderSize, y, borderSize, height), StringConst.String_Empty);
                }
            }
        }
    }
}