namespace CsCat
{
  public interface IPropNotify
  {
    /// <summary>
    /// 通知name属性更改了
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="old_value">更改前的值</param>
    /// <param name="new_value">更改后的值</param>
    void NotifyPropChanged(string property_name, object old_value, object new_value);
  }
}