namespace CsCat
{
  public class AutoSetValue
  {
    public static AutoSetValue<T> SetValue<T>(ref T pre_value, T post_value)
    {
      var self = new AutoSetValue<T>();
      self.pre_value = pre_value;
      self.post_value = post_value;
      pre_value = post_value;
      return self;
    }
  }
}