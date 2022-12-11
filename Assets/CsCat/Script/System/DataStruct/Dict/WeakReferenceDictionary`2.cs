using System.Collections.Generic;
using System;

namespace CsCat
{
	/// <summary>
	/// WeakReferenceDictionary
	/// </summary>
	/// <typeparam name="K"></typeparam>
	/// <typeparam name="V"></typeparam>
	public class WeakReferenceDictionary<K, V>
	{
		private readonly Dictionary<K, WeakReference> _dict = new Dictionary<K, WeakReference>();
		private readonly List<K> _toRemoveList = new List<K>();
		public List<V> valueList = new List<V>();


		public ICollection<K> Keys => this._dict.Keys;

		public ICollection<WeakReference> ReferenceValues => this._dict.Values;

		public List<V> Values
		{
			get
			{
				valueList.Clear();
				foreach (var kv in this._dict)
				{
					K current = kv.Key;
					if (TryGetValue(current, out var v))
						valueList.Add(v);
				}
				return valueList;
			}
		}

		public V this[K key]
		{
			get => TryGetValue(key, out var v) ? v : default;
			set => this.Add(key, value);
		}


		public void Add(K key, V value)
		{
			if (!this._dict.ContainsKey(key))
			{
				this._dict.Add(key, new WeakReference(value));
				return;
			}

			this._dict[key] = new WeakReference(value);
		}

		public void Clear()
		{
			this._dict.Clear();
		}

		public bool ContainsKey(K key)
		{
			return this._dict.ContainsKey(key);
		}

		public bool Remove(K key)
		{
			return this._dict.Remove(key);
		}

		public bool TryGetValue(K key, out V value)
		{
			if (this._dict.ContainsKey(key))
			{
				var valueResult = this._dict[key].GetValueResult<V>();
				value = valueResult.GetValue();
				return valueResult.GetIsHasValue();
			}

			value = default;
			return false;
		}

		public void GC()
		{
			_toRemoveList.Clear();
			foreach (var kv in _dict)
			{
				var key = kv.Key;
				var value = kv.Value;
				if (!value.IsAlive)
					_toRemoveList.Add(key);
			}

			if (_toRemoveList.Count <= 0) return;
			for (var i = 0; i < _toRemoveList.Count; i++)
			{
				var e = _toRemoveList[i];
				_dict.Remove(e);
			}

			System.GC.Collect();
		}
	}
}