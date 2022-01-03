using System.Collections.Generic;

namespace CsCat
{
	public class LangConst
	{
		public static string Json_File_Path = "Assets/Cfg/lang.json";
		private static Dictionary<char, bool> _Char_Exclude_Dict;

		public static Dictionary<char, bool> CharExcludeDict =>
			_Char_Exclude_Dict ?? (_Char_Exclude_Dict = new Dictionary<char, bool>
			{
				[' '] = true,
				['0'] = true,
				['1'] = true,
				['2'] = true,
				['3'] = true,
				['4'] = true,
				['5'] = true,
				['6'] = true,
				['7'] = true,
				['8'] = true,
				['9'] = true,
				// 英文符号
				[','] = true,
				['<'] = true,
				['.'] = true,
				['>'] = true,
				['/'] = true,
				['?'] = true,
				[';'] = true,
				[':'] = true,
				['\''] = true,
				['"'] = true,
				['['] = true,
				['{'] = true,
				[']'] = true,
				['}'] = true,
				['\\'] = true,
				['|'] = true,
				['`'] = true,
				['~'] = true,
				['!'] = true,
				['@'] = true,
				['#'] = true,
				['$'] = true,
				['%'] = true,
				['^'] = true,
				['&'] = true,
				['*'] = true,
				['('] = true,
				[')'] = true,
				['-'] = true,
				['_'] = true,
				['='] = true,
				['+'] = true,
				//中文符号
				['，'] = true,
				['《'] = true,
				['。'] = true,
				['》'] = true,
				['、'] = true,
				['？'] = true,
				['；'] = true,
				['：'] = true,
				['‘'] = true,
				['’'] = true,
				['“'] = true,
				['”'] = true,
				['【'] = true,
				['｛'] = true,
				['】'] = true,
				['｝'] = true,
				['、'] = true,
				['|'] = true,
				['·'] = true,
				['~'] = true,
				['！'] = true,
				['@'] = true,
				['#'] = true,
				['￥'] = true,
				['%'] = true,
				['…'] = true,
				['&'] = true,
				['*'] = true,
				['（'] = true,
				['）'] = true,
				['-'] = true,
				['—'] = true,
				['='] = true,
				['+'] = true
			});
	}
}