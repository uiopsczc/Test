using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
    public class TextUtil
    {
        private const string Text_Is_Gray_Key = "Text_Is_Gray";
        private const string Text_Origin_Color_Of_Gray_Key = "Text_Origin_Color_Of_Gray";

        public static void SetIsGray(Text text, bool isGray)
        {
            if (text == null)
                return;
            var monoBehaviourCache = text.GetMonoBehaviourCache();
            bool textIsGray = monoBehaviourCache.GetOrGetDefault(Text_Is_Gray_Key, () => false);
            if (textIsGray == isGray) return;
            if (isGray)
            {
                monoBehaviourCache[Text_Origin_Color_Of_Gray_Key] = text.color;
                text.color = text.color.ToGray();
            }
            else
                text.color = monoBehaviourCache.Get<Color>(Text_Origin_Color_Of_Gray_Key);

            monoBehaviourCache[Text_Is_Gray_Key] = isGray;
        }

        public static void SetAlpha(Text text, float alpha)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        }

        public static void SetColor(Text text, Color color, bool isNotUseColorAlpha = false)
        {
            text.color = new Color(color.r, color.g, color.b, isNotUseColorAlpha ? text.color.a : color.a);
        }
    }
}