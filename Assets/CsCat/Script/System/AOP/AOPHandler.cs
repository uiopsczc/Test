using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CsCat
{
	/// <summary>
	///   AOPHandle处理
	/// </summary>
	public class AOPHandler : ISingleton
	{
		/// <summary>
		///   所有类中aopMethod的缓存
		///   key是被切面的方法
		///   value是AOPAttributeMethodInfoProxyCategory列表
		///   AOPAttributeMethodInfoProxyCategory的东西可以参考AOPAttributeMethodInfoProxyCategory的类说明
		/// </summary>
		private readonly Dictionary<MethodBase, List<AOPAttributeMethodInfoProxyCategory>>
			aopAttributeMethodInfoProxyCategoryCacheDict =
				new Dictionary<MethodBase, List<AOPAttributeMethodInfoProxyCategory>>();


		public static AOPHandler instance => SingletonFactory.instance.Get<AOPHandler>();

		public void SingleInit()
		{
		}

		#region public method

		#region AOP_Handle

		/// <summary>
		///   前处理
		/// </summary>
		/// <param name="sourceMethodOwner"></param>
		/// <param name="sourceMethodBase"></param>
		/// <param name="sourceMethodArgs"></param>
		public void Pre_AOP_Handle(object sourceMethodOwner, MethodBase sourceMethodBase,
			params object[] sourceMethodArgs)
		{
			var aopAttributeMethodInfoProxyCategoryList =
				GetAOPAttributeMethodInfoProxyCategoryList(sourceMethodBase);
			aopAttributeMethodInfoProxyCategoryList.ForEach(
				aopAttributeMethodCategory =>
				{
					InvokeAOPAttributeMethod(aopAttributeMethodCategory, AOPMethodType.Pre_AOP_Handle,
						sourceMethodOwner,
						sourceMethodBase, sourceMethodArgs);
				}
			);
		}

		/// <summary>
		///   后处理
		/// </summary>
		/// <param name="sourceMethodOwner"></param>
		/// <param name="sourceMethodBase"></param>
		/// <param name="sourceMethodArgs"></param>
		public void Post_AOP_Handle(object sourceMethodOwner, MethodBase sourceMethodBase,
			params object[] sourceMethodArgs)
		{
			var aopAttributeMethodCategoryList = GetAOPAttributeMethodInfoProxyCategoryList(sourceMethodBase);
			aopAttributeMethodCategoryList.ForEach(
				aopAttributeMethodCategory =>
				{
					InvokeAOPAttributeMethod(aopAttributeMethodCategory, AOPMethodType.Post_AOP_Handle,
						sourceMethodOwner,
						sourceMethodBase, sourceMethodArgs);
				}
			);
		}

		#endregion

		/// <summary>
		///   获取特定的被切面的方法的AOPAttributeMethodInfoProxyCategoryList
		/// </summary>
		/// <param name="sourceMethodBase"></param>
		/// <returns></returns>
		private List<AOPAttributeMethodInfoProxyCategory> GetAOPAttributeMethodInfoProxyCategoryList(
			MethodBase sourceMethodBase)
		{
			var aopAttributeMethodInfoProxyCategories = from a in sourceMethodBase.GetCustomAttributes(true)
				where a is IAOPAttribute
				select new AOPAttributeMethodInfoProxyCategory(a as IAOPAttribute); //linq会延迟处理的，用到的时候才会调用
			var result = aopAttributeMethodInfoProxyCategoryCacheDict.GetOrAddDefault(sourceMethodBase,
				() => new List<AOPAttributeMethodInfoProxyCategory>(aopAttributeMethodInfoProxyCategories));
			return result;
		}

		/// <summary>
		///   查找并调用被切面的方法的其中一个AOPAttribute属性中的AOP处理方法
		/// </summary>
		/// <param name="aopAttributeMethodInfoProxyCategory"></param>
		/// <param name="aopMethodType"></param>
		/// <param name="sourceMethodOwner"></param>
		/// <param name="sourceMethodBase"></param>
		/// <param name="sourceMethodArgs"></param>
		private void InvokeAOPAttributeMethod(AOPAttributeMethodInfoProxyCategory aopAttributeMethodInfoProxyCategory,
			AOPMethodType aopMethodType, object sourceMethodOwner, MethodBase sourceMethodBase,
			params object[] sourceMethodArgs)
		{
			//获取目标函数
			var targetMethodInfoProxy = GetTargetMethodInfoProxy(aopAttributeMethodInfoProxyCategory, aopMethodType,
				sourceMethodOwner, sourceMethodBase, sourceMethodBase.GetParameterTypes());

			//缓存目标函数以备下次使用
			aopAttributeMethodInfoProxyCategory.aopMethodInfoDict[aopMethodType] = targetMethodInfoProxy;

			//调用目标函数
			var targetMethodInfo = targetMethodInfoProxy.GetTargetMethodInfo();
			var targetMethodArgs = GetTargetMethodArgs(targetMethodInfoProxy.isTargetMethodAddSelfArg,
				sourceMethodOwner,
				targetMethodInfoProxy.isTargetMethodWithSourceArgs, sourceMethodArgs);
			targetMethodInfo.Invoke(aopAttributeMethodInfoProxyCategory.aopAttribute, targetMethodArgs);
		}

		/// <summary>
		///   获取目标函数
		/// </summary>
		/// <param name="aopAttributeMethodInfoProxyCategory"></param>
		/// <param name="aopMethodType"></param>
		/// <param name="sourceMethodOwner"></param>
		/// <param name="sourceMethodBase"></param>
		/// <param name="sourceMethodParameterTypes"></param>
		/// <returns></returns>
		private MethodInfoProxy GetTargetMethodInfoProxy(
			AOPAttributeMethodInfoProxyCategory aopAttributeMethodInfoProxyCategory,
			AOPMethodType aopMethodType, object sourceMethodOwner, MethodBase sourceMethodBase,
			params Type[] sourceMethodParameterTypes)
		{
			//如果缓存中有对应的aop处理函数，则不用查找，直接使用
			if (aopAttributeMethodInfoProxyCategory.aopMethodInfoDict.ContainsKey(aopMethodType))
				return aopAttributeMethodInfoProxyCategory.aopMethodInfoDict[aopMethodType];

			//根据优先顺序查找
			var aopAttributeType = aopAttributeMethodInfoProxyCategory.aopAttribute.GetType();
			var sourceMethodName = sourceMethodBase.Name;
			return AOPUtil.SeachTargetMethodInfoProxy(aopAttributeType, sourceMethodBase.DeclaringType,
				sourceMethodName,
				aopMethodType, sourceMethodParameterTypes);
		}

		/// <summary>
		///   获取目标函数时实际需要传入的参数
		/// </summary>
		/// <param name="isTargetMethodAddSelfArg"></param>
		/// <param name="sourceMethodOwner"></param>
		/// <param name="isTargetMethodWithSourceArgs"></param>
		/// <param name="sourceMethodArgs"></param>
		/// <returns></returns>
		private object[] GetTargetMethodArgs(bool isTargetMethodAddSelfArg, object sourceMethodOwner,
			bool isTargetMethodWithSourceArgs, params object[] sourceMethodArgs)
		{
			var result = new List<object>();

			if (isTargetMethodAddSelfArg)
				result.Add(sourceMethodOwner);

			if (isTargetMethodWithSourceArgs && sourceMethodArgs != null && sourceMethodArgs.Length > 0)
				result.AddRange(sourceMethodArgs);

			return result.ToArray();
		}

		#endregion
	}
}