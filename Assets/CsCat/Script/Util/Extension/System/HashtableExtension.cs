using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public static class HashtableExtension
	{
		public static Dictionary<K, V> ToDict<K, V>(this Hashtable self)
		{
			Dictionary<K, V> dict = new Dictionary<K, V>();
			foreach (DictionaryEntry dictionaryEntry in self)
			{
				var key = dictionaryEntry.Key.To<K>();
				var value = dictionaryEntry.Value.To<V>();
				dict[key] = value;
			}
			return dict;
		}
	}
}