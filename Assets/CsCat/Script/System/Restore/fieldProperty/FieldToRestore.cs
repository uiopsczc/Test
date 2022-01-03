using System.Reflection;

namespace CsCat
{
	public class FieldToRestore : MemberToRestoreBase
	{
		#region field

		public FieldInfo fieldInfoToRestore;

		#endregion


		#region ctor

		/// <summary>
		///   ctor
		/// </summary>
		/// <param name="cause">引起还原的原因</param>
		/// <param name="owner">需要还原的对象</param>
		/// <param name="propertyNameToRestore">需要还原的属性名</param>
		public FieldToRestore(object cause, object owner, string nameToRestore) : base(cause, owner, nameToRestore)
		{
			var type = owner.GetType();
			fieldInfoToRestore = type.GetField(nameToRestore);
			valueToRestore = fieldInfoToRestore.GetValue(owner);
		}

		#endregion


		#region public method

		/// <summary>
		///   进行还原
		/// </summary>
		public override void Restore()
		{
			fieldInfoToRestore.SetValue(toRestoreBase.owner, valueToRestore);
		}

		#endregion
	}
}