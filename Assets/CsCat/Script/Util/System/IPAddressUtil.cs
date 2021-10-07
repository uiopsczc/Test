namespace CsCat
{
    public class IPAddressUtil
    {
        public static string GetLocalIP()
        {
            return NetUtil.GetLocalIP();
        }
    }
}