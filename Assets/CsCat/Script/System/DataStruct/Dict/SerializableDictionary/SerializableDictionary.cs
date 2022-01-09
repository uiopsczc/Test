using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	// Unity的Serializable不能序列化Dict和泛化的类，所以需要在SerializableDictionaryImpl添加对应的类型
	public abstract class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
	{
		[SerializeField] private TKey[] keys;
		[SerializeField] private TValue[] values;

		public Dictionary<TKey, TValue> dict;

		public static T New<T>() where T : SerializableDictionary<TKey, TValue>, new()
		{
			var result = new T();
			result.dict = new Dictionary<TKey, TValue>();
			return result;
		}

		public void OnAfterDeserialize()
		{
			var c = keys.Length;
			dict = new Dictionary<TKey, TValue>(c);
			for (int i = 0; i < c; i++)
			{
				dict[keys[i]] = values[i];
			}

			keys = null;
			values = null;
		}

		public void OnBeforeSerialize()
		{
			var c = dict.Count;
			keys = new TKey[c];
			values = new TValue[c];
			int i = 0;
			using (var e = dict.GetEnumerator())
				while (e.MoveNext())
				{
					var kvp = e.Current;
					keys[i] = kvp.Key;
					values[i] = kvp.Value;
					i++;
				}
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public int Count
		{
			get { return this.dict.Count; }
		}

		public Dictionary<TKey, TValue>.KeyCollection Keys
		{
			get { return this.dict.Keys; }
		}

		public Dictionary<TKey, TValue>.ValueCollection Values
		{
			get { return this.dict.Values; }
		}


		public TValue this[TKey key]
		{
			get { return this.dict[key]; }
			set { this.dict[key] = value; }
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