

using System.Net;
using UnityEngine;

namespace CsCat
{
  public class NetUtil
  {
    public static string GetLocalIP()
    {
      ///获取本地的IP地址
      string ip_address_string = string.Empty;
      foreach (IPAddress ip_address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
      {
        if (ip_address.AddressFamily.ToString().Equals("InterNetwork"))
        {
          ip_address_string = ip_address.ToString();
        }
      }

      return ip_address_string;
    }

    public static bool IsWifi()
    {
      return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
    }
  }
}
