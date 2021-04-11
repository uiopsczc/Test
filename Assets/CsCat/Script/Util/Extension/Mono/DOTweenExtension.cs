using DG.Tweening;
using UnityEngine;

namespace CsCat
{
  public static class DOTweenExtension
  {

    public static Tween SetDOTweenId(this Tween self, object obj_of_DOTweenId = null)
    {
      obj_of_DOTweenId = obj_of_DOTweenId ?? self.target;

      return self.SetId(obj_of_DOTweenId.DOTweenId());
    }

    public static Tweener DOBlendableLocalMoveXBy(this Transform target, float by_value, float duration,
      bool snapping = false)
    {
      return target.DOBlendableLocalMoveBy(
        new Vector3(by_value, 0, 0), duration,
        snapping);
    }

    public static Tweener DOBlendableLocalMoveYBy(this Transform target, float by_value, float duration,
      bool snapping = false)
    {
      return target.DOBlendableLocalMoveBy(
        new Vector3(0, by_value, 0), duration,
        snapping);
    }

    public static Tweener DOBlendableLocalMoveZBy(this Transform target, float by_value, float duration,
      bool snapping = false)
    {
      return target.DOBlendableLocalMoveBy(
        new Vector3(0, 0, by_value), duration,
        snapping);
    }


    public static Tweener DOBlendableMoveXBy(this Transform target, float by_value, float duration,
      bool snapping = false)
    {
      return target.DOBlendableMoveBy(
        new Vector3(by_value, 0, 0), duration,
        snapping);
    }

    public static Tweener DOBlendableMoveYBy(this Transform target, float by_value, float duration,
      bool snapping = false)
    {
      return target.DOBlendableMoveBy(
        new Vector3(0, by_value, 0), duration,
        snapping);
    }

    public static Tweener DOBlendableMoveZBy(this Transform target, float by_value, float duration,
      bool snapping = false)
    {
      return target.DOBlendableMoveBy(
        new Vector3(0, 0, by_value), duration,
        snapping);
    }

  }
}