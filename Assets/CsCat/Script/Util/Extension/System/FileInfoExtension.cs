using System.IO;
using System.Text;

namespace CsCat
{
  public static class FileInfoExtension
  {
    /// <summary>
    ///   后缀
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static string Suffix(this FileInfo self)
    {
      return self.Extension;
    }

    /// <summary>
    ///   不带后缀的name
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static string NameWithoutSuffix(this FileInfo self)
    {
      return self.Name.Substring(0, self.Name.LastIndexOf("."));
    }

    /// <summary>
    ///   不带后缀的name（全路径）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static string FullNameWithoutSuffix(this FileInfo self)
    {
      return self.FullName.Substring(0, self.FullName.LastIndexOf("."));
    }


    /// <summary>
    ///   将data写入文件file中(append:是否追加到文件末尾)
    /// </summary>
    /// <param name="self"></param>
    /// <param name="data"></param>
    /// <param name="is_append">是否追加到文件末尾</param>
    /// <returns></returns>
    public static void WriteFile(this FileInfo self, byte[] data, bool is_append)
    {
      var fos = new FileStream(self.FullName, is_append ? FileMode.Append : FileMode.Truncate, FileAccess.Write);
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
    ///   读取文件file的内容
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static byte[] ReadBytes(this FileInfo self)
    {
      var fis = new FileStream(self.FullName, FileMode.Open, FileAccess.Read);
      try
      {
        var data = new byte[(int) self.Length];
        fis.Read(data, 0, data.Length);
        return data;
      }
      finally
      {
        fis.Close();
      }
    }


    /// <summary>
    ///   向文件file写入content内容(append:是否追加到文件末尾)
    /// </summary>
    /// <param name="self"></param>
    /// <param name="content"></param>
    /// <param name="is_append">是否追加到文件末尾</param>
    /// <returns></returns>
    public static void WriteTextFile(this FileInfo self, string content, bool is_writeLine, bool is_append)
    {
      var fw = new StreamWriter(self.FullName, is_append);
      try
      {
        if (!is_writeLine)
          fw.Write(content);
        else
          fw.WriteLine(content);
      }
      finally
      {
        fw.Close();
      }
    }


    /// <summary>
    ///   读取文件file，返回字符串内容
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static string ReadTextFile(this FileInfo self)
    {
      var fr = new StreamReader(self.FullName);
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
    ///   将data写入文件file中
    /// </summary>
    /// <param name="self"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static void WriteFile(this FileInfo self, byte[] data)
    {
      self.WriteFile(data, false);
    }
  }
}