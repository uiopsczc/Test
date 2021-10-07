#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
    public partial class EditorGUIUtil
    {
        public EditorGUISetIconSizeScope SetIconSize(Vector2 newSize)
        {
            return new EditorGUISetIconSizeScope(newSize);
        }
    }
}
#endif