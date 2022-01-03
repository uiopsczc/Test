using System;
using System.Reflection;

namespace CsCat
{
	public class AssemblyUtil
	{
		public static Assembly GetAssembly(string assembly_name)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (var i = 0; i < assemblies.Length; i++)
			{
				var assembly = assemblies[i];
				if (assembly.GetName().Name.Equals(assembly_name))
					return assembly;
			}

			return null;
		}
	}
}