using System;
using System.Collections.Generic;

namespace CsCat
{
	public static class CompareUtil
	{
		public static int CompareWithRules<T>(T data1, T data2, IList<Comparison<T>> compareRules)
		{
			for (var i = 0; i < compareRules.Count; i++)
			{
				var compareRule = compareRules[i];
				if (compareRule == null)
					continue;
				var result = compareRule(data1, data2);
				if (result == 0)
					continue;
				return result;
			}

			return 0;
		}
	}
}