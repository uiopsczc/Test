using System;
using System.Collections.Generic;

namespace CsCat
{
	public class ValueDictList<TKey, TValue> : List<Dictionary<TKey, TValue>>
	{
		public new void Add(Dictionary<TKey, TValue> dict)
		{
			base.Add(dict);
		}

		public void Add(string dictString)
		{
			var dict = dictString.ToDictionary<TKey, TValue>();
			Add(dict);
		}

		public new void Clear()
		{
			base.Clear();
		}


	}
}