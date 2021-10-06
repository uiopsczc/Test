using System;

namespace CsCat
{
    public class WeakReferenceUtil
    {
        #region WeakReference

        public static ValueResult<V> GetValueResult<V>(WeakReference weakReference) => weakReference.IsAlive ? new ValueResult<V>(true, (V) weakReference.Target) : new ValueResult<V>(false, default(V));

        public static V GetValue<V>(WeakReference weakReference) => weakReference.IsAlive ? (V) weakReference.Target : default(V);

        #endregion
    }
}