namespace CsCat
{
  public abstract class MemberToRestoreBase : IRestore
  {

    #region field

    protected ToRestoreBase toRestoreBase;

    /// <summary>
    /// 还原时候的用到的值
    /// </summary>
    protected object value_to_restore;

    #endregion

    #region property

    public object cause
    {
      set { toRestoreBase.cause = value; }
      get { return toRestoreBase.cause; }
    }

    #endregion


    #region ctor

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="cause">引起还原的原因</param>
    /// <param name="owner">需要还原的对象</param>
    /// <param name="name_to_restore">需要还原的属性名</param>
    protected MemberToRestoreBase(object cause, object owner, string name_to_restore)
    {
      toRestoreBase = new ToRestoreBase(cause, owner, name_to_restore);

    }

    #endregion

    #region override method

    #region Equals

    public override bool Equals(object obj)
    {
      if (!(obj is MemberToRestoreBase))
        return false;
      MemberToRestoreBase other = (MemberToRestoreBase) obj;
      return other.toRestoreBase.Equals(toRestoreBase);
    }

    public override int GetHashCode()
    {
      return toRestoreBase.GetHashCode();
    }

    #endregion

    #endregion

    #region public method

    /// <summary>
    /// 进行还原
    /// </summary>
    public abstract void Restore();

    #endregion
  }
}