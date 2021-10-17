using System;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

namespace CsCat
{
    public static class ComponentExtensions
    {
        public static RectTransform RectTransform(this Component self)
        {
            return self.GetComponent<RectTransform>();
        }

        public static GameObject NewChildGameObject(this Component self, string path = null)
        {
            if (self == null || self.transform == null)
                return null;
            GameObject gameObject = new GameObject();
            if (!path.IsNullOrWhiteSpace())
            {
                int index = path.IndexOf(CharConst.Char_Slash);
                if (index > 0)
                {
                    var name = path.Substring(0, index);
                    gameObject.name = name;
                    NewChildGameObject(gameObject.transform, path.Substring(index + 1));
                }
                else
                    gameObject.name = path;
            }

            gameObject.transform.SetParent(self.transform, false);
            return gameObject;
        }

        public static Component NewChildWithComponent(this Component self, Type componentType, string path = null)
        {
            if (self == null)
                return null;
            GameObject gameObject = NewChildGameObject(self, path);
            return gameObject.AddComponent(componentType);
        }

        public static T NewChildWithComponent<T>(this Component self, string path = null) where T : Component
        {
            return NewChildWithComponent(self, typeof(T), path) as T;
        }

        public static RectTransform NewChildWithRectTransform(this Component self, string path = null)
        {
            return NewChildWithComponent<RectTransform>(self, path);
        }

        public static Image NewChildWithImage(this Component self, string path = null)
        {
            return NewChildWithComponent<Image>(self, path);
        }

        public static Text NewChildWithText(this Component self, string path = null, string content = null,
            int fontSize = 20, Color? color = null, TextAnchor? alignment = null, Font font = null)
        {
            Text text = NewChildWithComponent<Text>(self, path);
            if (text != null)
            {
                text.alignment = alignment.GetValueOrDefault(default);
                text.color = color.GetValueOrDefault(Color.black);
                text.font = font ?? (Font) Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                text.fontSize = fontSize;
                text.horizontalOverflow = HorizontalWrapMode.Overflow;
                text.verticalOverflow = VerticalWrapMode.Overflow;

                if (!content.IsNullOrWhiteSpace())
                    text.text = content;
            }

            return text;
        }

        public static GameObject GetOrNewGameObject(this Component self, string path)
        {
            return GameObjectUtil.GetOrNewGameObject(path, self.gameObject);
        }
    }
}