

using UnityEngine;

namespace CsCat
{
  public class RectTransformUtil
  {
    #region

    public static void SetAnchoredPositionX(RectTransform rectTransform, float value)
    {
      rectTransform.anchoredPosition = new Vector2(value, rectTransform.anchoredPosition.y);
    }

    public static void SetAnchoredPositionY(RectTransform rectTransform, float value)
    {
      rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, value);
    }

    public static void SetAnchoredPositionZ(RectTransform rectTransform, float value)
    {
      rectTransform.anchoredPosition3D =
        new Vector3(rectTransform.anchoredPosition3D.x, rectTransform.anchoredPosition3D.y, value);
    }

    public static void SetSizeDeltaX(RectTransform rectTransform, float value)
    {
      rectTransform.sizeDelta = new Vector2(value, rectTransform.sizeDelta.y);
    }

    public static void SetSizeDeltaY(RectTransform rectTransform, float value)
    {

      rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, value);
    }

    #endregion

    public static void CopyTo(RectTransform source, RectTransform target)
    {
      target.anchoredPosition = source.anchoredPosition;
      target.anchoredPosition3D = source.anchoredPosition3D;
      target.anchorMax = source.anchorMax;
      target.anchorMin = source.anchorMin;
      target.offsetMax = source.offsetMax;
      target.offsetMin = source.offsetMin;
      target.pivot = source.pivot;
      target.sizeDelta = source.sizeDelta;
      target.localRotation = source.localRotation;
      target.localScale = source.localScale;
    }
  }
}