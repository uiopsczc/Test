using System;

namespace CsCat
{
  public class MethodToRestoreWithArgs : MethodToRestoreBase
  {
    #region field

    /// <summary>
    ///   需要还原的方法的参数
    /// </summary>
    public object[] method_args_to_restore;

    #endregion

    #region ctor

    /// <summary>
    ///   ctor
    /// </summary>
    /// <param name="cause">引起还原的对应的名称</param>
    /// <param name="owner">需要还原的方法</param>
    /// <param name="method_name_to_restore">需要还原的方法名</param>
    /// <param name="method_args_to_restore">需要还原的方法的参数</param>
    public MethodToRestoreWithArgs(object cause, object owner, string method_name_to_restore,
      params object[] method_args_to_restore) : base(cause, owner, method_name_to_restore)
    {
      this.method_args_to_restore = method_args_to_restore;
      var type = owner.GetType();
      var arg_types = new Type[method_args_to_restore.Length];
      for (var i = 0; i < method_args_to_restore.Length; i++)
        arg_types[i] = method_args_to_restore[i].GetType();
      methodInfo_to_restore = type.GetMethodInfo(method_name_to_restore, BindingFlagsConst.All, arg_types);
    }

    #endregion

    #region public method

    /// <summary>
    ///   进行还原
    /// </summary>
    public override void Restore()
    {
      methodInfo_to_restore.Invoke(toRestoreBase.owner, method_args_to_restore);
    }

    #endregion
  }
}