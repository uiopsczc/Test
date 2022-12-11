using System;
using System.Reflection;
using UnityEngine;

namespace CsCat
{
	/// <summary>
	/// 测试AOPAttriubte属性   ，所有AOPAttribute都应以AOP_开头
	/// AOPAttriubte  如果有特殊的的情况则优先用特殊情况，否则用默认的该接口的IAOPAttribute实现
	/// 特殊情况调用优先顺序
	/// 1.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型(【被切面的方法的类本身】+被切面方法的参数)
	///   1.1.被切面的方法的名称_AOPMethodType的类型（【被切面的方法的类本身】+被切面方法的参数）
	/// 2.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型(被切面方法的参数) 
	///   2.1.被切面的方法的名称_AOPMethodType的类型（被切面方法的参数）
	/// 3.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型(【被切面的方法的类本身】)
	///   3.1.被切面的方法的名称_AOPMethodType的类型（【被切面的方法的类本身】）
	/// 4.被切面的方法的名称_AOPMethodType的类型（）
	///   4.1.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型()
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class AOP_TestAttribute : Attribute, IAOPAttribute
	{
		#region field

		private readonly string _desc;

		#endregion

		#region ctor

		public AOP_TestAttribute(string desc)
		{
			this._desc = desc;
		}

		#endregion

		#region public method

		/// <summary>
		/// 特殊切面前处理
		/// </summary>
		/// <param name="self"></param>
		/// <param name="message"></param>
		public void CallHello_AOP_Handle_Pre(AOPExample self, string message, Shape2D shape)
		{
			LogCat.LogError("AOP:" + shape.ToString());
		}



		/// <summary>
		/// 默认切面前处理
		/// </summary>
		public void Pre_AOP_Handle()
		{
			LogCat.LogWarning("Before :" + _desc + " ");
		}

		/// <summary>
		/// 默认切面后处理
		/// </summary>
		public void Post_AOP_Handle()
		{
			LogCat.LogWarning("After :" + _desc);
		}

		#endregion

	}
}