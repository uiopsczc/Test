using System;

namespace CsCat
{
    public static class WeakReferenceExtension
    {
        #region WeakReference

        public static ValueResult<V> GetValueResult<V>(this WeakReference self)
        {
            return WeakReferenceUtil.GetValueResult<V>(self);
        }

        public static V GetValue<V>(this WeakReference self)
        {
            return WeakReferenceUtil.GetValue<V>(self);
        }

        #endregion
    }
}