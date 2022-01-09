using System;
using System.Collections.Generic;

namespace CsCat
{
	public class DoubleDictionary<TKey, TValue> : Dictionary<TKey, TValue>
	{
		private Dictionary<TValue, TKey> value_key_dict = new Dictionary<TValue, TKey>();

		public new TValue this[TKey key]
		{
			get { return base[key]; }
			set
			{
				base[key] = value;
				value_key_dict[value] = key;
			}
		}

		public void Add(TKey key, TValue value)
		{
			this[key] = value;
		}

		public new void Clear()
		{
			base.Clear();
			this.value_key_dict.Clear();
		}

		public bool Remove(TKey key, TValue value)
		{
			if (base.Remove(key))
				return value_key_dict.Remove(value);
			return false;
		}

		public bool RemoveByKey(TKey key)
		{
			return this.Remove(key, this[key]);
		}

		public bool RemoveByValue(TValue value)
		{
			return this.Remove(this.value_key_dict[value], value);
		}

		public bool Contains(TKey key, TValue value)
		{
			return this.ContainsKey(key) && this.value_key_dict.ContainsKey(value);
		}
		public new bool ContainsValue(TValue value)
		{
			return this.value_key_dict.ContainsKey(value);
		}


		public void ForeachKeyValue(Action<TKey, TValue> action)
		{
			foreach (var key in this.Keys)
				action(key, this[key]);
		}

		public void ForeachValueKey(Action<TValue, TKey> action)
		{
			foreach (var key in this.value_key_dict.Keys)
				action(key, this.value_key_dict[key]);
		}

		public TValue GetValueByKey(TKey key)
		{
			return this[key];
		}

		public TKey GetKeyByValue(TValue value)
		{
			return this.value_key_dict[value];
		}
	}
}