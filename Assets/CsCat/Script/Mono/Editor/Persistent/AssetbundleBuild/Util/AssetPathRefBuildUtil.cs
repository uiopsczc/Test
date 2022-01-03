using UnityEditor;

namespace CsCat
{
	public class AssetPathRefBuildUtil
	{
		public static void Build()
		{
			AssetPathRefManager.instance.Save();
			StdioUtil.WriteTextFile(BuildConst.Output_Path + AssetPathRefConst.SaveFileName,
				StdioUtil.ReadTextFile(AssetPathRefConst.SaveFilePath));
			AssetDatabase.Refresh();
		}
	}
}