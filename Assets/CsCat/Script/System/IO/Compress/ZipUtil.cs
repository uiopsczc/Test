using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace CsCat
{
  public class ZipUtil
  {
    /// <summary>
    ///   打包指定文件夹下的文件，并在指定路径创建zip文件
    /// </summary>
    /// <param name="directory_path">指定待打包的文件夹</param>
    /// <param name="zip_file_path">创建的zip文件完全路径</param>
    /// <returns>是否成功生成</returns>
    public static bool CreateZipFile(string directory_path, string zip_file_path)
    {
      var success = false;
      if (!Directory.Exists(directory_path))
      {
        LogCat.LogErrorFormat("Cannot find directory '{0}'", directory_path);
        return false;
      }

      try
      {
        var file_names = Directory.GetFiles(directory_path);
        using (var stream = new ZipOutputStream(File.Create(zip_file_path)))
        {
          stream.SetLevel(9); // 压缩级别 0-9
          //s.Password = "123"; //Zip压缩文件密码
          var buffer = new byte[4096]; //缓冲区大小
          foreach (var file in file_names)
          {
            var entry = new ZipEntry(Path.GetFileName(file));
            entry.DateTime = DateTimeUtil.NowDateTime();
            stream.PutNextEntry(entry);
            using (var fs = File.OpenRead(file))
            {
              int source_bytes;
              do
              {
                source_bytes = fs.Read(buffer, 0, buffer.Length);
                stream.Write(buffer, 0, source_bytes);
              } while (source_bytes > 0);
            }
          }

          success = true;
          stream.Finish();
          stream.Close();
        }
      }
      catch (Exception ex)
      {
        success = false;
        LogCat.LogErrorFormat("Exception during processing {0}", ex);
      }

      return success;
    }

    public void UnZipFile(string zip_file_path)
    {
      if (!File.Exists(zip_file_path))
      {
        LogCat.LogErrorFormat("Cannot find file '{0}'", zip_file_path);
        return;
      }

      using (var s = new ZipInputStream(File.OpenRead(zip_file_path)))
      {
        ZipEntry theEntry;
        while ((theEntry = s.GetNextEntry()) != null)
        {
          var directory_name = Path.GetDirectoryName(theEntry.Name);
          var file_name = Path.GetFileName(theEntry.Name);

          // create directory
          if (directory_name.Length > 0) Directory.CreateDirectory(directory_name);

          if (file_name != string.Empty)
            using (var stream_writer = File.Create(theEntry.Name))
            {
              var size = 2048;
              var data = new byte[2048];
              while (true)
              {
                size = s.Read(data, 0, data.Length);
                if (size > 0)
                  stream_writer.Write(data, 0, size);
                else
                  break;
              }
            }
        }
      }
    }
  }
}