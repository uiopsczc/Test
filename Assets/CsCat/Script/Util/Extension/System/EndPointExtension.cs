using System.Net;

namespace CsCat
{
	public static class EndPointExtension
	{
		public static string Ip(this EndPoint self)
		{
			string address = self.ToString();
			int index = address.IndexOf(CharConst.Char_Colon);
			return address.Substring(0, index);
		}

		public static string Port(this EndPoint self)
		{
			string address = self.ToString();
			int index = address.IndexOf(CharConst.Char_Colon);
			return address.Substring(index + 1);
		}
	}
}