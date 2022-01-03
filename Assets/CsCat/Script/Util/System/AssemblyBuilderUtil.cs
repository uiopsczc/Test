using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace CsCat
{
	public class AssemblyBuilderUtil
	{
		private static readonly Dictionary<ObjectInfos, AssemblyBuilder> _assemblyBuilderDict =
			new Dictionary<ObjectInfos, AssemblyBuilder>();

		public static AssemblyBuilder GetAssemblyBuilder(string assemblyNameString = null,
			AssemblyBuilderAccess assemblyBuilderAccess = AssemblyBuilderAccess.Run)
		{
			var infos = new ObjectInfos(assemblyNameString, assemblyBuilderAccess);
			if (_assemblyBuilderDict.ContainsKey(infos)) return _assemblyBuilderDict[infos];

			ObjectInfos infos2 = null;
			if (assemblyNameString == null)
			{
				assemblyNameString = Guid.NewGuid().ToString()
					.Replace(StringConst.String_Minus, StringConst.String_Empty);
				infos2 = new ObjectInfos(assemblyNameString, assemblyBuilderAccess);
				if (_assemblyBuilderDict.ContainsKey(infos2)) return _assemblyBuilderDict[infos2];
			}

			var assemblyName = new AssemblyName(assemblyNameString);
			var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, assemblyBuilderAccess);
			_assemblyBuilderDict[infos] = assembly;
			if (infos2 != null)
				_assemblyBuilderDict[infos2] = assembly;


			return _assemblyBuilderDict[infos];
		}
	}
}