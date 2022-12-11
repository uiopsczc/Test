using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	// Unity的Serializable不能序列化Dict和泛化的类，所以需要在SerializableDictionaryImpl添加对应的类型
	public abstract class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
	{
		[SerializeField] private TKey[] _keys;
		[SerializeField] private TValue[] _values;

		public Dictionary<TKey, TValue> dict;

		public static T New<T>() where T : SerializableDictionary<TKey, TValue>, new()
		{
			var result = new T {dict = new Dictionary<TKey, TValue>()};
			return result;
		}

		public void OnAfterDeserialize()
		{
			var length = _keys.Length;
			dict = new Dictionary<TKey, TValue>(length);
			for (int i = 0; i < length; i++)
			{
				dict[_keys[i]] = _values[i];
			}

			_keys = null;
			_values = null;
		}

		public void OnBeforeSerialize()
		{
			var c = dict.Count;
			_keys = new TKey[c];
			_values = new TValue[c];
			int i = 0;
			using (var e = dict.GetEnumerator())
				while (e.MoveNext())
				{
					var kvp = e.Current;
					_keys[i] = kvp.Key;
					_values[i] = kvp.Value;
					i++;
				}
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public int Count => this.dict.Count;

		public Dictionary<TKey, TValue>.KeyCollection Keys => this.dict.Keys;

		public Dictionary<TKey, TValue>.ValueCollection Values => this.dict.Values;


		public TValue this[TKey key]
		{
			get => this.dict[key];
			set => this.dict[key] = value;
		}

		public void Add(TKey key, TValue value)
		{
			this.dict.Add(key, value);
		}


		public void Clear()
		{
			this.dict.Clear();
		}

		public bool ContainsKey(TKey key)
		{
			return this.dict.ContainsKey(key);
		}

		public bool ContainsValue(TValue value)
		{
			return this.dict.ContainsValue(value);
		}


		public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return this.dict.GetEnumerator();
		}

		public bool Remove(TKey key)
		{
			return this.dict.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.dict.TryGetValue(key, out value);
		}


		public override string ToString()
		{
			return dict.ToString2();
		}
	}
}