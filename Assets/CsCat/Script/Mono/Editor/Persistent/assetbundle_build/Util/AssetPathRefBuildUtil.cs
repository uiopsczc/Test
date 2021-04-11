using UnityEditor;

namespace CsCat
{

  public class AssetPathRefBuildUtil
  {
    public static void Build()
    {
      AssetPathRefManager.instance.Save();
      StdioUtil.WriteTextFile(BuildConst.Output_Path + AssetPathRefConst.Save_File_Name,
        StdioUtil.ReadTextFile(AssetPathRefConst.Save_File_Path));
      AssetDatabase.Refresh();
    }







  }
}