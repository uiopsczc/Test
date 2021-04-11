namespace CsCat
{
  public class ToRestoreBase
  {
    #region field

    /// <summary>
    /// 需要还原的对象
    /// </summary>
    public object owner;

    /// <summary>
    /// 引起还原的原因
    /// </summary>
    public object cause;

    /// <summary>
    /// 需要还原的属性名
    /// </summary>
    public string name_to_restore;

    #endregion



    #region ctor

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="cause">引起还原的对应的名称</param>
    /// <param name="owner">需要还原的对象</param>
    public ToRestoreBase(object cause, object owner, string name_to_restore)
    {
      this.cause = cause;
      this.owner = owner;
      this.name_to_restore = name_to_restore;
    }

    #endregion


    #region override method

    #region Equals

    public override bool Equals(object obj)
    {
      if (!(obj is ToRestoreBase))
        return false;
      ToRestoreBase other = (ToRestoreBase) obj;
      if (other.owner == owner && other.name_to_restore.Equals(name_to_restore))
        return true;
      else
        return false;
    }

    public override int GetHashCode()
    {
      return ObjectUtil.GetHashCode(owner, name_to_restore);
    }

    #endregion

    #endregion

  }
}
