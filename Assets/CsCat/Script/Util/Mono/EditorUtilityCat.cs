#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CsCat
{
    public class EditorUtilityCat
    {
        public static void DisplayDialog(string message, string copyContent = null)
        {
            EditorUtility.DisplayDialog("", message, "确定");
            Debug.Log(message);
            if (copyContent.IsNullOrWhiteSpace())
                GUIUtility.systemCopyBuffer = copyContent;
        }
    }
}
#endif