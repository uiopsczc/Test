using System;
using System.Reflection;

namespace CsCat
{
  public class MemberToRestoreProxy : IRestore
  {

    #region field

    private MemberToRestoreBase memberToRestoreBase;

    #endregion

    #region property

    public object cause
    {
      set { memberToRestoreBase.cause = value; }
      get { return memberToRestoreBase.cause; }
    }

    #endregion


    #region ctor

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="cause">引起还原的原因</param>
    /// <param name="owner">需要还原的对象</param>
    /// <param name="name_to_restore">需要还原的属性名</param>
    /// <param name="method_args_to_restore">需要还原的方法的参数</param>
    public MemberToRestoreProxy(object cause, object owner, string name_to_restore,
      params object[] method_args_to_restore)
    {
      Type type = owner.GetType();
      MemberInfo memberInfo = type.GetMember(name_to_restore)[0];
      MemberTypes memberType = memberInfo.MemberType;
      if (memberType == MemberTypes.Field)
        memberToRestoreBase = new FieldToRestore(cause, owner, name_to_restore);
      if (memberType == MemberTypes.Property)
        memberToRestoreBase = new PorpertyToRestore(cause, owner, name_to_restore);
      if (memberType == MemberTypes.Method)
      {
        if (method_args_to_restore != null && method_args_to_restore.Length > 0)
          memberToRestoreBase = new MethodToRestoreWithArgs(cause, owner, name_to_restore, method_args_to_restore);
        else
          memberToRestoreBase = new MethodToRestoreWithoutArgs(cause, owner, name_to_restore);
      }

      throw new Exception(string.Format("can not handle memberType({0}) of memberName({1})", memberType,
        name_to_restore));
    }

    #endregion

    #region override method

    #region Equals

    public override bool Equals(object obj)
    {
      var other_proxy = obj as MemberToRestoreProxy;
      if (other_proxy != null)
      {
        return other_proxy.memberToRestoreBase.Equals(memberToRestoreBase);
      }

      var other_toRestoreBase = obj as MemberToRestoreBase;
      if (other_toRestoreBase != null)
      {
        return other_toRestoreBase.Equals(memberToRestoreBase);
      }

      return false;
    }

    public override int GetHashCode()
    {
      return memberToRestoreBase.GetHashCode();
    }

    #endregion

    #endregion

    #region public method

    /// <summary>
    /// 进行还原
    /// </summary>
    public void Restore()
    {
      memberToRestoreBase.Restore();
    }

    #endregion
  }
}