namespace CsCat
{
	public class KeyValuePairCat<K, V> : IToString2, IDespawn
	{
		public K key { set; get; }
		public V value { set; get; }

		public KeyValuePairCat()
		{
		}

		public KeyValuePairCat(K key, V value)
		{
			this.Init(key, value);
		}

		public KeyValuePairCat<K, V> Init(K key, V value)
		{
			this.key = key;
			this.value = value;
			return this;
		}


		public override bool Equals(object obj)
		{
			if (!(obj is KeyValuePairCat<K, V> other))
				return false;
			return ObjectUtil.Equals(key, other.key) && ObjectUtil.Equals(value, other.value);
		}

		public override int GetHashCode()
		{
			return ObjectUtil.GetHashCode(key, value);
		}

		public string ToString2(bool isFillStringWithDoubleQuote = false)
		{
			return string.Format("[{0},{1}]", key, value);
		}

		public void OnDespawn()
		{
			this.key = default;
			this.value = default;
		}
	}
}