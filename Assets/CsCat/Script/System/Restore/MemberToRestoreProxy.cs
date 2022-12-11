using System;
using System.Reflection;

namespace CsCat
{
	public class MemberToRestoreProxy : IRestore
	{
		#region field

		private readonly MemberToRestoreBase _memberToRestoreBase;

		#endregion

		#region property

		public object cause
		{
			set => _memberToRestoreBase.cause = value;
			get => _memberToRestoreBase.cause;
		}

		#endregion


		#region ctor

		/// <summary>
		/// ctor
		/// </summary>
		/// <param name="cause">引起还原的原因</param>
		/// <param name="owner">需要还原的对象</param>
		/// <param name="nameToRestore">需要还原的属性名</param>
		/// <param name="methodArgsToRestore">需要还原的方法的参数</param>
		public MemberToRestoreProxy(object cause, object owner, string nameToRestore,
			params object[] methodArgsToRestore)
		{
			Type type = owner.GetType();
			MemberInfo memberInfo = type.GetMember(nameToRestore)[0];
			MemberTypes memberType = memberInfo.MemberType;
			if (memberType == MemberTypes.Field)
				_memberToRestoreBase = new FieldToRestore(cause, owner, nameToRestore);
			if (memberType == MemberTypes.Property)
				_memberToRestoreBase = new PropertyToRestore(cause, owner, nameToRestore);
			if (memberType == MemberTypes.Method)
			{
				if (methodArgsToRestore != null && methodArgsToRestore.Length > 0)
					_memberToRestoreBase =
						new MethodToRestoreWithArgs(cause, owner, nameToRestore, methodArgsToRestore);
				else
					_memberToRestoreBase = new MethodToRestoreWithoutArgs(cause, owner, nameToRestore);
			}

			throw new Exception(string.Format("can not handle memberType({0}) of memberName({1})", memberType,
				nameToRestore));
		}

		#endregion

		#region override method

		#region Equals

		public override bool Equals(object obj)
		{
			if (obj is MemberToRestoreProxy otherProxy)
				return otherProxy._memberToRestoreBase.Equals(_memberToRestoreBase);

			if (obj is MemberToRestoreBase otherToRestoreBase)
				return otherToRestoreBase.Equals(_memberToRestoreBase);

			return false;
		}

		public override int GetHashCode()
		{
			return _memberToRestoreBase.GetHashCode();
		}

		#endregion

		#endregion

		#region public method

		/// <summary>
		/// 进行还原
		/// </summary>
		public void Restore()
		{
			_memberToRestoreBase.Restore();
		}

		#endregion
	}
}