using System.Security.Cryptography;
using System.Text;

namespace CsCat
{
  public class MD5Util
  {
    /// <summary>
    ///   MD5加密
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string Encrypt(string s)
    {
      var md5_hash = MD5.Create();
      var data = md5_hash.ComputeHash(s.GetBytes());
      var stringBuilder = new StringBuilder();
      for (var i = 0; i < data.Length; i++) stringBuilder.Append(data[i].ToString("x2"));
      return stringBuilder.ToString();
    }
  }
}