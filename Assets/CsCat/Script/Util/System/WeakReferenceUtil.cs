using System;

namespace CsCat
{
  public class WeakReferenceUtil
  {
    #region WeakReference<V>

    //public static ValueResult<V> GetValueResult<V>(WeakReference<V> weakReference)where V:class
    //{
    //	V v;
    //	if(weakReference.TryGetTarget(out v))
    //	{
    //		return new ValueResult<V>(true,v);
    //	}
    //	else
    //	{
    //		return new ValueResult<V>(false, default(V));
    //	}
    //}
    //public static V GetValue<V>(WeakReference<V> weakReference) where V : class
    //{
    //	V v;
    //	if (weakReference.TryGetTarget(out v))
    //	{
    //		return v;
    //	}
    //	else
    //	{
    //		return default(V);
    //	}
    //}

    #endregion


    #region WeakReference

    public static ValueResult<V> GetValueResult<V>(WeakReference weakReference)
    {
      if (weakReference.IsAlive)
      {
        return new ValueResult<V>(true, (V)weakReference.Target);
      }
      else
      {
        return new ValueResult<V>(false, default(V));
      }
    }

    public static V GetValue<V>(WeakReference weakReference)
    {
      if (weakReference.IsAlive)
      {
        return (V)weakReference.Target;
      }
      else
      {
        return default(V);
      }
    }

    #endregion


  }
}