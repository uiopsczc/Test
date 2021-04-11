using System.Net;

namespace CsCat
{
  public class IPAddressUtil
  {
    public static string GetLocalIP()
    {
      ///获取本地的IP地址
      var ip_address_string = string.Empty;
      foreach (var ip_address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        if (ip_address.AddressFamily.ToString().Equals("InterNetwork"))
          ip_address_string = ip_address.ToString();
      return ip_address_string;
    }
  }
}