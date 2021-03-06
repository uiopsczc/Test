using System.Collections.Generic;

namespace CsCat
{
  /// <summary>
  ///   提供嫁接缓存
  ///   如A类的实例a需要一个名字为fieldName的dictionary
  ///   可以使用Get<dictionary>(a  fieldName)获得  实际数据存放在本类的dict
  /// </summary>
  public class GlobalCache : Cache, ISingleton
  {
    #region property

    public static GlobalCache instance = SingletonFactory.instance.Get<GlobalCache>();

    #endregion

    #region field

    #endregion

    #region public method

    protected Dictionary<object, object> GetOwnerDict(object owner)
    {
      return dict.GetOrAddDefault(owner, () => { return new Dictionary<object, object>(); });
    }

    public T Get<T>(object owner, string field_name)
    {
      return GetOwnerDict(owner).GetOrGetDefault<T>(field_name);
    }


    public void Remove(object owner, string field_name = null)
    {
      Remove<object>(owner, field_name);
    }

    public T Remove<T>(object owner, string field_name = null)
    {
      if (dict.ContainsKey(owner))
      {
        if (field_name == null)
          return dict.Remove2<T>(owner);
        return GetOwnerDict(owner).Remove2<T>(field_name);
      }

      return default;
    }

    #endregion
  }
}