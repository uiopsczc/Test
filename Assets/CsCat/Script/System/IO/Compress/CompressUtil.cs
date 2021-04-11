using System.IO;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace CsCat
{
  public class CompressUtil
  {
    #region static method

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
    /// <param name="len"></param>
    /// <returns></returns>
    public static byte[] Compress(byte[] data, int offset, int len)
    {
      return Compress(data, offset, len, -1);
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
    /// <param name="len"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public static byte[] Compress(byte[] data, int offset, int len, int level)
    {
      var outs = new ByteOutputStream();
      Compress(data, offset, len, level, outs);
      data = outs.ToByteArray();
      outs.Close();
      return data;
    }

    /// <summary>
    ///   对ins进行压缩
    /// </summary>
    /// <param name="ins"></param>
    /// <returns></returns>
    public static byte[] Compress(Stream ins)
    {
      var outs = new ByteOutputStream();
      Compress(ins, -1, outs);
      var data = outs.ToByteArray();
      outs.Close();
      return data;
    }

    /// <summary>
    ///   对data进行ins级别的压缩
    /// </summary>
    /// <param name="ins"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public static byte[] Compress(Stream ins, int level)
    {
      var outs = new ByteOutputStream();
      Compress(ins, level, outs);
      var data = outs.ToByteArray();
      outs.Close();
      return data;
    }

    /// <summary>
    ///   对data进行压缩，输出到outs
    /// </summary>
    /// <param name="data"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void Compress(byte[] data, Stream outs)
    {
      Compress(data, 0, data.Length, -1, outs);
    }

    /// <summary>
    ///   对data进行level级别压缩，输出到outs
    /// </summary>
    /// <param name="data"></param>
    /// <param name="level"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void Compress(byte[] data, int level, Stream outs)
    {
      Compress(data, 0, data.Length, level, outs);
    }

    /// <summary>
    ///   对data从offset开始，长度为len进行压缩，输出到outs
    /// </summary>
    /// <param name="data"></param>
    /// <param name="offset"></param>
    /// <param name="len"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void Compress(byte[] data, int offset, int len, Stream outs)

    {
      Compress(data, offset, len, -1, outs);
    }

    /// <summary>
    ///   对data从offset开始，长度为len进行level级别压缩，输出到outs
    /// </summary>
    /// <param name="data"></param>
    /// <param name="offset"></param>
    /// <param name="len"></param>
    /// <param name="level"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void Compress(byte[] data, int offset, int len, int level, Stream outs)
    {
      var deflater = new Deflater();
      if (level >= 0 && level <= 9)
        deflater.SetLevel(level);
      var douts = new DeflaterOutputStream(outs, deflater, 4 * 1024);
      douts.Write(data, offset, len);
      douts.Finish();
    }

    /// <summary>
    ///   对ins压缩，输出到outs
    /// </summary>
    /// <param name="ins"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void Compress(Stream ins, Stream outs)
    {
      Compress(ins, -1, outs);
    }

    /// <summary>
    ///   对ins进行level级别压缩，输出到outs
    /// </summary>
    /// <param name="ins"></param>
    /// <param name="level"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void Compress(Stream ins, int level, Stream outs)
    {
      var deflater = new Deflater();
      if (level >= 0 && level <= 9)
        deflater.SetLevel(level);
      var dos = new DeflaterOutputStream(outs, deflater, 4 * 1024);
      StdioUtil.CopyStream(ins, dos);
      dos.Finish();
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
    /// <param name="len"></param>
    /// <returns></returns>
    public static byte[] Decompress(byte[] data, int offset, int len)
    {
      var os = new ByteOutputStream();
      Decompress(data, offset, len, os);
      data = os.ToByteArray();
      os.Close();
      return data;
    }

    /// <summary>
    ///   对ins进行解压
    /// </summary>
    /// <param name="ins"></param>
    /// <returns></returns>
    public static byte[] Decompress(Stream ins)
    {
      var outs = new ByteOutputStream();
      Decompress(ins, outs);
      var data = outs.ToByteArray();
      outs.Close();
      return data;
    }

    /// <summary>
    ///   对data进行解压，输出到outs
    /// </summary>
    /// <param name="data"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void Decompress(byte[] data, Stream outs)
    {
      Decompress(data, 0, data.Length, outs);
    }

    /// <summary>
    ///   对data从offset开始，长度为len进行解压，输出到outs
    /// </summary>
    /// <param name="data"></param>
    /// <param name="offset"></param>
    /// <param name="len"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void Decompress(byte[] data, int offset, int len, Stream outs)

    {
      Decompress(new ByteInputStream(data, offset, len), outs);
    }

    /// <summary>
    ///   对ins解压，输出到outs
    /// </summary>
    /// <param name="ins"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void Decompress(Stream ins, Stream outs)
    {
      var iis = new InflaterInputStream(ins);
      StdioUtil.CopyStream(iis, outs);
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
    /// <param name="len"></param>
    /// <returns></returns>
    public static byte[] GZipCompress(byte[] data, int offset, int len)
    {
      var os = new ByteOutputStream();
      GZipCompress(data, offset, len, os);
      data = os.ToByteArray();
      os.Close();
      return data;
    }

    /// <summary>
    ///   对ins进行GZip压缩
    /// </summary>
    /// <param name="ins"></param>
    /// <returns></returns>
    public static byte[] GZipCompress(Stream ins)
    {
      var outs = new ByteOutputStream();
      GZipCompress(ins, outs);
      var data = outs.ToByteArray();
      outs.Close();
      return data;
    }

    /// <summary>
    ///   对data进行GZip压缩，输出到outs
    /// </summary>
    /// <param name="data"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void GZipCompress(byte[] data, Stream outs)
    {
      GZipCompress(data, 0, data.Length, outs);
    }

    /// <summary>
    ///   对data从offset开始，长度为len进行GZip压缩，输出到outs
    /// </summary>
    /// <param name="data"></param>
    /// <param name="offset"></param>
    /// <param name="len"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void GZipCompress(byte[] data, int offset, int len, Stream outs)

    {
      var gouts = new GZipStream(outs, CompressionMode.Compress, true);
      gouts.Write(data, offset, len);
      gouts.Close();
    }

    /// <summary>
    ///   对ins进行GZip压缩，输出到outs
    /// </summary>
    /// <param name="ins"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void GZipCompress(Stream ins, Stream outs)
    {
      var gouts = new GZipStream(outs, CompressionMode.Compress, true);
      StdioUtil.CopyStream(ins, gouts);
      gouts.Close();
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
    /// <param name="len"></param>
    /// <returns></returns>
    public static byte[] GZipDecompress(byte[] data, int offset, int len)
    {
      var os = new ByteOutputStream();
      GZipDecompress(data, offset, len, os);
      data = os.ToByteArray();
      os.Close();
      return data;
    }

    /// <summary>
    ///   对ins进行GZip解压
    /// </summary>
    /// <param name="ins"></param>
    /// <returns></returns>
    public static byte[] GZipDecompress(Stream ins)
    {
      var outs = new ByteOutputStream();
      GZipDecompress(ins, outs);
      var data = outs.ToByteArray();
      outs.Close();
      return data;
    }

    /// <summary>
    ///   对data进行GZip解压，输出到outs
    /// </summary>
    /// <param name="data"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void GZipDecompress(byte[] data, Stream outs)
    {
      GZipDecompress(data, 0, data.Length, outs);
    }

    /// <summary>
    ///   对data从offset开始，长度为len进行GZip解压，输出到outs
    /// </summary>
    /// <param name="data"></param>
    /// <param name="offset"></param>
    /// <param name="len"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void GZipDecompress(byte[] data, int offset, int len, Stream outs)
    {
      var gins = new GZipStream(new ByteInputStream(data, offset, len), CompressionMode.Decompress, true);
      StdioUtil.CopyStream(gins, outs);
      gins.Close();
    }

    /// <summary>
    ///   对ins进行GZip解压，输出到outs
    /// </summary>
    /// <param name="ins"></param>
    /// <param name="outs"></param>
    /// <returns></returns>
    public static void GZipDecompress(Stream ins, Stream outs)
    {
      var gins = new GZipStream(ins, CompressionMode.Decompress, true);
      StdioUtil.CopyStream(gins, outs);
      gins.Close();
    }

    #endregion
  }
}