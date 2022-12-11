using System;
using System.Collections.Generic;
using System.Reflection;

namespace CsCat
{
	public static class MethodBaseExtension
	{
		public static Type[] GetParameterTypes(this MethodBase self)
		{
			var result = new List<Type>();
			var parameterInfos = self.GetParameters();
			for (var i = 0; i < parameterInfos.Length; i++)
			{
				var parameterInfo = parameterInfos[i];
				result.Add(parameterInfo.ParameterType);
			}

			return result.ToArray();
		}
	}
}