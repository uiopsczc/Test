using System.IO;
using CSObjectWrapEditor;
using UnityEditor;

namespace CsCat
{
	public class XLuaReference
	{
		[MenuItem("XLua/Clear And Generate Code", false, 1)]
		public static void ClearAndGenerateCode()
		{
			if (Directory.Exists(GeneratorConfig.commonPath))
			{
				EditorPrefs.SetBool("AfterAssemblyReload_OneTime_Callback", true);
				Generator.ClearAll();
			}
			else
				Generator.GenAll();
		}
	}
}