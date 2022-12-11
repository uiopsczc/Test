using System;

namespace CsCat
{
	public class MethodToRestoreWithArgs : MethodToRestoreBase
	{
		#region field

		/// <summary>
		///   需要还原的方法的参数
		/// </summary>
		public object[] methodArgsToRestore;

		#endregion

		#region ctor

		/// <summary>
		///   ctor
		/// </summary>
		/// <param name="cause">引起还原的对应的名称</param>
		/// <param name="owner">需要还原的方法</param>
		/// <param name="methodNameToRestore">需要还原的方法名</param>
		/// <param name="methodArgsToRestore">需要还原的方法的参数</param>
		public MethodToRestoreWithArgs(object cause, object owner, string methodNameToRestore,
			params object[] methodArgsToRestore) : base(cause, owner, methodNameToRestore)
		{
			this.methodArgsToRestore = methodArgsToRestore;
			var type = owner.GetType();
			var argTypes = new Type[methodArgsToRestore.Length];
			for (var i = 0; i < methodArgsToRestore.Length; i++)
				argTypes[i] = methodArgsToRestore[i].GetType();
			_methodInfoToRestore = type.GetMethodInfo(methodNameToRestore, BindingFlagsConst.All, argTypes);
		}

		#endregion

		#region public method

		/// <summary>
		///   进行还原
		/// </summary>
		public override void Restore()
		{
			_methodInfoToRestore.Invoke(toRestoreBase.owner, methodArgsToRestore);
		}

		#endregion
	}
}