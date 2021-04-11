using System;

namespace CsCat
{
  public class AutoSetValue<T>
  {
    public T post_value;

    public T pre_value;

    public AutoSetValue<T> IfChanged(Action<T, T> action)
    {
      if (!pre_value.Equals(post_value))
        action(pre_value, post_value);
      return this;
    }
  }
}