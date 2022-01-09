
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class CacheMonoBehaviour : MonoBehaviour
	{
		public Dictionary<string, object> dict = new Dictionary<string, object>();

		public void Set(object obj, string key = null)
		{
			if (key == null)
				key = obj.GetType().FullName;
			dict[key] = obj;
		}

		public void Set(object obj, string key, string subKey)
		{
			if (key == null)
				key = obj.GetType().FullName;
			Dictionary<string, object> subDict = dict.GetOrAddDefault2(key, () => new Dictionary<string, object>());
			subDict[subKey] = obj;
		}

		public T Get<T>(string key = null)
		{
			if (key == null)
				key = typeof(T).FullName;
			return (T)dict[key];
		}

		public T Get<T>(string key, string subKey)
		{
			Dictionary<string, object> subDict = Get<Dictionary<string, object>>(key);
			return (T)subDict[key];
		}

		public T GetOrAdd<T>(string key, Func<T> defaultFunc)
		{
			if (key == null)
				key = typeof(T).FullName;
			if (!dict.ContainsKey(key))
				dict[key] = defaultFunc();
			return (T)dict[key];
		}

		public T GetOrAdd<T>(string key, string sub_key, Func<T> defaultFunc)
		{
			Dictionary<string, object> subDict = GetOrAdd(key, () => new Dictionary<string, object>());
			return subDict.GetOrAddDefault2(sub_key, defaultFunc);
		}

	}
}