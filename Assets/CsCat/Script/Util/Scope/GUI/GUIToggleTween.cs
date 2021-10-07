using System;
using UnityEngine;

namespace CsCat
{
    public class GUIToggleTween
    {
        public bool isOpened = true;
        public float value = 1;

        [NonSerialized] public Rect lastRect;

        [NonSerialized] public float lastUpdateTime;

        /** Update the visibility in Layout to avoid complications with different events not drawing the same thing */
        bool _isNeedToShow = true;

        public bool isNeedToShow
        {
            get
            {
                if (Event.current.type == EventType.Layout)
                    _isNeedToShow = isOpened || value > 0F;
                return _isNeedToShow;
            }
        }

        public static implicit operator bool(GUIToggleTween tween)
        {
            return tween.isNeedToShow;
        }
    }
}