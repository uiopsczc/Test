using System.Collections.Generic;

namespace CsCat
{
	public static class DoerAttrParserConst
	{
		public static string Pattern1 = @"\{.*?\}";
		public static string Pattern2 = @"\$\{.*?\}";
		public static string Pattern3 = @"\$\$\{.*?\}";

		public static List<string> Type_String_List = new List<string>()
		{
			"", //整数
            "%", //浮点数
            "@", //布尔值
            "#", //字符串
        };
	}
}