

using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  public class TextUtil
  {
    private const string Text_Is_Gray_Key = "Text_Is_Gray";
    private const string Text_Orgin_Color_Of_Gray_Key = "Text_Orgin_Color_Of_Gray";

    public static void SetIsGray(Text text, bool is_gray)
    {
      if (text == null)
        return;
      var monoBehaviourCache = text.GetMonoBehaviourCache();
      bool text_is_gray = monoBehaviourCache.GetOrGetDefault(Text_Is_Gray_Key, () => false);
      if (text_is_gray != is_gray)
      {
        if (is_gray)
        {
          monoBehaviourCache[Text_Orgin_Color_Of_Gray_Key] = text.color;
          text.color = Color.gray;
        }
        else
          text.color = monoBehaviourCache.Get<Color>(Text_Orgin_Color_Of_Gray_Key);
        monoBehaviourCache[Text_Is_Gray_Key] = is_gray;
      }
    }

    public static void SetAlpha(Text text, float alpha)
    {
      text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }

    public static void SetColor(Text text, Color color, bool is_not_use_color_alpha = false)
    {
      text.color = new Color(color.r, color.g, color.b, is_not_use_color_alpha ? text.color.a : color.a);
    }
  }
}