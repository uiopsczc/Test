using CSObjectWrapEditor;
using UnityEditor;

namespace CsCat
{
  public partial class AssemblyReloadEventsMono
  {
    static void AfterAssemblyReload()
    {
      GenericMenuCatUtil.Load();
      AssetPathRefManager.instance.LoadFromPath(
        AssetPathRefConst.Save_File_Path.WithRootPath(FilePathConst.ProjectPath));
      if (EditorPrefs.GetBool("AfterAssemblyReload_OneTime_Callback"))
        AfterAssemblyReload_Once_Callback();

    }

    static void AfterAssemblyReload_Once_Callback()
    {
      EditorPrefs.SetBool("AfterAssemblyReload_OneTime_Callback", false);
      Generator.GenAll(); // 生成xlua gen文件
    }
  }
}