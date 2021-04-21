using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace CsCat
{
  public class StdioUtil
  {
    #region field

    public static readonly IFileSystemInfoFilter DIR_FILTER = new DirFilter();

    #endregion

    #region static method

    #region public

    /// <summary>
    /// dir目录下创建prefix+yyyy+suffix文件
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="prefix"></param>
    /// <param name="suffix"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public static FileInfo CreateYearlyFile(DirectoryInfo dir, string prefix, string suffix,
      RandomManager randomManager = null)
    {
      return CreateTimelyFile(dir, prefix, suffix, 4, randomManager);
    }

    /// <summary>
    /// dir目录下创建prefix+yyyyMM+suffix文件
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="prefix"></param>
    /// <param name="suffix"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public static FileInfo CreateMonthlyFile(DirectoryInfo dir, string prefix, string suffix,
      RandomManager randomManager = null)
    {
      return CreateTimelyFile(dir, prefix, suffix, 6, randomManager);
    }

    /// <summary>
    /// dir目录下创建prefix+yyyyMMdd+suffix文件
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="prefix"></param>
    /// <param name="suffix"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public static FileInfo CreateDailyFile(DirectoryInfo dir, string prefix, string suffix,
      RandomManager randomManager = null)
    {
      return CreateTimelyFile(dir, prefix, suffix, 8, randomManager);
    }

    /// <summary>
    /// dir目录下创建prefix+yyyyMMddHH+suffix文件
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="prefix"></param>
    /// <param name="suffix"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public static FileInfo CreateHourlyFile(DirectoryInfo dir, string prefix, string suffix,
      RandomManager randomManager = null)
    {
      return CreateTimelyFile(dir, prefix, suffix, 10, randomManager);
    }

    /// <summary>
    /// dir目录下创建prefix+yyyyMMddHHmmss+(随机数3位)+suffix文件
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="prefix"></param>
    /// <param name="suffix"></param>
    /// <returns></returns>
    public static FileInfo CreateTimesliceFile(DirectoryInfo dir, string prefix, string suffix,
      RandomManager randomManager = null)
    {
      randomManager = randomManager ?? Client.instance.randomManager;
      int i = 0;
      do
      {
        string dateTime = DateTimeUtil.NowDateTime().ToString("yyyyMMddHHmmssSSS");
        string rand = new StringBuilder(randomManager.RandomInt(0, 1000) + "").ToString();
        string stem = dateTime
                      + rand.FillHead(3, 'c');
        string file_name = prefix + stem + suffix;

        var file = new FileInfo(dir.SubPath(file_name));
        if (file.Exists)
          continue;
        file.Create().Close();
        return file;
      } while (i++ < 10000);

      throw new IOException(dir.FullName + "中无法创建唯一文件");
    }

    /// <summary>
    /// 获得文件名称的后缀名
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetExtName(string name)
    {
      string s = name.Replace('\\', '/');
      int pos1 = s.LastIndexOf("/");
      int pos = s.LastIndexOf(".");
      if (pos == -1 || pos < pos1)
        return "";
      return name.Substring(pos);
    }

    /// <summary>
    /// 移除文件后缀名
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string RemoveExtName(string name)
    {
      string s = name.Replace('\\', '/');
      int pos1 = s.LastIndexOf("/");
      int pos = s.LastIndexOf(".");
      if (pos == -1 || pos < pos1)
        return name;
      return name.Substring(0, pos);
    }

    /// <summary>
    /// 更改文件后缀名
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ext_name"></param>
    /// <returns></returns>
    public static string ChangeExtName(string name, string ext_name)
    {
      if (!ext_name.StartsWith("."))
        ext_name = "." + ext_name;
      return RemoveExtName(name) + ext_name;
    }

    /// <summary>
    /// 读取ins到buf中
    /// </summary>
    /// <param name="ins"></param>
    /// <param name="buf"></param>
    /// <returns></returns>
    public static int ReadStream(System.IO.Stream ins, byte[] buf)
    {
      return ReadStream(ins, buf, 0, buf.Length);
    }

    /// <summary>
    /// 读取输入流中的数据,直到缓冲区满
    /// </summary>
    /// <param name="ins"></param>
    /// <param name="buf"></param>
    /// <param name="offset"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public static int ReadStream(System.IO.Stream ins, byte[] buf, int offset, int len)
    {
      int k = 0;
      do
      {
        int j = ins.Read(buf, offset + k, len - k);
        if (j > 0)
        {
          k += j;
          if (k >= len)
            break;
          continue;
        }
        else
          break;
      } while (true);

      return k;
    }

    /// <summary>
    /// 在Stream读取len长度的数据
    /// </summary>
    /// <param name="ins"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public static byte[] ReadStream(System.IO.Stream ins, int len)
    {
      var buf = new byte[len];
      len = ReadStream(ins, buf);
      if (len < buf.Length)
        return ByteUtil.SubBytes(buf, 0, len);
      return buf;
    }

    /// <summary>
    /// 读取ins的全部数据
    /// </summary>
    /// <param name="ins"></param>
    /// <returns></returns>
    public static byte[] ReadStream(System.IO.Stream ins)
    {
      var outs = new MemoryStream();
      CopyStream(ins, outs);
      return outs.ToArray();
    }

    /// <summary>
    /// 读取ins的全部数据到outs中
    /// </summary>
    /// <param name="ins"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void CopyStream(System.IO.Stream ins, System.IO.Stream outs)
    {
      var data = new byte[4096];
      int len;
      do
      {
        len = ReadStream(ins, data);
        if (len > 0)
          outs.Write(data, 0, len);
      } while (len >= data.Length); //一般情况下是等于，读完的时候是少于
    }

    /// <summary>
    /// 将data写入文件fileName中
    /// </summary>
    /// <param name="file_name"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static void WriteFile(string file_name, byte[] data)
    {
      WriteFile(new FileInfo(file_name), data);
    }

    /// <summary>
    ///  将data写入文件file中
    /// </summary>
    /// <param name="file"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static void WriteFile(FileInfo file, byte[] data)
    {
      WriteFile(file, data, false);
    }

    /// <summary>
    /// 将data写入文件fileName中(append:是否追加到文件末尾)
    /// </summary>
    /// <param name="file_name"></param>
    /// <param name="data"></param>
    /// <param name="append">是否追加到文件末尾</param>
    /// <returns></returns>
    public static void WriteFile(string file_name, byte[] data, bool append)
    {
      WriteFile(new FileInfo(file_name), data, append);
    }

    /// <summary>
    ///  将data写入文件file中(append:是否追加到文件末尾)
    /// </summary>
    /// <param name="file"></param>
    /// <param name="data"></param>
    /// <param name="append">是否追加到文件末尾</param>
    /// <returns></returns>
    public static void WriteFile(FileInfo file, byte[] data, bool append)
    {
      CreateFileIfNotExist(file.FullName);
      var fos = new FileStream(file.FullName, append ? FileMode.Append : FileMode.Truncate, FileAccess.Write);
      try
      {
        fos.Write(data, 0, data.Length);
      }
      finally
      {
        fos.Close();
      }
    }

    /// <summary>
    /// 读取文件fileName的内容
    /// </summary>
    /// <param name="file_name"></param>
    /// <returns></returns>
    public static byte[] ReadFile(string file_name)
    {
      return ReadFile(new FileInfo(file_name));
    }

    /// <summary>
    /// 读取文件file的内容
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static byte[] ReadFile(FileInfo file)
    {
      var fis = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
      try
      {
        var data = new byte[(int)file.Length];
        fis.Read(data, 0, data.Length);
        return data;
      }
      finally
      {
        fis.Close();
      }
    }

    /// <summary>
    /// 向文件fileName写入content内容(append:是否追加到文件末尾)
    /// </summary>
    /// <param name="file_name"></param>
    /// <param name="content"></param>
    /// <param name="is_append">是否追加到文件末尾</param>
    /// <returns></returns>
    public static void WriteTextFile(string file_name, string content, bool is_writeLine = false,
      bool is_append = false)
    {
      WriteTextFile(new FileInfo(file_name), content, is_writeLine, is_append);
    }

    /// <summary>
    /// 向文件file写入content内容(append:是否追加到文件末尾)
    /// </summary>
    /// <param name="file"></param>
    /// <param name="content"></param>
    /// <param name="is_append">是否追加到文件末尾</param>
    /// <returns></returns>
    public static void WriteTextFile(FileInfo file, string content, bool is_writeLine, bool is_append)
    {
      CreateFileIfNotExist(file.FullName);
      var fw = new StreamWriter(file.FullName, is_append);
      try
      {
        if (!is_writeLine)
          fw.Write(content);
        else
          fw.WriteLine(content);
        fw.Flush();
      }
      finally
      {
        fw.Close();
      }
    }

    public static void WriteTextFile(FileInfo file, List<string> content_list, bool is_append)
    {
      CreateFileIfNotExist(file.FullName);
      var fw = new StreamWriter(file.FullName, is_append);
      try
      {
        foreach (var content in content_list)
          fw.WriteLine(content);
        fw.Flush();
      }
      finally
      {
        fw.Close();
      }
    }




    /// <summary>
    /// 读取文件fileName，返回字符串内容
    /// </summary>
    /// <param name="file_name"></param>
    /// <returns></returns>
    public static string ReadTextFile(string file_name)
    {
      return ReadTextFile(new FileInfo(file_name));
    }

    /// <summary>
    /// 读取文件file，返回字符串内容
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string ReadTextFile(FileInfo file)
    {
      var fr = new StreamReader(file.FullName);
      var sb = new StringBuilder();
      var chars = new char[1024];
      try
      {
        int n;
        while ((n = fr.Read(chars, 0, chars.Length)) != 0)
          sb.Append(chars, 0, n);
        return sb.ToString();
      }
      finally
      {
        fr.Close();
      }

    }

    /// <summary>
    /// 读取url的内容
    /// </summary>
    /// <param name="url"></param>
    /// <param name="retry_count">连接失败最大重连次数</param>
    /// <param name="err_wait_duration">连接失败后等待时间</param>
    /// <returns></returns>
    public static byte[] ReadUrl(string url, int retry_count, int err_wait_duration)
    {
      int i = 0;
      WebRequest request = WebRequest.Create(url);
      while (true)
      {
        try
        {
          System.IO.Stream ins = request.GetResponse().GetResponseStream();
          try
          {
            byte[] data = ReadStream(ins);
            return data;
          }
          finally
          {
            if (ins != null)
              ins.Close();
          }
        }
        catch
        {
          i++;
          if (i > retry_count)
            throw new IOException("重新读取超过" + retry_count + "次");
          try
          {
            if (err_wait_duration > 0L)
              Thread.Sleep(err_wait_duration);
          }
          catch
          {
          }
        }
      }
    }

    /// <summary>
    /// 将src的内容复制到dst中（src可以是文件夹）
    /// </summary>
    /// <param name="src"></param>
    /// <param name="dst"></param>
    /// <returns></returns>
    public static void CopyFile(FileSystemInfo src, FileSystemInfo dst)
    {
      if (src.IsDirectory())
      {
        string str1 = src.FullName.ToLower();
        string str2 = dst.FullName.ToLower();
        if (str2.StartsWith(str1))
        {
          throw new IOException("重叠递归复制" + str1 + "->" + str2);
        }

        var dir2 = new DirectoryInfo(KeyUtil.GetCombinedKey(Path.DirectorySeparatorChar, dst.FullName, src.Name));
        dir2.Create();
        if (!dir2.IsDirectory())
          throw new IOException("无法创建目录" + dir2);

        FileSystemInfo[] srcs = ((DirectoryInfo)src).GetFileSystemInfos();
        foreach (FileSystemInfo t in srcs)
          CopyFile(t, dir2);
      }
      else
      {
        var fis = new FileStream(src.FullName, FileMode.Open, FileAccess.Read);
        try
        {
          FileInfo dstInfo;
          if (dst.IsDirectory())
          {
            dstInfo = new FileInfo(((DirectoryInfo)dst).SubPath(src.Name));
            dstInfo.Create().Close();
          }
          else
          {
            DirectoryInfo pdir2 = ((FileInfo)dst).Directory; // 目标文件dst的父级目录
            if (pdir2 != null)
              pdir2.Create();
            if (pdir2 == null || !pdir2.Exists)
              throw new IOException("无法创建目录:" + pdir2);
            dstInfo = (FileInfo)dst;
            dstInfo.Create().Close();
          }

          if (!(src).Equals(dstInfo))
          {
            var fos = new FileStream(dst.FullName, FileMode.Truncate, FileAccess.Write);
            try
            {
              CopyStream(fis, fos);
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
    /// 移除文件path（path可以是文件夹）
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static void RemoveFiles(string path)
    {
      var dir = new DirectoryInfo(path);
      var file = new FileInfo(path);
      if (dir.Exists)
        RemoveFile(dir);
      else
        RemoveFile(file);
    }

    /// <summary>
    /// 移除文件file（file可以是文件夹）
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static void RemoveFile(FileSystemInfo file)
    {
      if (!file.Exists)
        return;
      if (file.IsDirectory())
        ClearDir((DirectoryInfo)file);
      file.Delete();
    }

    /// <summary>
    /// 移除文件夹dir
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static void ClearDir(string dir)
    {
      ClearDir(new DirectoryInfo(dir));
    }

    /// <summary>
    /// 移除文件夹dir
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static void ClearDir(DirectoryInfo dir)
    {
      if (dir.Exists && dir.IsDirectory())
      {
        FileSystemInfo[] fs = dir.GetFileSystemInfos();
        foreach (FileSystemInfo t in fs)
          RemoveFile(t);
      }
    }

    /// <summary>
    /// 搜索文件夹dir下符合过滤条件filter中的文件，将文件添加到results中
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="filter"></param>
    /// <param name="results"></param>
    /// <returns></returns>
    public static void SearchFiles(DirectoryInfo dir, IFileSystemInfoFilter filter, IList results)
    {
      FileSystemInfo[] fs = dir.GetFileSystemInfos();
      var list = new ArrayList();
      foreach (FileSystemInfo f in fs)
      {
        if (filter.Accept(f))
          list.Add(f);
      }

      fs = new FileSystemInfo[list.Count];
      list.CopyTo(fs);
      foreach (FileSystemInfo t in fs)
      {
        if (t.IsDirectory())
          SearchFiles((DirectoryInfo)t, filter, results);
        else
          results.Add(t);
      }
    }

    public static void CreateDirectoryIfNotExist(string path)
    {
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }
    }

    public static void CreateFileIfNotExist(string path)
    {
      if (!File.Exists(path))
      {
        string dir_path = Path.GetDirectoryName(path);
        CreateDirectoryIfNotExist(dir_path);
        File.Create(path).Dispose();
      }
    }

    #endregion

    #region private

    /// <summary>
    /// dir目录下创建prefix+yyyyMMddHHmmssSSS(len决定取yyyyMMddHHmmssSSS的多少位)+suffix文件
    /// </summary>
    /// <param name="dir">目录</param>
    /// <param name="prefix">前缀</param>
    /// <param name="suffix">后缀</param>
    /// <param name="len"></param>
    /// <returns></returns>
    private static FileInfo CreateTimelyFile(DirectoryInfo dir, string prefix, string suffix, int len,
      RandomManager randomManager = null)
    {
      randomManager = randomManager ?? Client.instance.randomManager;
      int i = 0;
      do
      {
        string dateTime = DateTimeUtil.NowDateTime().ToString("yyyyMMddHHmmssSSS");
        string rand = new StringBuilder(randomManager.RandomInt(0, 1000).ToString()).ToString();
        string stem = (dateTime + rand.FillHead(3, '0'))
          .Substring(0, len);

        DirectoryInfo timely_dir = dir.CreateSubdirectory(stem);
        timely_dir.Create();
        if (timely_dir.Exists)
        {
          string file_name = prefix + stem + suffix;
          var fileInfo = new FileInfo(timely_dir.SubPath(file_name));
          if (fileInfo.Exists)
            continue;
          fileInfo.Create().Close();
          return fileInfo;
        }

        throw new IOException("无法创建目录:" + timely_dir.FullName);
      } while (i++ < 10000);

      throw new IOException(dir.FullName + "中无法创建唯一文件");
    }

    #endregion

    #endregion




  }
}
