using System;
using System.Reflection;
using UnityEngine;

namespace CsCat
{
    public static class GUIStyleExtension
    {
        public static GUIStyle Append(this GUIStyle self, Action<GUIStyle> appendCallback)
        {
            appendCallback(self);
            return self;
        }

        public static GUIStyle Clone(this GUIStyle self)
        {
            return new GUIStyle(self);
        }

        public static GUIStyle SetFontSize(this GUIStyle self, int fontSize)
        {
            self.fontSize = fontSize;
            return self;
        }

        public static GUIStyle SetFontStyle(this GUIStyle self, FontStyle fontStyle)
        {
            self.fontStyle = fontStyle;
            return self;
        }

        public static GUIStyle SetRichText(this GUIStyle self, bool isRichText)
        {
            self.richText = isRichText;
            return self;
        }

        public static GUIStyle SetTextAnchor(this GUIStyle self, TextAnchor textAnchor)
        {
            self.alignment = textAnchor;
            return self;
        }

        public static GUIStyle SetFixedHeight(this GUIStyle self, float fixedHeight)
        {
            self.fixedHeight = fixedHeight;
            return self;
        }

        public static GUIStyle SetFixedWidth(this GUIStyle self, float fixedWidth)
        {
            self.fixedWidth = fixedWidth;
            return self;
        }

        public static GUIStyle SetName(this GUIStyle self, string name)
        {
            self.name = name;
            return self;
        }

        public static GUIStyle SetName(this GUIStyle self, GUIStyle anotherStyle)
        {
            return SetName(self, anotherStyle.name);
        }

        public static GUIStyle SetPadding(this GUIStyle self, RectOffset padding)
        {
            self.padding = padding;
            return self;
        }

        public static GUIStyle SetBorder(this GUIStyle self, RectOffset border)
        {
            self.border = border;
            return self;
        }

        public static GUIStyle SetClipping(this GUIStyle self, TextClipping clipping)
        {
            self.clipping = clipping;
            return self;
        }

        public static GUIStyle SetContentOffset(this GUIStyle self, Vector2 contentOffset)
        {
            self.contentOffset = contentOffset;
            return self;
        }

        public static GUIStyle SetImagePosition(this GUIStyle self, ImagePosition imagePosition)
        {
            self.imagePosition = imagePosition;
            return self;
        }

        public static GUIStyle SetMargin(this GUIStyle self, RectOffset margin)
        {
            self.margin = margin;
            return self;
        }

        public static GUIStyle SetStretchHeight(this GUIStyle self, bool stretchHeight)
        {
            self.stretchHeight = stretchHeight;
            return self;
        }

        public static GUIStyle SetStretchWidth(this GUIStyle self, bool stretchWidth)
        {
            self.stretchWidth = stretchWidth;
            return self;
        }


        public static GUIStyle SetWordWrap(this GUIStyle self, bool wordWrap)
        {
            self.wordWrap = wordWrap;
            return self;
        }

        public static GUIStyle SetOverflow(this GUIStyle self, RectOffset overflow)
        {
            self.overflow = overflow;
            return self;
        }
    }
}