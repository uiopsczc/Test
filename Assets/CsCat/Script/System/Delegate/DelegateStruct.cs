using System;

namespace CsCat
{
	/// <summary>
	/// 如何将Delegate作为参数   使用lamba表达式,()=>{} 作为参数要将该delegate设置为Callback等，如 (Action)(()=>{LogCat.Log("helloWorld");})
	/// </summary>
	public class DelegateStruct
	{
		public Delegate delegation;
		public object[] args;


		public DelegateStruct(Delegate delegation, params object[] args)
		{
			this.delegation = delegation;
			this.args = args;
		}


		public object Call()
		{
			return delegation.DynamicInvoke(args);
		}
	}
}