using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class LinkedDictionary<K, V>
	{
		public class DictionaryEnumerator<K, V> : IEnumerator<KeyValuePair<K, V>>
		{
			private List<K> _keyList;
			private List<V> _valueList;
			private int _position = -1;
			private KeyValuePair<K, V> _current;


			public DictionaryEnumerator(List<K> keyList, List<V> valueList)
			{
				Init(keyList, valueList);
			}

			public void Init(List<K> keyList, List<V> valueList)
			{
				this._keyList = keyList;
				this._valueList = valueList;
			}

			public bool MoveNext()
			{
				_position++;
				return _position < _keyList.Count;
			}

			public void Reset()
			{
				_position = -1;
			}

			object IEnumerator.Current => Current;

			public KeyValuePair<K, V> Current
			{
				get
				{
					_current = new KeyValuePair<K, V>(_keyList[_position], _valueList[_position]);
					return _current;
				}
			}

			public void Dispose()
			{
			}
		}
	}
}