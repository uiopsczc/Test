using System;

namespace CsCat
{
	/// <summary>
	/// 如何将Delegate作为参数   使用lamba表达式,()=>{} 作为参数要将该delegate设置为Callback等，如 (Action)(()=>{LogCat.Log("helloWorld");})
	/// </summary>
	public class DelegateStruct
	{
		public Delegate _delegation;
		public object[] _args;


		public DelegateStruct(Delegate delegation, params object[] args)
		{
			this._delegation = delegation;
			this._args = args;
		}


		public object Invoke()
		{
			return _delegation.DynamicInvoke(_args);
		}
	}
}