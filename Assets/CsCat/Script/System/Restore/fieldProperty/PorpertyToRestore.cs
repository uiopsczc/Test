using System.Reflection;

namespace CsCat
{
  public class PorpertyToRestore : MemberToRestoreBase
  {
    #region field

    public PropertyInfo propertyInfo_to_restore;

    #endregion

    #region ctor

    /// <summary>
    ///   ctor
    /// </summary>
    /// <param name="cause">引起还原的原因</param>
    /// <param name="owner">需要还原的对象</param>
    /// <param name="propertyNameToRestore">需要还原的属性名</param>
    public PorpertyToRestore(object cause, object owner, string name_to_restore) : base(cause, owner, name_to_restore)
    {
      var type = owner.GetType();
      propertyInfo_to_restore = type.GetProperty(name_to_restore);
      value_to_restore = propertyInfo_to_restore.GetValue(owner, null);
    }

    #endregion

    #region public method

    /// <summary>
    ///   进行还原
    /// </summary>
    public override void Restore()
    {
      propertyInfo_to_restore.SetValue(toRestoreBase.owner, value_to_restore, null);
    }

    #endregion
  }
}