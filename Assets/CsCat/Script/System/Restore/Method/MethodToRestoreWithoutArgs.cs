namespace CsCat
{
  public class MethodToRestoreWithoutArgs : MethodToRestoreBase
  {
    #region ctor

    /// <summary>
    ///   ctor
    /// </summary>
    /// <param name="cause">引起还原的对应的名称</param>
    /// <param name="owner">需要还原的方法</param>
    /// <param name="method_name_to_restore">需要还原的方法名</param>
    public MethodToRestoreWithoutArgs(object cause, object owner, string method_name_to_restore) : base(cause, owner,
      method_name_to_restore)
    {
      var type = owner.GetType();
      methodInfo_to_restore = type.GetMethodInfo2(method_name_to_restore);
    }

    #endregion

    #region public method

    /// <summary>
    ///   进行还原
    /// </summary>
    public override void Restore()
    {
      methodInfo_to_restore.Invoke(toRestoreBase.owner, null);
    }

    #endregion
  }
}