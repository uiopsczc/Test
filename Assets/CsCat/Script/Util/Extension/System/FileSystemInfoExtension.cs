using System.IO;

namespace CsCat
{
  public static class FileSystemInfoExtension
  {
    /// <summary>
    ///   是否是目录
    /// </summary>
    public static bool IsDirectory(this FileSystemInfo self)
    {
      return (self.Attributes & FileAttributes.Directory) == FileAttributes.Directory;
    }

    /// <summary>
    ///   是否是文件
    /// </summary>
    public static bool IsFile(this FileSystemInfo self)
    {
      return !self.IsDirectory();
    }

    /// <summary>
    ///   父目录
    /// </summary>
    public static DirectoryInfo Parent(this FileSystemInfo self)
    {
      if (self.IsFile())
        return ((FileInfo) self).Directory;
      return ((DirectoryInfo) self).Parent;
    }

    /// <summary>
    ///   将src的内容复制到dst中（src可以是文件夹）
    /// </summary>
    /// <param name="self"></param>
    /// <param name="dst"></param>
    /// <returns></returns>
    public static void CopyFileTo(this FileSystemInfo self, FileSystemInfo dst)
    {
      if (self.IsDirectory())
      {
        var str1 = self.FullName.ToLower();
        var str2 = dst.FullName.ToLower();
        if (str2.StartsWith(str1)) throw new IOException("重叠递归复制" + str1 + "->" + str2);
        var dir2 = new DirectoryInfo(dst.FullName + Path.DirectorySeparatorChar + self.Name);
        dir2.Create();
        if (!dir2.IsDirectory())
          throw new IOException("无法创建目录" + dir2);

        var srcs = ((DirectoryInfo) self).GetFileSystemInfos();
        foreach (var t in srcs)
          CopyFileTo(t, dir2);
      }
      else
      {
        var fis = new FileStream(self.FullName, FileMode.Open, FileAccess.Read);
        try
        {
          FileInfo dstInfo;
          if (dst.IsDirectory())
          {
            dstInfo = new FileInfo(((DirectoryInfo) dst).SubPath(self.Name));
            dstInfo.Create().Close();
          }
          else
          {
            var pdir2 = ((FileInfo) dst).Directory; // 目标文件dst的父级目录
            if (pdir2 != null)
              pdir2.Create();
            if (pdir2 == null || !pdir2.Exists)
              throw new IOException("无法创建目录:" + pdir2);
            dstInfo = (FileInfo) dst;
            dstInfo.Create().Close();
          }

          if (!self.Equals(dstInfo))
          {
            var fos = new FileStream(dst.FullName, FileMode.Truncate, FileAccess.Write);
            try
            {
              fis.CopyToStream(fos);
            }
            finally
            {
              fos.Close();
            }
          }
        }
        finally
        {
          fis.Close();
        }
      }
    }


    /// <summary>
    ///   移除文件file（file可以是文件夹）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static void RemoveFiles(this FileSystemInfo self)
    {
      if (self.IsDirectory())
        ((DirectoryInfo) self).ClearDir();
      self.Delete();
    }

    public static string FullName(this FileSystemInfo self, char separator = '/')
    {
      return self.FullName.ReplaceDirectorySeparatorChar(separator);
    }
  }
}