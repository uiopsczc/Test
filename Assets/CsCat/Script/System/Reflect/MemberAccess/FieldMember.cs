using System;
using System.Reflection;

namespace CsCat
{
	/// <summary>
	///   FieldMember
	/// </summary>
	public sealed class FieldMember : MemberAccessor
	{
		#region field

		private readonly FieldInfo _fieldInfo;

		#endregion

		#region ctor

		public FieldMember(FieldInfo fieldInfo)
		{
			this._fieldInfo = fieldInfo;
			getter = fieldInfo.GetValue; //设置getter方法
			setter = fieldInfo.SetValue; //设置setter方法
		}

		#endregion

		#region property

		/// <summary>
		///   该属性类型
		/// </summary>
		public override Type memberType => _fieldInfo.FieldType;

		/// <summary>
		///   该属性的信息
		/// </summary>
		public override MemberInfo memberInfo => _fieldInfo;

		#endregion
	}
}