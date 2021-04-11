using System.IO;

namespace CsCat
{
  public class XMLConfigUtil
  {
    public static XMLConfig LoadConfig(string config_file_path)
    {
      return new XMLConfig(config_file_path);
    }

    public static XMLConfig LoadConfig(FileInfo config_file)
    {
      return new XMLConfig(config_file);
    }

    public static XMLConfig LoadConfig(Stream stream)
    {
      return new XMLConfig(stream);
    }
  }
}