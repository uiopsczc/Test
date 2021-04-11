using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;


public static class AutoCompleteSearchFieldStyles
{
        public const float resultHeight = 20f;
        public const float resultsBorderWidth = 2f;
        public const float resultsMargin = 15f;
        public const float resultsLabelOffset = 2f;
   
        public static readonly GUIStyle entryEven;
        public static readonly GUIStyle entryOdd;
        public static readonly GUIStyle labelStyle;
        public static readonly GUIStyle resultsBorderStyle;


        static AutoCompleteSearchFieldStyles()
        {
            entryOdd = new GUIStyle("CN EntryBackOdd");
            entryEven = new GUIStyle("CN EntryBackEven");
            resultsBorderStyle = new GUIStyle("hostview");

            labelStyle = new GUIStyle(EditorStyles.label)
            {
                alignment = TextAnchor.MiddleLeft,
                richText = true
            };
        }
}