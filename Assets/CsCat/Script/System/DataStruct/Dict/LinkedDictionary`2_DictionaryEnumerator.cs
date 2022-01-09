using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class LinkedDictionary<K, V>
	{
		public class DictionaryEnumerator<K, V> : IEnumerator<KeyValuePair<K, V>>
		{

			List<K> keyList;
			List<V> valueList;
			int position = -1;
			private KeyValuePair<K, V> current;



			public DictionaryEnumerator(List<K> keyList, List<V> valueList)
			{
				Init(keyList, valueList);
			}

			public void Init(List<K> keyList, List<V> valueList)
			{
				this.keyList = keyList;
				this.valueList = valueList;
			}

			public bool MoveNext()
			{
				position++;
				return position < keyList.Count;
			}

			public void Reset()
			{
				position = -1;
			}

			object IEnumerator.Current => Current;

			public KeyValuePair<K, V> Current
			{
				get
				{
					current = new KeyValuePair<K, V>(keyList[position], valueList[position]);
					return current;
				}
			}

			public void Dispose()
			{
			}
		}
	}
}