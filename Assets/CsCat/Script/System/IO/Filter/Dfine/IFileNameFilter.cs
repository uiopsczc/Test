using System.IO;

namespace CsCat
{
    public interface IFileNameFilter
    {
        bool Accept(DirectoryInfo dir, string fileName);
    }
}