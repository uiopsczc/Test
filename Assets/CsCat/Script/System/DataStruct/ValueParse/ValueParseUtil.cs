using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public static class ValueParseUtil
	{
		private static readonly List<Hashtable> valueParseList = new List<Hashtable>();

		private static void AddValueParseElement<T>(Func<string, T> func)
		{
			var hashtable = new Hashtable
			{ [StringConst.String_type] = typeof(T), [StringConst.String_parseFunc] = func };
			valueParseList.Add(hashtable);
		}

		public static List<Hashtable> GetValueParseList()
		{
			valueParseList.Clear();
			AddValueParseElement(content => content);
			AddValueParseElement(bool.Parse);
			AddValueParseElement(char.Parse);
			AddValueParseElement(short.Parse);
			AddValueParseElement(int.Parse);
			AddValueParseElement(long.Parse);
			AddValueParseElement(content => content.ToVector3());
			AddValueParseElement(content => content.ToVector2());
			return valueParseList;
		}
	}
}