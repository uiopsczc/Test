using System.IO;

namespace CsCat
{
  public class DirFilter : IFileSystemInfoFilter
  {
    #region public method

    public bool Accept(FileSystemInfo file)
    {
      return file.IsDirectory();
    }

    #endregion
  }
}