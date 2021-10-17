using DG.Tweening;
using UnityEngine;

namespace CsCat
{
    public static class DOTweenExtension
    {
        public static Tween SetDOTweenId(this Tween self, object objOfDOTweenId = null)
        {
            objOfDOTweenId = objOfDOTweenId ?? self.target;

            return self.SetId(objOfDOTweenId.DOTweenId());
        }

        public static Tweener DOBlendableLocalMoveXBy(this Transform target, float byValue, float duration,
            bool snapping = false)
        {
            return target.DOBlendableLocalMoveBy(
                new Vector3(byValue, 0, 0), duration,
                snapping);
        }

        public static Tweener DOBlendableLocalMoveYBy(this Transform target, float byValue, float duration,
            bool snapping = false)
        {
            return target.DOBlendableLocalMoveBy(
                new Vector3(0, byValue, 0), duration,
                snapping);
        }

        public static Tweener DOBlendableLocalMoveZBy(this Transform target, float byValue, float duration,
            bool snapping = false)
        {
            return target.DOBlendableLocalMoveBy(
                new Vector3(0, 0, byValue), duration,
                snapping);
        }


        public static Tweener DOBlendableMoveXBy(this Transform target, float byValue, float duration,
            bool snapping = false)
        {
            return target.DOBlendableMoveBy(
                new Vector3(byValue, 0, 0), duration,
                snapping);
        }

        public static Tweener DOBlendableMoveYBy(this Transform target, float byValue, float duration,
            bool snapping = false)
        {
            return target.DOBlendableMoveBy(
                new Vector3(0, byValue, 0), duration,
                snapping);
        }

        public static Tweener DOBlendableMoveZBy(this Transform target, float byValue, float duration,
            bool snapping = false)
        {
            return target.DOBlendableMoveBy(
                new Vector3(0, 0, byValue), duration,
                snapping);
        }
    }
}