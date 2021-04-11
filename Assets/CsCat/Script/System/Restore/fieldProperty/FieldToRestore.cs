using System.Reflection;

namespace CsCat
{
  public class FieldToRestore : MemberToRestoreBase
  {
    #region field

    public FieldInfo fieldInfo_to_restore;

    #endregion


    #region ctor

    /// <summary>
    ///   ctor
    /// </summary>
    /// <param name="cause">引起还原的原因</param>
    /// <param name="owner">需要还原的对象</param>
    /// <param name="propertyNameToRestore">需要还原的属性名</param>
    public FieldToRestore(object cause, object owner, string name_to_restore) : base(cause, owner, name_to_restore)
    {
      var type = owner.GetType();
      fieldInfo_to_restore = type.GetField(name_to_restore);
      value_to_restore = fieldInfo_to_restore.GetValue(owner);
    }

    #endregion


    #region public method

    /// <summary>
    ///   进行还原
    /// </summary>
    public override void Restore()
    {
      fieldInfo_to_restore.SetValue(toRestoreBase.owner, value_to_restore);
    }

    #endregion
  }
}