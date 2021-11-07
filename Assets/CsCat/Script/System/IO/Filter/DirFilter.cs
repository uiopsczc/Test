using System.IO;

namespace CsCat
{
    public class DirFilter : IFileSystemInfoFilter
    {
        public bool Accept(FileSystemInfo fileSystemInfo)
        {
            return fileSystemInfo.IsDirectory();
        }
    }
}