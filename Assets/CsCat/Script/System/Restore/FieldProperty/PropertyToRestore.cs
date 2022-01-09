using System.Reflection;

namespace CsCat
{
	public class PropertyToRestore : MemberToRestoreBase
	{
		#region field

		public PropertyInfo propertyInfoToRestore;

		#endregion

		#region ctor

		/// <summary>
		///   ctor
		/// </summary>
		/// <param name="cause">引起还原的原因</param>
		/// <param name="owner">需要还原的对象</param>
		/// <param name="propertyNameToRestore">需要还原的属性名</param>
		public PropertyToRestore(object cause, object owner, string nameToRestore) : base(cause, owner,
			nameToRestore)
		{
			var type = owner.GetType();
			propertyInfoToRestore = type.GetProperty(nameToRestore);
			valueToRestore = propertyInfoToRestore.GetValue(owner, null);
		}

		#endregion

		#region public method

		/// <summary>
		///   进行还原
		/// </summary>
		public override void Restore()
		{
			propertyInfoToRestore.SetValue(toRestoreBase.owner, valueToRestore, null);
		}

		#endregion
	}
}