using System;
using System.Collections.Generic;
using System.IO;

namespace CsCat
{
  public static class DirectoryInfoExtension
  {
    /// <summary>
    /// 获取DirectoryInfo目录下的fileName的路径
    /// </summary>
    public static string SubPath(this DirectoryInfo self, string file_name)
    {
      return self.FullName + Path.DirectorySeparatorChar + file_name;
    }

    /// <summary>
    /// 移除文件夹dir
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static void ClearDir(this DirectoryInfo self)
    {
      if (self.IsDirectory())
      {
        FileSystemInfo[] fs = self.GetFileSystemInfos();
        foreach (FileSystemInfo t in fs)
          t.RemoveFiles();
      }
    }

    /// <summary>
    /// 搜索文件夹dir下符合过滤条件filter中的文件，将文件添加到results中
    /// </summary>
    /// <param name="self"></param>
    /// <param name="filter"></param>
    /// <param name="results"></param>
    /// <returns></returns>
    public static List<FileSystemInfo> SearchFiles(this DirectoryInfo self, Func<FileSystemInfo, bool> filter)
    {
      FileSystemInfo[] fs = self.GetFileSystemInfos();
      List<FileSystemInfo> result = new List<FileSystemInfo>();
      foreach (FileSystemInfo f in fs)
      {
        if (filter(f))
          result.Add(f);
        if (f.IsDirectory())
        {
          List<FileSystemInfo> sub_result_list = SearchFiles((DirectoryInfo)f, filter);
          if (sub_result_list.Count > 0)
            result.AddRange(sub_result_list);
        }
      }

      return result;
    }
  }
}