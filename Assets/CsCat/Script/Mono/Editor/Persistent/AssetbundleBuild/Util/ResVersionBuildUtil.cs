using UnityEditor;

namespace CsCat
{
	public class ResVersionBuildUtil
	{
		public static void Build()
		{
			string filePath = BuildConst.ResVersionFilePath.WithRootPath(FilePathConst.ProjectPath);
			StdioUtil.CreateFileIfNotExist(filePath);
			string resVersion = StdioUtil.ReadTextFile(filePath);
			resVersion = resVersion.IsNullOrWhiteSpace() ? BuildConst.ResVersionDefault : IncreaseResSubVersion(resVersion);
			StdioUtil.WriteTextFile(filePath, resVersion);
			StdioUtil.WriteTextFile(BuildConst.Output_Path + BuildConst.ResVersionFileName, resVersion);
			AssetDatabase.Refresh();
		}


		public static string IncreaseResSubVersion(string resVersion)
		{
			// 每一次构建资源，子版本号自增，注意：前两个字段这里不做托管，自行编辑设置
			string[] version = resVersion.Split('.');
			if (version.Length > 0)
			{
				int.TryParse(version[version.Length - 1], out var subVer);
				version[version.Length - 1] = string.Format("{0:D5}", subVer + 1);
			}

			resVersion = string.Join(".", version);
			return resVersion;
		}
	}
}