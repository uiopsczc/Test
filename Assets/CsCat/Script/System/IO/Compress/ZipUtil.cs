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
		/// <param name="directoryPath">指定待打包的文件夹</param>
		/// <param name="zipFilePath">创建的zip文件完全路径</param>
		/// <returns>是否成功生成</returns>
		public static bool CreateZipFile(string directoryPath, string zipFilePath)
		{
			var isSuccess = false;
			if (!Directory.Exists(directoryPath))
			{
				LogCat.LogErrorFormat("Cannot find directory '{0}'", directoryPath);
				return false;
			}

			try
			{
				var fileNames = Directory.GetFiles(directoryPath);
				using (var zipOutputStream = new ZipOutputStream(File.Create(zipFilePath)))
				{
					zipOutputStream.SetLevel(9); // 压缩级别 0-9
												 //s.Password = "123"; //Zip压缩文件密码
					var buffer = new byte[4096]; //缓冲区大小
					for (var i = 0; i < fileNames.Length; i++)
					{
						var file = fileNames[i];
						var entry = new ZipEntry(Path.GetFileName(file)) {DateTime = DateTimeUtil.NowDateTime()};
						zipOutputStream.PutNextEntry(entry);
						using (var fileStream = File.OpenRead(file))
						{
							int sourceBytes;
							do
							{
								sourceBytes = fileStream.Read(buffer, 0, buffer.Length);
								zipOutputStream.Write(buffer, 0, sourceBytes);
							} while (sourceBytes > 0);
						}
					}

					isSuccess = true;
					zipOutputStream.Finish();
					zipOutputStream.Close();
				}
			}
			catch (Exception ex)
			{
				isSuccess = false;
				LogCat.LogErrorFormat("Exception during processing {0}", ex);
			}

			return isSuccess;
		}

		public void UnZipFile(string zipFilePath)
		{
			if (!File.Exists(zipFilePath))
			{
				LogCat.LogErrorFormat("Cannot find file '{0}'", zipFilePath);
				return;
			}

			using (var s = new ZipInputStream(File.OpenRead(zipFilePath)))
			{
				ZipEntry theEntry;
				while ((theEntry = s.GetNextEntry()) != null)
				{
					var directoryName = Path.GetDirectoryName(theEntry.Name);
					var fileName = Path.GetFileName(theEntry.Name);

					// create directory
					if (directoryName.Length > 0) Directory.CreateDirectory(directoryName);

					if (fileName != string.Empty)
						using (var streamWriter = File.Create(theEntry.Name))
						{
							var size = 2048;
							var data = new byte[size];
							while (true)
							{
								size = s.Read(data, 0, data.Length);
								if (size > 0)
									streamWriter.Write(data, 0, size);
								else
									break;
							}
						}
				}
			}
		}
	}
}