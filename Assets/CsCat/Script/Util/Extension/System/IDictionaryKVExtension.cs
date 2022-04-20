using System;
using System.Collections.Generic;

namespace CsCat
{
	public static class IDictionaryKVExtension
	{
		/// <summary>
		///例子
		///Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
		///dict.GetOrAddNew<List<string>>("kk").Add("chenzhongmou");
		///采用延迟调用
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <param name="key"></param>
		/// <param name="dv"></param>
		/// <returns></returns>
		public static V GetOrAddDefault<K, V>(this IDictionary<K, V> self, K key, Func<V> defaultValueFunc = null)
		{
			if (self.TryGetValue(key, out var result))
				return result;
			self[key] = defaultValueFunc == null ? default : defaultValueFunc();
			return self[key];
		}

		/// <summary>
		/// 没有的时候返回dv，不会设置值
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <param name="key"></param>
		/// <param name="dv"></param>
		/// <returns></returns>
		public static V GetOrGetDefault<K, V>(this IDictionary<K, V> self, K key, Func<V> defaultValueFunc = null)
		{
			if (self != null && self.TryGetValue(key, out var result))
				return result;
			return defaultValueFunc == null ? default : defaultValueFunc();
		}


		public static void RemoveByFunc<K, V>(this IDictionary<K, V> self, Func<K, V, bool> func)
		{
			List<K> toRemoveKeyList = new List<K>();
			foreach (var keyValue in self)
			{
				var key = keyValue.Key;
				var value = keyValue.Value;
				if (func(key, value))
					toRemoveKeyList.Add(key);
			}
			for (var i = 0; i < toRemoveKeyList.Count; i++)
			{
				var toRemoveKey = toRemoveKeyList[i];
				self.Remove(toRemoveKey);
			}
		}

		public static void RemoveByValue<K, V>(this IDictionary<K, V> self, V value, bool isAll = false)
		{
			bool isHasRemoveKey = false;
			if (isAll == false)
			{
				K toRemoveKey = default;
				foreach (var keyValue in self)
				{
					if (!ObjectUtil.Equals(keyValue.Value, value)) continue;
					isHasRemoveKey = true;
					toRemoveKey = keyValue.Key;
					break;
				}
				if (isHasRemoveKey)
					self.Remove(toRemoveKey);
				return;
			}
			List<K> toRemoveKeyList = new List<K>();
			foreach (var keyValue in self)
			{
				if (ObjectUtil.Equals(keyValue.Value, value))
					toRemoveKeyList.Add(keyValue.Key);
			}
			for (var i = 0; i < toRemoveKeyList.Count; i++)
			{
				var toRemoveKey = toRemoveKeyList[i];
				self.Remove(toRemoveKey);
			}
		}

		public static void RemoveAllAndClear<K, V>(this IDictionary<K, V> self, Action<K, V> onRemoveAction)
		{
			foreach (var keyValue in self)
				onRemoveAction(keyValue.Key, keyValue.Value);
			self.Clear();
		}


		public static V Remove2<K, V>(this IDictionary<K, V> self, K key)
		{
			if (self.TryGetValue(key, out var result))
			{
				self.Remove(key);
				return result;
			}
			return default;
		}

		public static void Combine<K, V>(this IDictionary<K, V> self, IDictionary<K, V> another)
		{
			foreach (var anotherKeyValue in another)
			{
				var anotherKey = anotherKeyValue.Key;
				if (!self.ContainsKey(anotherKey))
					self[anotherKey] = another[anotherKey];
			}
		}


		public static List<T> RandomList<T>(this IDictionary<T, float> self, int outCount, bool isUnique,
			RandomManager randomManager = null)
		{
			randomManager = randomManager ?? Client.instance.randomManager;
			return randomManager.RandomList(self, outCount, isUnique);
		}

		public static T Random<T>(this IDictionary<T, float> self, RandomManager randomManager = null)
		{
			return self.RandomList(1, false, randomManager)[0];
		}

		public static K FindKey<K, V>(this IDictionary<K, V> self, K key)
		{
			foreach (var keyValue in self)
			{
				if (keyValue.Key.Equals(key))
					return keyValue.Key;
			}
			return default;
		}
	}
}