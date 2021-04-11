

namespace CsCat
{
  public class AutoValueUtil
  {
    public static AutoSetValue<T> SetValue<T>(ref T pre_value, T post_value)
    {
      return AutoSetValue.SetValue(ref pre_value, post_value);
    }




  }

}
