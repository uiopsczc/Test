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
		public static readonly IFileSystemInfoFilter DIR_FILTER = new DirFilter();


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
				string dateTime = DateTimeUtil.NowDateTime().ToString(StringConst.String_yyyyMMddHHmmssSSS);
				string rand = new StringBuilder(randomManager.RandomInt(0, 1000) + "").ToString();
				string stem = dateTime
							  + rand.FillHead(3, CharConst.Char_c);
				string fileName = prefix + stem + suffix;

				var file = new FileInfo(dir.SubPath(fileName));
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
			string s = name.Replace(CharConst.Char_BackSlash, CharConst.Char_Slash);
			int pos1 = s.LastIndexOf(CharConst.Char_Slash);
			int pos = s.LastIndexOf(CharConst.Char_Dot);
			if (pos == -1 || pos < pos1)
				return StringConst.String_Empty;
			return name.Substring(pos);
		}

		/// <summary>
		/// 移除文件后缀名
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string RemoveExtName(string name)
		{
			string s = name.Replace(CharConst.Char_BackSlash, CharConst.Char_Slash);
			int pos1 = s.LastIndexOf(CharConst.Char_Slash);
			int pos = s.LastIndexOf(CharConst.Char_Dot);
			if (pos == -1 || pos < pos1)
				return name;
			return name.Substring(0, pos);
		}

		/// <summary>
		/// 更改文件后缀名
		/// </summary>
		/// <param name="name"></param>
		/// <param name="extName"></param>
		/// <returns></returns>
		public static string ChangeExtName(string name, string extName)
		{
			if (!extName.StartsWith(StringConst.String_Dot))
				extName = StringConst.String_Dot + extName;
			return RemoveExtName(name) + extName;
		}

		/// <summary>
		/// 读取ins到buf中
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public static int ReadStream(Stream inStream, byte[] buffer)
		{
			return ReadStream(inStream, buffer, 0, buffer.Length);
		}

		/// <summary>
		/// 读取输入流中的数据,直到缓冲区满
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static int ReadStream(Stream inStream, byte[] buffer, int offset, int length)
		{
			int k = 0;
			do
			{
				int j = inStream.Read(buffer, offset + k, length - k);
				if (j > 0)
				{
					k += j;
					if (k >= length)
						break;
					continue;
				}

				break;
			} while (true);

			return k;
		}

		/// <summary>
		/// 在Stream读取len长度的数据
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static byte[] ReadStream(Stream inStream, int length)
		{
			var buffer = new byte[length];
			length = ReadStream(inStream, buffer);
			if (length < buffer.Length)
				return ByteUtil.SubBytes(buffer, 0, length);
			return buffer;
		}

		/// <summary>
		/// 读取ins的全部数据
		/// </summary>
		/// <param name="inStream"></param>
		/// <returns></returns>
		public static byte[] ReadStream(Stream inStream)
		{
			var outStream = new MemoryStream();
			CopyStream(inStream, outStream);
			return outStream.ToArray();
		}

		/// <summary>
		/// 读取ins的全部数据到outs中
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void CopyStream(Stream inStream, Stream outStream)
		{
			var data = new byte[4096];
			int len;
			do
			{
				len = ReadStream(inStream, data);
				if (len > 0)
					outStream.Write(data, 0, len);
			} while (len >= data.Length); //一般情况下是等于，读完的时候是少于
		}

		/// <summary>
		/// 将data写入文件fileName中
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static void WriteFile(string fileName, byte[] data)
		{
			WriteFile(new FileInfo(fileName), data);
		}

		/// <summary>
		///  将data写入文件file中
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static void WriteFile(FileInfo fileInfo, byte[] data)
		{
			WriteFile(fileInfo, data, false);
		}

		/// <summary>
		/// 将data写入文件fileName中(append:是否追加到文件末尾)
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="data"></param>
		/// <param name="isAppend">是否追加到文件末尾</param>
		/// <returns></returns>
		public static void WriteFile(string fileName, byte[] data, bool isAppend)
		{
			WriteFile(new FileInfo(fileName), data, isAppend);
		}

		/// <summary>
		///  将data写入文件file中(append:是否追加到文件末尾)
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <param name="data"></param>
		/// <param name="isAppend">是否追加到文件末尾</param>
		/// <returns></returns>
		public static void WriteFile(FileInfo fileInfo, byte[] data, bool isAppend)
		{
			CreateFileIfNotExist(fileInfo.FullName);
			var fos = new FileStream(fileInfo.FullName, isAppend ? FileMode.Append : FileMode.Truncate,
				FileAccess.Write);
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
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static byte[] ReadFile(string fileName)
		{
			return ReadFile(new FileInfo(fileName));
		}

		/// <summary>
		/// 读取文件file的内容
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <returns></returns>
		public static byte[] ReadFile(FileInfo fileInfo)
		{
			var inFileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
			try
			{
				var data = new byte[(int)fileInfo.Length];
				inFileStream.Read(data, 0, data.Length);
				return data;
			}
			finally
			{
				inFileStream.Close();
			}
		}

		/// <summary>
		/// 向文件fileName写入content内容(append:是否追加到文件末尾)
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="content"></param>
		/// <param name="isAppend">是否追加到文件末尾</param>
		/// <returns></returns>
		public static void WriteTextFile(string fileName, string content, bool isWriteLine = false,
			bool isAppend = false)
		{
			WriteTextFile(new FileInfo(fileName), content, isWriteLine, isAppend);
		}

		public static void WriteTextFile(string fileName, List<string> contentList, bool isAppend = false)
		{
			WriteTextFile(new FileInfo(fileName), contentList, isAppend);
		}

		/// <summary>
		/// 向文件file写入content内容(append:是否追加到文件末尾)
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <param name="content"></param>
		/// <param name="isAppend">是否追加到文件末尾</param>
		/// <returns></returns>
		public static void WriteTextFile(FileInfo fileInfo, string content, bool isWriteLine, bool isAppend)
		{
			CreateFileIfNotExist(fileInfo.FullName);
			var streamWriter = new StreamWriter(fileInfo.FullName, isAppend);
			try
			{
				if (!isWriteLine)
					streamWriter.Write(content);
				else
					streamWriter.WriteLine(content);
				streamWriter.Flush();
			}
			finally
			{
				streamWriter.Close();
			}
		}

		public static void WriteTextFile(FileInfo fileInfo, List<string> contentList, bool isAppend)
		{
			CreateFileIfNotExist(fileInfo.FullName);
			var streamWriter = new StreamWriter(fileInfo.FullName, isAppend);
			try
			{
				foreach (var content in contentList)
					streamWriter.WriteLine(content);
				streamWriter.Flush();
			}
			finally
			{
				streamWriter.Close();
			}
		}


		/// <summary>
		/// 读取文件fileName，返回字符串内容
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static string ReadTextFile(string fileName)
		{
			return ReadTextFile(new FileInfo(fileName));
		}

		/// <summary>
		/// 读取文件file，返回字符串内容
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <returns></returns>
		public static string ReadTextFile(FileInfo fileInfo)
		{
			var streamReader = new StreamReader(fileInfo.FullName);
			var stringBuilder = new StringBuilder();
			var chars = new char[1024];
			try
			{
				int n;
				while ((n = streamReader.Read(chars, 0, chars.Length)) != 0)
					stringBuilder.Append(chars, 0, n);
				return stringBuilder.ToString();
			}
			finally
			{
				streamReader.Close();
			}
		}

		/// <summary>
		/// 读取url的内容
		/// </summary>
		/// <param name="url"></param>
		/// <param name="retryCount">连接失败最大重连次数</param>
		/// <param name="errWaitDuration">连接失败后等待时间</param>
		/// <returns></returns>
		public static byte[] ReadUrl(string url, int retryCount, int errWaitDuration)
		{
			int i = 0;
			WebRequest request = WebRequest.Create(url);
			while (true)
			{
				try
				{
					Stream inStream = request.GetResponse().GetResponseStream();
					try
					{
						byte[] data = ReadStream(inStream);
						return data;
					}
					finally
					{
						inStream?.Close();
					}
				}
				catch
				{
					i++;
					if (i > retryCount)
						throw new IOException(string.Format("重新读取超过{0}次", retryCount));
					try
					{
						if (errWaitDuration > 0L)
							Thread.Sleep(errWaitDuration);
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
		/// <param name="srcFileSystemInfo"></param>
		/// <param name="dstFileSystemInfo"></param>
		/// <returns></returns>
		public static void CopyFile(FileSystemInfo srcFileSystemInfo, FileSystemInfo dstFileSystemInfo)
		{
			if (srcFileSystemInfo.IsDirectory())
			{
				string srcFileFullName = srcFileSystemInfo.FullName.ToLower();
				string dstFileFullName = dstFileSystemInfo.FullName.ToLower();
				if (dstFileFullName.StartsWith(srcFileFullName))
					throw new IOException(string.Format("重叠递归复制{0}->{1}", srcFileFullName, dstFileFullName));

				var dstDirectoryInfo = new DirectoryInfo(
					KeyUtil.GetCombinedKey(Path.DirectorySeparatorChar, dstFileSystemInfo.FullName,
						srcFileSystemInfo.Name));
				dstDirectoryInfo.Create();
				if (!dstDirectoryInfo.IsDirectory())
					throw new IOException(string.Format("无法创建目录{0}", dstDirectoryInfo));

				FileSystemInfo[] srcFileSystemInfos = ((DirectoryInfo)srcFileSystemInfo).GetFileSystemInfos();
				foreach (FileSystemInfo fileSystemInfo in srcFileSystemInfos)
					CopyFile(fileSystemInfo, dstDirectoryInfo);
			}
			else
			{
				var srcFileStream = new FileStream(srcFileSystemInfo.FullName, FileMode.Open, FileAccess.Read);
				try
				{
					FileInfo dstFileInfo;
					if (dstFileSystemInfo.IsDirectory())
					{
						dstFileInfo = new FileInfo(((DirectoryInfo)dstFileSystemInfo).SubPath(srcFileSystemInfo.Name));
						dstFileInfo.Create().Close();
					}
					else
					{
						DirectoryInfo dstParentDirectoryInfo = ((FileInfo)dstFileSystemInfo).Directory; // 目标文件dst的父级目录
						dstParentDirectoryInfo?.Create();
						if (dstParentDirectoryInfo == null || !dstParentDirectoryInfo.Exists)
							throw new IOException(string.Format("无法创建目录:{0}", dstParentDirectoryInfo));
						dstFileInfo = (FileInfo)dstFileSystemInfo;
						dstFileInfo.Create().Close();
					}

					if (!(srcFileSystemInfo).Equals(dstFileInfo))
					{
						var dstFileStream = new FileStream(dstFileSystemInfo.FullName, FileMode.Truncate,
							FileAccess.Write);
						try
						{
							CopyStream(srcFileStream, dstFileStream);
						}
						finally
						{
							dstFileStream.Close();
						}
					}
				}
				finally
				{
					srcFileStream.Close();
				}
			}
		}

		/// <summary>
		/// 移除文件path（path可以是文件夹）
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static void RemoveFiles(string filePath)
		{
			var directoryInfo = new DirectoryInfo(filePath);
			var fileInfo = new FileInfo(filePath);
			if (directoryInfo.Exists)
				RemoveFile(directoryInfo);
			else
				RemoveFile(fileInfo);
		}

		/// <summary>
		/// 移除文件file（file可以是文件夹）
		/// </summary>
		/// <param name="fileSystemInfo"></param>
		/// <returns></returns>
		public static void RemoveFile(FileSystemInfo fileSystemInfo)
		{
			if (!fileSystemInfo.Exists)
				return;
			if (fileSystemInfo.IsDirectory())
				ClearDir((DirectoryInfo)fileSystemInfo);
			fileSystemInfo.Delete();
		}

		/// <summary>
		/// 移除文件夹dir
		/// </summary>
		/// <param name="dirPath"></param>
		/// <returns></returns>
		public static void ClearDir(string dirPath)
		{
			ClearDir(new DirectoryInfo(dirPath));
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
				FileSystemInfo[] fileSystemInfos = dir.GetFileSystemInfos();
				for (var i = 0; i < fileSystemInfos.Length; i++)
				{
					FileSystemInfo fileSystemInfo = fileSystemInfos[i];
					RemoveFile(fileSystemInfo);
				}
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
			FileSystemInfo[] fileSystemInfos = dir.GetFileSystemInfos();
			var list = new ArrayList();
			for (var i = 0; i < fileSystemInfos.Length; i++)
			{
				FileSystemInfo fileSystemInfo = fileSystemInfos[i];
				if (filter.Accept(fileSystemInfo))
					list.Add(fileSystemInfo);
			}

			fileSystemInfos = new FileSystemInfo[list.Count];
			list.CopyTo(fileSystemInfos);
			for (var i = 0; i < fileSystemInfos.Length; i++)
			{
				FileSystemInfo fileSystemInfo = fileSystemInfos[i];
				if (fileSystemInfo.IsDirectory())
					SearchFiles((DirectoryInfo)fileSystemInfo, filter, results);
				else
					results.Add(fileSystemInfo);
			}
		}

		public static void CreateDirectoryIfNotExist(string path)
		{
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
		}

		public static void CreateFileIfNotExist(string filePath)
		{
			if (!File.Exists(filePath))
			{
				string dirPath = Path.GetDirectoryName(filePath);
				CreateDirectoryIfNotExist(dirPath);
				File.Create(filePath).Dispose();
			}
		}


		/// <summary>
		/// dir目录下创建prefix+yyyyMMddHHmmssSSS(len决定取yyyyMMddHHmmssSSS的多少位)+suffix文件
		/// </summary>
		/// <param name="dir">目录</param>
		/// <param name="prefix">前缀</param>
		/// <param name="suffix">后缀</param>
		/// <param name="length"></param>
		/// <returns></returns>
		private static FileInfo CreateTimelyFile(DirectoryInfo dir, string prefix, string suffix, int length,
			RandomManager randomManager = null)
		{
			randomManager = randomManager ?? Client.instance.randomManager;
			int i = 0;
			do
			{
				string dateTime = DateTimeUtil.NowDateTime().ToString(StringConst.String_yyyyMMddHHmmssSSS);
				string rand = new StringBuilder(randomManager.RandomInt(0, 1000).ToString()).ToString();
				string stem = (dateTime + rand.FillHead(3, CharConst.Char_0))
					.Substring(0, length);

				DirectoryInfo timelyDirectoryInfo = dir.CreateSubdirectory(stem);
				timelyDirectoryInfo.Create();
				if (timelyDirectoryInfo.Exists)
				{
					string fileName = prefix + stem + suffix;
					var fileInfo = new FileInfo(timelyDirectoryInfo.SubPath(fileName));
					if (fileInfo.Exists)
						continue;
					fileInfo.Create().Close();
					return fileInfo;
				}

				throw new IOException(string.Format("无法创建目录:", timelyDirectoryInfo.FullName));
			} while (i++ < 10000);

			throw new IOException(string.Format("{0}中无法创建唯一文件", dir.FullName));
		}
	}
}