using System.Collections.Generic;

namespace CsCat
{
  /// <summary>
  ///   用于保存信息
  /// </summary>
  public class ObjectInfos
  {
    public List<object> list = new List<object>();

    public ObjectInfos(params object[] args)
    {
      if (args != null)
        foreach (var arg in args)
          list.Add(arg);
    }

    public override bool Equals(object obj)
    {
      var other = (ObjectInfos) obj;
      var other_list = other.list;
      if (list.Count != other_list.Count)
        return false;
      for (var i = 0; i < list.Count; i++)
        if (!ObjectUtil.Equals(list[i], other_list[i]))
          return false;
      return true;
    }

    public override int GetHashCode()
    {
      return ObjectUtil.GetHashCode(list.ToArray());
    }
  }
}