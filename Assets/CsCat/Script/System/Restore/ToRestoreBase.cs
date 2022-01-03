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
		public string nameToRestore;

		#endregion


		#region ctor

		/// <summary>
		/// ctor
		/// </summary>
		/// <param name="cause">引起还原的对应的名称</param>
		/// <param name="owner">需要还原的对象</param>
		public ToRestoreBase(object cause, object owner, string nameToRestore)
		{
			this.cause = cause;
			this.owner = owner;
			this.nameToRestore = nameToRestore;
		}

		#endregion


		#region override method

		#region Equals

		public override bool Equals(object obj)
		{
			if (!(obj is ToRestoreBase other))
				return false;
			return other.owner == owner && other.nameToRestore.Equals(nameToRestore);
		}

		public override int GetHashCode()
		{
			return ObjectUtil.GetHashCode(owner, nameToRestore);
		}

		#endregion

		#endregion
	}
}