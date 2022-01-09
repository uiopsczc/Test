using System;
using System.Reflection;


namespace CsCat
{
	public class AOPUtil
	{
		///维度1 目标参数方法名维度
		///处理两种方法名的情况
		///		1.方法名：被切面的方法的类_被切面的方法的名称_AOPMethodType的类型 
		///     2.方法名：被切面的方法的名称_AOPMethodType的类型 
		private static string[] Seach_Format_Target_Method_Name_Orders = new string[]
		{
			"{0}_{1}_{2}",
			"{1}_{2}",
		};

		///维度2 目标函数的参数维度-是否 目标函数的参数列表 带有 原函数的参数列表
		///处理两种方法名的情况
		///		1.参数列表：带有原函数的参数列表
		///     2.参数列表：不带有原函数的参数列表
		private static bool[] Is_Target_Method_With_Source_ArgTypes_Orders = new bool[]
		{
			true,
			false
		};

		///维度3 目标函数的参数维度-是否 目标函数的参数列表 带有 原函数所在类的实例引用self
		///处理两种方法名的情况
		///		1.参数列表：带原函数所在类的实例引用self
		///     2.参数列表：不原函数所在类的实例引用self
		private static bool[] Is_Target_Method_Self_Arg_Orders = new bool[]
		{
			true,
			false
		};


		/// <summary>
		/// 获取目标参数方法顺序
		/// </summary>
		/// <param name="source_type"></param>
		/// <param name="source_method_name"></param>
		/// <param name="aop_method_type"></param>
		/// <returns></returns>
		private static string[] GetSeachTargetMethodNameOrders(Type source_type, string source_method_name,
			AOPMethodType aop_method_type)
		{
			string[] result = new string[Seach_Format_Target_Method_Name_Orders.Length + 1];
			for (int i = 0; i < Seach_Format_Target_Method_Name_Orders.Length; i++)
				result[i] = GetTargetMethodName(Seach_Format_Target_Method_Name_Orders[i], source_type,
					source_method_name,
					aop_method_type);
			// 再加上默认的处理方法
			result[Seach_Format_Target_Method_Name_Orders.Length] = aop_method_type.ToString();
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="format_target_method_name"></param>
		/// <param name="source_type"></param>
		/// <param name="source_method_name"></param>
		/// <param name="aopMethodType"></param>
		/// <returns></returns>
		private static string GetTargetMethodName(string format_target_method_name, Type source_type,
			string source_method_name, AOPMethodType aopMethodType)
		{
			return string.Format(format_target_method_name, source_type.GetLastName(), source_method_name,
				aopMethodType.ToString());
		}


		/// 特殊情况调用优先顺序
		/// 1.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型(【被切面的方法的类本身】+被切面方法的参数)
		///   1.1.被切面的方法的名称_AOPMethodType的类型（【被切面的方法的类本身】+被切面方法的参数）
		/// 2.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型(被切面方法的参数) 
		///   2.1.被切面的方法的名称_AOPMethodType的类型（被切面方法的参数）
		/// 3.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型(【被切面的方法的类本身】)
		///   3.1.被切面的方法的名称_AOPMethodType的类型（【被切面的方法的类本身】）
		/// 4.被切面的方法的名称_AOPMethodType的类型（）
		///   4.1.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型()
		/// 5.默认的处理方法
		public static MethodInfoProxy SeachTargetMethodInfoProxy(Type aopAttributeType, Type sourceType,
			string sourceMethodName, AOPMethodType aopMethodType, Type[] sourceMethodArgTypes)
		{
			//从特殊到一般，注意有顺序先后的查找
			var names = GetSeachTargetMethodNameOrders(sourceType, sourceMethodName,
				aopMethodType);
			for (var i = 0; i < names.Length; i++)
			{
				string targetMethodName = names[i];
				for (var j = 0; j < Is_Target_Method_With_Source_ArgTypes_Orders.Length; j++)
				{
					bool isTargetMethodWithSoruceArgType = Is_Target_Method_With_Source_ArgTypes_Orders[j];
					for (var k = 0; k < Is_Target_Method_Self_Arg_Orders.Length; k++)
					{
						bool isTargetMethodSelfArg = Is_Target_Method_Self_Arg_Orders[k];
						MethodInfoProxy methodInfoProxy = new MethodInfoProxy(targetMethodName, aopAttributeType,
							sourceType,
							isTargetMethodSelfArg, isTargetMethodWithSoruceArgType, sourceMethodArgTypes);
						MethodInfo targetMethod = aopAttributeType.GetMethodInfo(targetMethodName,
							BindingFlagsConst.All,
							methodInfoProxy.methodArgTypesProxy.targetMethodArgTypes);
						if (targetMethod != null) //命中
							return methodInfoProxy;
					}
				}
			}

			throw new Exception(string.Format("can not find AOPAttributeMethod of  Method:{0}->{1}  AOPAttribute:{2}",
				sourceType.ToString(), sourceMethodName, aopAttributeType));
		}
	}
}