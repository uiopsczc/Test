using System.Collections.Generic;

namespace CsCat
{
  public class CsvConst
  {
    /// <summary>
    /// 列名为键的约定的形式
    /// </summary>
    public static List<string> Key_Symbol_List = new List<string>() { "(key)", "（key）", "（key)", "(key）" };

    public static string BasePath = FilePathConst.ResourcesPath;
  }
}