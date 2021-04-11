using System.Collections.Generic;


namespace CsCat
{
  /// <summary>
  /// 请使用 MemberToRestoreProxy  插入需要还原的数据
  /// </summary>
  public class RestoreManager : ISingleton
  {
    #region field

    /// <summary>
    /// 所有的需要还原的属性列表
    /// </summary>
    List<IRestore> restore_list = new List<IRestore>();

    /// <summary>
    /// 里面的元素用于还原后从restoreList中删除
    /// </summary>
    List<IRestore> to_remove_list = new List<IRestore>();

    #endregion

    #region property

    public static RestoreManager instance
    {
      get { return SingletonFactory.instance.Get<RestoreManager>(); }
    }

    #endregion

    #region public method

    /// <summary>
    /// 添加需要还原的restore
    /// 不会重复添加
    /// </summary>
    /// <param name="restore"></param>
    public void Add(IRestore restore)
    {
      if (restore_list.Contains(restore))
        return;
      restore_list.Add(restore);
    }

    /// <summary>
    /// 进行还原
    /// </summary>
    /// <param name="source"></param>
    public void Restore(object source)
    {
      to_remove_list.Clear();
      restore_list.ForEach((element) =>
      {
        if (element.Equals(source))
        {
          element.Restore();
          to_remove_list.Add(element);
        }
      });
      to_remove_list.ForEach((element) => { restore_list.Remove(element); });
      to_remove_list.Clear();
    }

    #endregion




  }
}
