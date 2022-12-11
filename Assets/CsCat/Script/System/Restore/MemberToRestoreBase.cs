namespace CsCat
{
	public abstract class MemberToRestoreBase : IRestore
	{
		#region field

		protected ToRestoreBase _toRestoreBase;

		/// <summary>
		/// 还原时候的用到的值
		/// </summary>
		protected object _valueToRestore;

		#endregion

		#region property

		public object cause
		{
			set => _toRestoreBase.cause = value;
			get => _toRestoreBase.cause;
		}

		#endregion


		#region ctor

		/// <summary>
		/// ctor
		/// </summary>
		/// <param name="cause">引起还原的原因</param>
		/// <param name="owner">需要还原的对象</param>
		/// <param name="nameToRestore">需要还原的属性名</param>
		protected MemberToRestoreBase(object cause, object owner, string nameToRestore)
		{
			_toRestoreBase = new ToRestoreBase(cause, owner, nameToRestore);
		}

		#endregion

		#region override method

		#region Equals

		public override bool Equals(object obj)
		{
			if (!(obj is MemberToRestoreBase other))
				return false;
			return other._toRestoreBase.Equals(_toRestoreBase);
		}

		public override int GetHashCode()
		{
			return _toRestoreBase.GetHashCode();
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