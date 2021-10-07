using System.Net;
using UnityEngine;

namespace CsCat
{
    public class NetUtil
    {
        private const string InterNetwork_String = "InterNetwork";

        public static string GetLocalIP()
        {
            //获取本地的IP地址
            string ipAddressString = StringConst.String_Empty;
            foreach (IPAddress ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (InterNetwork_String.Equals(ipAddress.AddressFamily.ToString()))
                    ipAddressString = ipAddress.ToString();
            }

            return ipAddressString;
        }

        public static bool IsWifi()
        {
            return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        }
    }
}