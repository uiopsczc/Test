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
			var parameterInfoes = self.GetParameters();
			foreach (var parameterInfo in parameterInfoes)
				result.Add(parameterInfo.ParameterType);
			return result.ToArray();
		}
	}
}