using System.Reflection;

namespace CsCat
{
  public abstract class MethodToRestoreBase : MemberToRestoreBase
  {
    #region field

    protected MethodInfo methodInfo_to_restore;

    #endregion


    #region ctor

    /// <summary>
    ///   ctor
    /// </summary>
    /// <param name="cause">引起还原的对应的名称</param>
    /// <param name="owner">需要还原的方法</param>
    /// <param name="methodNameToRestore">需要还原的方法名</param>
    public MethodToRestoreBase(object cause, object owner, string name_to_restore) : base(cause, owner, name_to_restore)
    {
      toRestoreBase = new ToRestoreBase(cause, owner, name_to_restore);
    }

    #endregion
  }
}