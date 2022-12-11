using System;
using System.Collections.Generic;

namespace CsCat
{
	public class ValueListDictionary<TKey, TValue> : Dictionary<TKey, List<TValue>>
	{
		public void Add(TKey key, TValue value, bool isValueUnique = false)
		{
			this.GetOrAddDefault(key, () => new List<TValue>());
			if (isValueUnique && this[key].Contains(value))
				return;
			this[key].Add(value);
		}

		public new void Clear()
		{
			base.Clear();
		}

		public bool Remove(TKey key, TValue value)
		{
			if (!this.ContainsKey(key))
				return false;
			var result = this[key].Remove(value);
			if (result)
				Check(key);
			return result;
		}

		public bool Contains(TKey key, TValue value)
		{
			return this.ContainsKey(key) && this[key].Contains(value);
		}

		public void Check(TKey key)
		{
			if (this.ContainsKey(key) && this[key].IsNullOrEmpty())
				this.Remove(key);
		}

		public void CheckAll()
		{
			List<TKey> toRemoveKeyList = new List<TKey>();
			foreach (var kv in this)
			{
				var key = kv.Key;
				if (this[key].IsNullOrEmpty())
					toRemoveKeyList.Add(key);
			}

			for (var i = 0; i < toRemoveKeyList.Count; i++)
			{
				var toRemoveKey = toRemoveKeyList[i];
				this.Remove(toRemoveKey);
			}
		}

		public void ForEach(Action<TKey, TValue> action)
		{
			foreach (var kv in this)
			{
				var key = kv.Key;
				var valueList = this[key];
				for (var i = 0; i < valueList.Count; i++)
				{
					var value = valueList[i];
					action(key, value);
				}
			}

			CheckAll();
		}

		public void Foreach(TKey key, Action<TValue> action, bool isIgnoreValueNull = true)
		{
			if (!this.ContainsKey(key))
				return;
			List<TValue> valueList = this[key];
			if (valueList == null)
				return;

			for (var i = 0; i < valueList.Count; i++)
			{
				TValue value = valueList[i];
				if (isIgnoreValueNull && value == null)
					continue;
				try
				{
					action(value);
				}
				catch (Exception e)
				{
					LogCat.LogError(e);
				}
			}

			CheckAll();
		}
	}
}