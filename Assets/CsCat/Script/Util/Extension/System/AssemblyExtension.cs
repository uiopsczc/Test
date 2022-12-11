using System;
using System.Collections.Generic;
using System.Reflection;

namespace CsCat
{
	public static class AssemblyExtension
	{
		/// <summary>
		/// 获得NameSpace下的所有类,例如：Assembly.GetExecutingAssembly().GetClassesOfNameSpace("cat.io");
		/// </summary>
		public static Type[] GetTypesOfNameSpace(this Assembly assembly, string targetNamespace)
		{
			List<Type> typeList = new List<Type>();
			var types = assembly.GetTypes();
			for (var i = 0; i < types.Length; i++)
			{
				var type = types[i];
				if (targetNamespace.Equals(type.Namespace))
					typeList.Add(type);
			}

			return typeList.ToArray();
		}


		public static MemberInfo[] GetCustomAttributeMemberInfos<T>(this Assembly assembly)
		{
			List<MemberInfo> result = new List<MemberInfo>();
			var types = assembly.GetTypes();
			for (var i = 0; i < types.Length; i++)
			{
				var type = types[i];
				for (var j = 0; j < type.GetMembers(BindingFlagsConst.All).Length; j++)
				{
					var memberInfo = type.GetMembers(BindingFlagsConst.All)[j];
					if (memberInfo.GetCustomAttribute<T>() == null) continue;
					result.AddUnique(memberInfo);
				}
			}

			return result.ToArray();
		}
	}
}