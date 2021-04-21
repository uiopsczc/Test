

using UnityEngine;

namespace CsCat
{
  public static class RectTransformExtension
  {
    #region

    public static void SetAnchoredPositionX(this RectTransform self, float value)
    {
      RectTransformUtil.SetAnchoredPositionX(self, value);
    }

    public static void SetAnchoredPositionY(this RectTransform self, float value)
    {
      RectTransformUtil.SetAnchoredPositionY(self, value);
    }

    public static void SetAnchoredPositionZ(this RectTransform self, float value)
    {
      RectTransformUtil.SetAnchoredPositionZ(self, value);
    }

    public static void SetSizeDeltaX(this RectTransform self, float value)
    {
      RectTransformUtil.SetSizeDeltaX(self, value);
    }

    public static void SetSizeDeltaY(this RectTransform self, float value)
    {

      RectTransformUtil.SetSizeDeltaY(self, value);
    }

    /// <summary>
    /// 设置 anchorMin.x
    /// </summary>
    public static void SetAnchorMinX(this RectTransform self, float x)
    {
      var anchorMin = self.anchorMin;
      anchorMin.x = x;
      self.anchorMin = anchorMin;
    }


    /// <summary>
    /// 设置 anchorMin.y
    /// </summary>
    public static void SetAnchorMinY(this RectTransform self, float y)
    {
      var anchorMin = self.anchorMin;
      anchorMin.y = y;
      self.anchorMin = anchorMin;
    }


    /// <summary>
    /// 设置 anchorMax.x
    /// </summary>
    public static void SetAnchorMaxX(this RectTransform self, float x)
    {
      var anchorMax = self.anchorMax;
      anchorMax.x = x;
      self.anchorMax = anchorMax;
    }


    /// <summary>
    /// 设置 anchorMax.y
    /// </summary>
    public static void SetAnchorMaxY(this RectTransform self, float y)
    {
      var anchorMax = self.anchorMax;
      anchorMax.y = y;
      self.anchorMax = anchorMax;
    }


    /// <summary>
    /// 设置 pivot.x
    /// </summary>
    public static void SetPivotX(this RectTransform self, float x)
    {
      var pivot = self.pivot;
      pivot.x = x;
      self.pivot = pivot;
    }


    /// <summary>
    /// 设置 pivot.y
    /// </summary>
    public static void SetPivotY(this RectTransform self, float y)
    {
      var pivot = self.pivot;
      pivot.y = y;
      self.pivot = pivot;
    }


    public static void SetLeft(this RectTransform self, float value)
    {
      self.offsetMin = new Vector2(value, self.offsetMin.y);
    }
    public static void SetRight(this RectTransform self, float value)
    {
      self.offsetMax = new Vector2(value, self.offsetMax.y);
    }

    public static void SetLeftRight(this RectTransform self, float left, float right)
    {
      self.SetLeft(left);
      self.SetRight(right);
    }

    public static void SetBottom(this RectTransform self, float value)
    {
      self.offsetMin = new Vector2(self.offsetMin.x, value);
    }
    public static void SetTop(this RectTransform self, float value)
    {
      self.offsetMax = new Vector2(self.offsetMax.x, value);
    }


    public static void SeBottomTop(this RectTransform self, float bottom, float top)
    {
      self.SetBottom(bottom);
      self.SetTop(top);
    }
    #endregion

    public static void CopyTo(this RectTransform source, RectTransform target)
    {
      RectTransformUtil.CopyTo(source, target);
    }

    public static void CopyFrom(this RectTransform source, RectTransform target)
    {
      RectTransformUtil.CopyTo(target, source);
    }



  }
}