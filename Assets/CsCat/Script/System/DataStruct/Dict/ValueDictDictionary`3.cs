using System;
using System.Collections.Generic;

namespace CsCat
{
	public class ValueDictDictionary<TKey1, TKey2, TValue2> : Dictionary<TKey1, Dictionary<TKey2, TValue2>>
	{
		public void Set(TKey1 key1, TKey2 key2, TValue2 value2)
		{
			this.GetOrAddDefault(key1, () => new Dictionary<TKey2, TValue2>());
			this[key1][key2] = value2;
		}

		public new void Clear()
		{
			base.Clear();
		}

		public bool Remove(TKey1 key1, TKey2 key2)
		{
			if (!this.ContainsKey(key1))
				return false;
			var result = this[key1].Remove(key2);
			if (result)
				Check(key1);
			return result;
		}

		public void Check(TKey1 key1)
		{
			if (this.ContainsKey(key1) && this[key1].IsNullOrEmpty())
				this.Remove(key1);
		}

		public bool ContainsKey(TKey1 key1, TKey2 key2)
		{
			return this.ContainsKey(key1) && this[key1].ContainsKey(key2);
		}

		public bool ContainsKey(TKey1 key1, TValue2 value2)
		{
			return this.ContainsKey(key1) && this[key1].ContainsValue(value2);
		}

		public void CheckAll()
		{
			List<TKey1> toRemoveKey1List = new List<TKey1>();
			foreach (var kv in this)
			{
				var key1 = kv.Key;
				if (this[key1].IsNullOrEmpty())
					toRemoveKey1List.Add(key1);
			}

			for (var i = 0; i < toRemoveKey1List.Count; i++)
			{
				var toRemoveKey1 = toRemoveKey1List[i];
				this.Remove(toRemoveKey1);
			}
		}

		public void ForeachKV2OfKey1(TKey1 key1, Action<TKey2, TValue2> action)
		{
			if (!this.ContainsKey(key1))
				return;
			Dictionary<TKey2, TValue2> dict2 = this[key1];
			if (dict2 == null)
				return;

			foreach (KeyValuePair<TKey2, TValue2> kv2 in dict2)
			{
				try
				{
					action(kv2.Key, kv2.Value);
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