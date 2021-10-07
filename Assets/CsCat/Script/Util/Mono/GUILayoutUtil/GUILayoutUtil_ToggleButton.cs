using UnityEngine;

namespace CsCat
{
    public partial class GUILayoutUtil
    {
        public static bool ToggleButton(string label, bool value)
        {
            GUIStyle buttonStyle = StringConst.String_Button;
            if (GUILayout.Button(label,
                value
                    ? new GUIStyle(StringConst.String_Button) {normal = {background = buttonStyle.active.background}}
                    : StringConst.String_Button))
                value = !value;
            return value;
        }
    }
}