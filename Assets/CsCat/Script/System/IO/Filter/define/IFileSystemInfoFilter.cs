
using System.IO;

namespace CsCat
{
  public interface IFileSystemInfoFilter
  {
    /// <summary>
    /// 筛选出指定路径下的文件
    /// </summary>
    /// <param name="path_name"></param>
    /// <returns></returns>
    bool Accept(FileSystemInfo path_name);
  }
}

