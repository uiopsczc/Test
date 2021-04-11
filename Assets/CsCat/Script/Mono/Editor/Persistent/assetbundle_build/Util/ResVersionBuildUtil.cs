using UnityEditor;

namespace CsCat
{
  public class ResVersionBuildUtil
  {

    public static void Build()
    {
      string filePath = BuildConst.Res_Version_File_Path.WithRootPath(FilePathConst.ProjectPath);
      StdioUtil.CreateFileIfNotExist(filePath);
      string resVersion = StdioUtil.ReadTextFile(filePath);
      if (resVersion.IsNullOrWhiteSpace())
        resVersion = BuildConst.Res_Version_Default;
      else
        resVersion = IncreaseResSubVersion(resVersion);
      StdioUtil.WriteTextFile(filePath, resVersion);
      StdioUtil.WriteTextFile(BuildConst.Output_Path + BuildConst.Res_Version_File_Name, resVersion);
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