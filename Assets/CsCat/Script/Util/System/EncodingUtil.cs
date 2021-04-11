using System;
using System.IO;
using System.Text;

namespace CsCat
{
  public class EncodingUtil
  {
    /// <summary>
    ///   对data从offset开始，长度为len，进行encoding编码后获取字符串
    /// </summary>
    public static string GetString(byte[] data, int offset, int len, Encoding encoding = null)
    {
      return encoding.GetString(data, offset, len);
    }


    /// <summary>
    ///   对s进行encoding解码，然后获取对应的bytes
    /// </summary>
    /// <param name="s"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static byte[] GetBytes(string s, Encoding encoding = null)
    {
      return encoding.GetBytes(s);
    }

    public static Encoding GetEncoding(string file_path)
    {
      FileStream fileStream = new FileStream(file_path, FileMode.Open, FileAccess.Read);
      Encoding encoding = GetEncoding(fileStream);
      fileStream.Close();
      return encoding;
    }



    public static Encoding GetEncoding(FileStream fileStream)
    {
      byte[] Unicode = new byte[] {0xFF, 0xFE, 0x41};
      byte[] UnicodeBIG = new byte[] {0xFE, 0xFF, 0x00};
      byte[] UTF8 = new byte[] {0xEF, 0xBB, 0xBF}; //带BOM 
      Encoding reVal = Encoding.Default;

      BinaryReader r = new BinaryReader(fileStream, System.Text.Encoding.Default);
      int i;
      int.TryParse(fileStream.Length.ToString(), out i);
      byte[] ss = r.ReadBytes(i);
      if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
      {
        reVal = Encoding.UTF8;
      }
      else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
      {
        reVal = Encoding.BigEndianUnicode;
      }
      else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
      {
        reVal = Encoding.Unicode;
      }

      r.Close();
      return reVal;
    }


    /// <summary> 
    /// 判断是否是不带 BOM 的 UTF8 格式 
    /// </summary> 
    /// <param name=“data“></param> 
    /// <returns></returns> 
    private static bool IsUTF8Bytes(byte[] data)
    {
      int charByteCounter = 1; //计算当前正分析的字符应还有的字节数 
      byte curByte; //当前分析的字节. 
      for (int i = 0; i < data.Length; i++)
      {
        curByte = data[i];
        if (charByteCounter == 1)
        {
          if (curByte >= 0x80)
          {
            //判断当前 
            while (((curByte <<= 1) & 0x80) != 0)
            {
              charByteCounter++;
            }

            //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X 
            if (charByteCounter == 1 || charByteCounter > 6)
            {
              return false;
            }
          }
        }
        else
        {
          //若是UTF-8 此时第一位必须为1 
          if ((curByte & 0xC0) != 0x80)
          {
            return false;
          }

          charByteCounter--;
        }
      }

      if (charByteCounter > 1)
      {
        throw new Exception("非预期的byte格式");
      }

      return true;
    }
  }
}