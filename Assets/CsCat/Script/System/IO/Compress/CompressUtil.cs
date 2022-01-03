using System.IO;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace CsCat
{
	public class CompressUtil
	{
		/// <summary>
		///   对data进行压缩
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static byte[] Compress(byte[] data)
		{
			return Compress(data, 0, data.Length, -1);
		}

		/// <summary>
		///   对data从offset开始，长度为len进行压缩
		/// </summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static byte[] Compress(byte[] data, int offset, int length)
		{
			return Compress(data, offset, length, -1);
		}

		/// <summary>
		///   对data进行level级别的压缩
		/// </summary>
		/// <param name="data"></param>
		/// <param name="level"></param>
		/// <returns></returns>
		public static byte[] Compress(byte[] data, int level)
		{
			return Compress(data, 0, data.Length, level);
		}

		/// <summary>
		///   对data从offset开始，长度为len进行level级别的压缩
		/// </summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <param name="level"></param>
		/// <returns></returns>
		public static byte[] Compress(byte[] data, int offset, int length, int level)
		{
			var byteOutputStream = new ByteOutputStream();
			Compress(data, offset, length, level, byteOutputStream);
			data = byteOutputStream.ToByteArray();
			byteOutputStream.Close();
			return data;
		}

		/// <summary>
		///   对ins进行压缩
		/// </summary>
		/// <param name="inStream"></param>
		/// <returns></returns>
		public static byte[] Compress(Stream inStream)
		{
			var outStream = new ByteOutputStream();
			Compress(inStream, -1, outStream);
			var data = outStream.ToByteArray();
			outStream.Close();
			return data;
		}

		/// <summary>
		///   对data进行ins级别的压缩
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="level"></param>
		/// <returns></returns>
		public static byte[] Compress(Stream inStream, int level)
		{
			var outStream = new ByteOutputStream();
			Compress(inStream, level, outStream);
			var data = outStream.ToByteArray();
			outStream.Close();
			return data;
		}

		/// <summary>
		///   对data进行压缩，输出到outs
		/// </summary>
		/// <param name="data"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void Compress(byte[] data, Stream outStream)
		{
			Compress(data, 0, data.Length, -1, outStream);
		}

		/// <summary>
		///   对data进行level级别压缩，输出到outs
		/// </summary>
		/// <param name="data"></param>
		/// <param name="level"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void Compress(byte[] data, int level, Stream outStream)
		{
			Compress(data, 0, data.Length, level, outStream);
		}

		/// <summary>
		///   对data从offset开始，长度为len进行压缩，输出到outs
		/// </summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void Compress(byte[] data, int offset, int length, Stream outStream)

		{
			Compress(data, offset, length, -1, outStream);
		}

		/// <summary>
		///   对data从offset开始，长度为len进行level级别压缩，输出到outs
		/// </summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <param name="level"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void Compress(byte[] data, int offset, int length, int level, Stream outStream)
		{
			var deflater = new Deflater();
			if (level >= 0 && level <= 9)
				deflater.SetLevel(level);
			var deflaterOutputStream = new DeflaterOutputStream(outStream, deflater, 4 * 1024);
			deflaterOutputStream.Write(data, offset, length);
			deflaterOutputStream.Finish();
		}

		/// <summary>
		///   对ins压缩，输出到outs
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void Compress(Stream inStream, Stream outStream)
		{
			Compress(inStream, -1, outStream);
		}

		/// <summary>
		///   对ins进行level级别压缩，输出到outs
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="level"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void Compress(Stream inStream, int level, Stream outStream)
		{
			var deflater = new Deflater();
			if (level >= 0 && level <= 9)
				deflater.SetLevel(level);
			var deflaterOutputStream = new DeflaterOutputStream(outStream, deflater, 4 * 1024);
			StdioUtil.CopyStream(inStream, deflaterOutputStream);
			deflaterOutputStream.Finish();
		}

		/// <summary>
		///   对data进行解压
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static byte[] Decompress(byte[] data)
		{
			return Decompress(data, 0, data.Length);
		}

		/// <summary>
		///   对data从offset开始，长度为len进行解压
		/// </summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static byte[] Decompress(byte[] data, int offset, int length)
		{
			var byteOutputStream = new ByteOutputStream();
			Decompress(data, offset, length, byteOutputStream);
			data = byteOutputStream.ToByteArray();
			byteOutputStream.Close();
			return data;
		}

		/// <summary>
		///   对ins进行解压
		/// </summary>
		/// <param name="inStream"></param>
		/// <returns></returns>
		public static byte[] Decompress(Stream inStream)
		{
			var outStream = new ByteOutputStream();
			Decompress(inStream, outStream);
			var data = outStream.ToByteArray();
			outStream.Close();
			return data;
		}

		/// <summary>
		///   对data进行解压，输出到outs
		/// </summary>
		/// <param name="data"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void Decompress(byte[] data, Stream outStream)
		{
			Decompress(data, 0, data.Length, outStream);
		}

		/// <summary>
		///   对data从offset开始，长度为len进行解压，输出到outs
		/// </summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void Decompress(byte[] data, int offset, int length, Stream outStream)

		{
			Decompress(new ByteInputStream(data, offset, length), outStream);
		}

		/// <summary>
		///   对ins解压，输出到outs
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void Decompress(Stream inStream, Stream outStream)
		{
			var iis = new InflaterInputStream(inStream);
			StdioUtil.CopyStream(iis, outStream);
			iis.Close();
		}

		/// <summary>
		///   对data进行GZip压缩
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static byte[] GZipCompress(byte[] data)
		{
			return GZipCompress(data, 0, data.Length);
		}

		/// <summary>
		///   对data从offset开始，长度为len进行GZip压缩
		/// </summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static byte[] GZipCompress(byte[] data, int offset, int length)
		{
			var byteOutputStream = new ByteOutputStream();
			GZipCompress(data, offset, length, byteOutputStream);
			data = byteOutputStream.ToByteArray();
			byteOutputStream.Close();
			return data;
		}

		/// <summary>
		///   对ins进行GZip压缩
		/// </summary>
		/// <param name="inStream"></param>
		/// <returns></returns>
		public static byte[] GZipCompress(Stream inStream)
		{
			var outStream = new ByteOutputStream();
			GZipCompress(inStream, outStream);
			var data = outStream.ToByteArray();
			outStream.Close();
			return data;
		}

		/// <summary>
		///   对data进行GZip压缩，输出到outs
		/// </summary>
		/// <param name="data"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void GZipCompress(byte[] data, Stream outStream)
		{
			GZipCompress(data, 0, data.Length, outStream);
		}

		/// <summary>
		///   对data从offset开始，长度为len进行GZip压缩，输出到outs
		/// </summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void GZipCompress(byte[] data, int offset, int length, Stream outStream)

		{
			var gzipStream = new GZipStream(outStream, CompressionMode.Compress, true);
			gzipStream.Write(data, offset, length);
			gzipStream.Close();
		}

		/// <summary>
		///   对ins进行GZip压缩，输出到outs
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void GZipCompress(Stream inStream, Stream outStream)
		{
			var gzipStream = new GZipStream(outStream, CompressionMode.Compress, true);
			StdioUtil.CopyStream(inStream, gzipStream);
			gzipStream.Close();
		}

		/// <summary>
		///   对data进行GZip解压
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static byte[] GZipDecompress(byte[] data)
		{
			return GZipDecompress(data, 0, data.Length);
		}

		/// <summary>
		///   对data从offset开始，长度为len进行GZip解压
		/// </summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static byte[] GZipDecompress(byte[] data, int offset, int length)
		{
			var byteOutputStream = new ByteOutputStream();
			GZipDecompress(data, offset, length, byteOutputStream);
			data = byteOutputStream.ToByteArray();
			byteOutputStream.Close();
			return data;
		}

		/// <summary>
		///   对ins进行GZip解压
		/// </summary>
		/// <param name="inStream"></param>
		/// <returns></returns>
		public static byte[] GZipDecompress(Stream inStream)
		{
			var byteOutputStream = new ByteOutputStream();
			GZipDecompress(inStream, byteOutputStream);
			var data = byteOutputStream.ToByteArray();
			byteOutputStream.Close();
			return data;
		}

		/// <summary>
		///   对data进行GZip解压，输出到outs
		/// </summary>
		/// <param name="data"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void GZipDecompress(byte[] data, Stream outStream)
		{
			GZipDecompress(data, 0, data.Length, outStream);
		}

		/// <summary>
		///   对data从offset开始，长度为len进行GZip解压，输出到outs
		/// </summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void GZipDecompress(byte[] data, int offset, int length, Stream outStream)
		{
			var inGZipStream =
				new GZipStream(new ByteInputStream(data, offset, length), CompressionMode.Decompress, true);
			StdioUtil.CopyStream(inGZipStream, outStream);
			inGZipStream.Close();
		}

		/// <summary>
		///   对ins进行GZip解压，输出到outs
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void GZipDecompress(Stream inStream, Stream outStream)
		{
			var inGZipStream = new GZipStream(inStream, CompressionMode.Decompress, true);
			StdioUtil.CopyStream(inGZipStream, outStream);
			inGZipStream.Close();
		}
	}
}