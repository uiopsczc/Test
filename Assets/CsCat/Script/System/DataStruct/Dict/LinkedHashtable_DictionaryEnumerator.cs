using System.Collections;

namespace CsCat
{
	public partial class LinkedHashtable
	{
		class DictionaryEnumerator : IDictionaryEnumerator
		{

			private ArrayList _keyList;
			private ArrayList _valueList;
			private int _position = -1;
			private DictionaryEntry _current;
			private DictionaryEntry _entry;


			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					object key = _keyList[_position];
					object value = _valueList[_position];
					_entry.Key = key;
					_entry.Value = value;
					return _entry;
				}
			}

			object IDictionaryEnumerator.Key => _keyList[_position];

			object IDictionaryEnumerator.Value => _valueList[_position];

			object IEnumerator.Current
			{
				get
				{
					object key = _keyList[_position];
					object value = _valueList[_position];
					_current.Key = key;
					_current.Value = value;
					return _current;
				}
			}


			public DictionaryEnumerator(ArrayList keyList, ArrayList valueList)
			{
				Init(keyList, valueList);
			}

			public void Init(ArrayList keyList, ArrayList valueList)
			{
				this._keyList = keyList;
				this._valueList = valueList;
			}

			public void Reset()
			{
				_position = -1;
			}


			bool IEnumerator.MoveNext()
			{
				_position++;
				return _position < _keyList.Count;
			}

			void IEnumerator.Reset()
			{
				Reset();
			}

		}
	}
}