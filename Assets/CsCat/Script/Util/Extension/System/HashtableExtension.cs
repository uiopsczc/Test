using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public static class HashtableExtension
	{
		public static Dictionary<K, V> ToDict<K, V>(this Hashtable self)
		{
			Dictionary<K, V> dict = new Dictionary<K, V>();
			foreach (var key in self.Keys)
				dict[key.To<K>()] = self[key].To<V>();
			return dict;
		}
	}
}