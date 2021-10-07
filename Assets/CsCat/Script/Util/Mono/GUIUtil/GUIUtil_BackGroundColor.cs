using UnityEngine;

namespace CsCat
{
    public partial class GUIUtil
    {
        public static GUIBackgroundColorScope BackgroundColor(Color newColor)
        {
            return new GUIBackgroundColorScope(newColor);
        }
    }
}