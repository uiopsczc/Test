using System;
using System.Collections.Generic;

namespace CsCat
{
	public class DoubleDictionary<TKey, TValue> : Dictionary<TKey, TValue>
	{
		private readonly Dictionary<TValue, TKey> _valueKeyDict = new Dictionary<TValue, TKey>();

		public new TValue this[TKey key]
		{
			get => base[key];
			set
			{
				base[key] = value;
				_valueKeyDict[value] = key;
			}
		}

		public new void Add(TKey key, TValue value)
		{
			this[key] = value;
		}

		public new void Clear()
		{
			base.Clear();
			this._valueKeyDict.Clear();
		}

		public bool Remove(TKey key, TValue value)
		{
			return base.Remove(key) && _valueKeyDict.Remove(value);
		}

		public bool RemoveByKey(TKey key)
		{
			return this.Remove(key, this[key]);
		}

		public bool RemoveByValue(TValue value)
		{
			return this.Remove(this._valueKeyDict[value], value);
		}

		public bool Contains(TKey key, TValue value)
		{
			return this.ContainsKey(key) && this._valueKeyDict.ContainsKey(value);
		}
		public new bool ContainsValue(TValue value)
		{
			return this._valueKeyDict.ContainsKey(value);
		}


		public void ForeachKeyValue(Action<TKey, TValue> action)
		{
			foreach (var key in this.Keys)
				action(key, this[key]);
		}

		public void ForeachValueKey(Action<TValue, TKey> action)
		{
			foreach (var key in this._valueKeyDict.Keys)
				action(key, this._valueKeyDict[key]);
		}

		public TValue GetValueByKey(TKey key)
		{
			return this[key];
		}

		public TKey GetKeyByValue(TValue value)
		{
			return this._valueKeyDict[value];
		}
	}
}