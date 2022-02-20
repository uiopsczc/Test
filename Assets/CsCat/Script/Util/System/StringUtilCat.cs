using System.Collections.Generic;

namespace CsCat
{
	public class StringUtilCat
	{
		public static string[] SplitIgnore(string self, string split = StringConst.String_Comma,
			string ignoreLeft = StringConst.String_Regex_DoubleQuotes,
			string ignoreRight = null)
		{
			return self.SplitIgnore(split, ignoreLeft, ignoreRight);
		}

		public static bool IsNumber(string self)
		{
			return self.IsNumber();
		}

		public static string LinkString(string split, params string[] args)
		{
			using (var scope = new StringBuilderScope())
			{
				for (int i = 0; i < args.Length; i++)
				{
					string arg = args[i];
					if (i == args.Length - 1)
						scope.stringBuilder.Append(arg);
					else
						scope.stringBuilder.Append(arg + split);
				}

				return scope.stringBuilder.ToString();
			}
		}

		public static string LinkStringWithCommon(params string[] args)
		{
			if (args == null || args.Length == 0)
				return null;

			switch (args.Length)
			{
				case 1:
					return args.ToString();
				case 2:
					return string.Format(StringConst.String_Format_LinkComma_2, args);
				case 3:
					return string.Format(StringConst.String_Format_LinkComma_3, args);
				case 4:
					return string.Format(StringConst.String_Format_LinkComma_4, args);
				case 5:
					return string.Format(StringConst.String_Format_LinkComma_5, args);
				case 6:
					return string.Format(StringConst.String_Format_LinkComma_6, args);
				case 7:
					return string.Format(StringConst.String_Format_LinkComma_7, args);
				case 8:
					return string.Format(StringConst.String_Format_LinkComma_8, args);
				case 9:
					return string.Format(StringConst.String_Format_LinkComma_9, args);
				case 10:
					return string.Format(StringConst.String_Format_LinkComma_10, args);
				default:
					throw new ExceptionArgsTooLong();
			}
		}

		public static string LinkStringWithUnderLine(params string[] args)
		{
			if (args == null || args.Length == 0)
				return null;

			switch (args.Length)
			{
				case 1:
					return args.ToString();
				case 2:
					return string.Format(StringConst.String_Format_LinkUnderLine_2, args);
				case 3:
					return string.Format(StringConst.String_Format_LinkUnderLine_3, args);
				case 4:
					return string.Format(StringConst.String_Format_LinkUnderLine_4, args);
				case 5:
					return string.Format(StringConst.String_Format_LinkUnderLine_5, args);
				case 6:
					return string.Format(StringConst.String_Format_LinkUnderLine_6, args);
				case 7:
					return string.Format(StringConst.String_Format_LinkUnderLine_7, args);
				case 8:
					return string.Format(StringConst.String_Format_LinkUnderLine_8, args);
				case 9:
					return string.Format(StringConst.String_Format_LinkUnderLine_9, args);
				case 10:
					return string.Format(StringConst.String_Format_LinkUnderLine_10, args);
				default:
					throw new ExceptionArgsTooLong();
			}
		}

		public static string RoundBrackets(string arg)
		{
			return string.Format(StringConst.String_Format_RoundBrackets, arg);
		}

		public static int CheckInsertLine(string content, int startCheckInsertIndex, List<string> lineList)
		{
			content = content.Trim(new[] {'\r', '\n'});
			int insertLineIndex = lineList.IndexOf(content, startCheckInsertIndex);
			//如果lineList中没有content的内容的行，则直接插入
			if (insertLineIndex < 0)
			{
				lineList.Insert(startCheckInsertIndex, content);
				insertLineIndex = startCheckInsertIndex;
			}

			return insertLineIndex;
		}
	}
}