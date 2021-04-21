using System.IO;
using System.Text;
using UnityEditor;

namespace CsCat
{
  /// <summary>
  ///   CZM工具菜单
  /// </summary>
  public partial class CZMToolMenu
  {
    [MenuItem(CZMToolConst.MenuRoot + "将所有cs文件保存为utf8编码")]
    public static void ConvertFilesEncoding()
    {
      string dir_path = FilePathConst.AssetsPath;
      //string dir_path = "F:/WorkSpace/Unity/Test/Assets/cscat/test";
      string[] file_paths = Directory.GetFiles(dir_path, "*.cs", SearchOption.AllDirectories);
      foreach (string file_path in file_paths)
      {
        SetFileEncoding(file_path.Replace('\\', '/'));
      }
      LogCat.log("EncodingToUtf8 完成");
    }

    static void SetFileEncoding(string file_path, Encoding target_encoding = null)
    {
      if (target_encoding == null)
        target_encoding = Encoding.UTF8;
      Encoding encoding = EncodingUtil.GetEncoding(file_path);
      //    LogCat.logError(file_path);
      //    LogCat.logError(target_encoding);
      var data = File.ReadAllBytes(file_path);
      data = target_encoding.GetBytes(encoding.GetString(data));
      File.WriteAllBytes(file_path, data);

    }
  }
}