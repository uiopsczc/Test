using System;
using System.Collections.Generic;

namespace CsCat
{
	public class DoubleDictionary<TKey, TValue> : Dictionary<TKey, TValue>
	{
		private Dictionary<TValue, TKey> valueKeyDict = new Dictionary<TValue, TKey>();

		public new TValue this[TKey key]
		{
			get => base[key];
			set
			{
				base[key] = value;
				valueKeyDict[value] = key;
			}
		}

		public void Add(TKey key, TValue value)
		{
			this[key] = value;
		}

		public new void Clear()
		{
			base.Clear();
			this.valueKeyDict.Clear();
		}

		public bool Remove(TKey key, TValue value)
		{
			return base.Remove(key) && valueKeyDict.Remove(value);
		}

		public bool RemoveByKey(TKey key)
		{
			return this.Remove(key, this[key]);
		}

		public bool RemoveByValue(TValue value)
		{
			return this.Remove(this.valueKeyDict[value], value);
		}

		public bool Contains(TKey key, TValue value)
		{
			return this.ContainsKey(key) && this.valueKeyDict.ContainsKey(value);
		}
		public new bool ContainsValue(TValue value)
		{
			return this.valueKeyDict.ContainsKey(value);
		}


		public void ForeachKeyValue(Action<TKey, TValue> action)
		{
			foreach (var key in this.Keys)
				action(key, this[key]);
		}

		public void ForeachValueKey(Action<TValue, TKey> action)
		{
			foreach (var key in this.valueKeyDict.Keys)
				action(key, this.valueKeyDict[key]);
		}

		public TValue GetValueByKey(TKey key)
		{
			return this[key];
		}

		public TKey GetKeyByValue(TValue value)
		{
			return this.valueKeyDict[value];
		}
	}
}