using System.Collections;
using LitJson;

namespace CsCat
{
	public static class IsToString2
	{
		/// <summary>
		///   用于Object的ToString2，有ToString2的类必须在这里添加对应的处理
		/// </summary>
		public static string ToString2(object o, bool isFillStringWithDoubleQuote = false)
		{
			switch (o)
			{
				case JsonData jsonData:
					return jsonData.ToJsonWithUTF8();
				case ICollection collection:
					return collection.ToString2(isFillStringWithDoubleQuote);
				case IToString2 string2:
					return string2.ToString2(isFillStringWithDoubleQuote);
			}

			var result = o.ToString();
			if (o is string && isFillStringWithDoubleQuote) result = result.WarpWithDoubleQuotes();
			return result;
		}
	}
}