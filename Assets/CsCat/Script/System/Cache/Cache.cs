using System;
using System.Collections.Generic;

namespace CsCat
{
	/// <summary>
	/// 缓存
	/// </summary>
	public class Cache : IDeSpawn
	{
		#region field

		public Dictionary<object, object> dict = new Dictionary<object, object>();

		#endregion

		public object this[object key]
		{
			get => dict[key];
			set => dict[key] = value;
		}

		public void Remove(object key)
		{
			this.dict.Remove(key);
		}

		public object Get(object key)
		{
			return this.dict[key];
		}

		public T Get<T>(object key)
		{
			return (T)this.dict[key];
		}

		public bool ContainsKey(object key)
		{
			return this.dict.ContainsKey(key);
		}

		public bool ContainsKey<T>()
		{
			return this.dict.ContainsKey(typeof(T).ToString());
		}

		public bool ContainsValue(object value)
		{
			return this.dict.ContainsValue(value);
		}


		public T GetOrAddDefault<T>(object key, Func<T> dvFunc = null)
		{
			return dict.GetOrAddDefault2<T>(key, dvFunc);
		}

		public T GetOrAddDefault<T>(Func<T> dvFunc = null)
		{
			return dict.GetOrAddDefault2<T>(typeof(T).ToString(), dvFunc);
		}


		public T GetOrGetDefault<T>(object key, Func<T> dvFunc = null)
		{
			return dict.GetOrGetDefault2<T>(key, dvFunc);
		}

		public object Remove2(object key)
		{
			return Remove2<object>(key);
		}

		public T Remove2<T>(object key)
		{
			return dict.Remove3<T>(key);
		}

		public void Clear()
		{
			dict.Clear();
		}

		public void OnDeSpawn()
		{
			Clear();
		}
	}
}