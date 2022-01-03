using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsCat
{
	public static class ICollectionExtension
	{
		public static bool IsNullOrEmpty(this ICollection self)
		{
			return self == null || self.Count == 0;
		}

		public static T[] ToArray<T>(this ICollection self)
		{
			T[] result = new T[self.Count];
			int curIndex = -1;
			var iterator = self.GetEnumerator();
			while (iterator.MoveNext(ref curIndex))
				result[curIndex] = (T)iterator.Current;
			return result;
		}

		public static List<T> ToList<T>(this ICollection self)
		{
			List<T> result = new List<T>(self.Count);
			int curIndex = -1;
			var iterator = self.GetEnumerator();
			while (iterator.MoveNext(ref curIndex))
				result.Add((T)iterator.Current);
			return result;
		}


		#region ToStrign2

		public static string ToString2(this ICollection self, bool isFillStringWithDoubleQuote = false)
		{
			bool isFirst = true;
			using (var scope = new StringBuilderScope())
			{
				switch (self)
				{
					case Array _:
						scope.stringBuilder.Append(StringConst.String_LeftRoundBrackets);
						break;
					case IList _:
						scope.stringBuilder.Append(StringConst.String_LeftSquareBrackets);
						break;
					case IDictionary _:
						scope.stringBuilder.Append(StringConst.String_LeftCurlyBrackets);
						break;
				}

				if (self is IDictionary dictionary)
				{
					foreach (var key in dictionary.Keys)
					{
						if (isFirst)
							isFirst = false;
						else
							scope.stringBuilder.Append(StringConst.String_Comma);
						scope.stringBuilder.Append(key.ToString2(isFillStringWithDoubleQuote));
						scope.stringBuilder.Append(StringConst.String_Colon);
						object value = dictionary[key];
						scope.stringBuilder.Append(value.ToString2(isFillStringWithDoubleQuote));
					}
				}
				else //list
				{
					foreach (var o in self)
					{
						if (isFirst)
							isFirst = false;
						else
							scope.stringBuilder.Append(StringConst.String_Comma);
						scope.stringBuilder.Append(o.ToString2(isFillStringWithDoubleQuote));
					}
				}

				switch (self)
				{
					case Array _:
						scope.stringBuilder.Append(StringConst.String_RightRoundBrackets);
						break;
					case IList _:
						scope.stringBuilder.Append(StringConst.String_RightSquareBrackets);
						break;
					case IDictionary _:
						scope.stringBuilder.Append(StringConst.String_RightCurlyBrackets);
						break;
				}

				return scope.stringBuilder.ToString();
			}
		}

		#endregion
	}
}