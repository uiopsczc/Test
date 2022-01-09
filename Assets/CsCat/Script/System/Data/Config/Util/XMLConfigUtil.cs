using System.IO;

namespace CsCat
{
	public class XMLConfigUtil
	{
		public static XMLConfig LoadConfig(string configFilePath)
		{
			return new XMLConfig(configFilePath);
		}

		public static XMLConfig LoadConfig(FileInfo configFile)
		{
			return new XMLConfig(configFile);
		}

		public static XMLConfig LoadConfig(Stream stream)
		{
			return new XMLConfig(stream);
		}
	}
}