using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public static class IDictionaryExtension
	{
		public static T Get<T>(this IDictionary self, object key)
		{
			return self.Contains(key) ? self[key].To<T>() : default;
		}

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
		public static V GetOrAddDefault2<V>(this IDictionary self, object key, Func<V> defaultValueFunc = null)
		{
			if (self.Contains(key)) return (V)self[key];
			var result = defaultValueFunc == null ? default : defaultValueFunc();
			self[key] = result;
			return result;
		}

		public static V Remove3<V>(this IDictionary self, object key)
		{
			if (!self.Contains(key))
				return default;

			V result = (V)self[key];
			self.Remove(key);
			return result;
		}

		/// <summary>
		/// 没有的时候返回dv，不会设置值
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <param name="key"></param>
		/// <param name="dv"></param>
		/// <returns></returns>
		public static V GetOrGetDefault2<V>(this IDictionary self, object key, Func<V> defaultValueFunc = null)
		{
			return self == null || !self.Contains(key)
				? defaultValueFunc == null ? default : defaultValueFunc()
				: (V)self[key];
		}

		//删除值为null值、0数值、false逻辑值、空字符串、空集合等数据项
		public static void Trim(this IDictionary self)
		{
			List<object> toRemoveKeyList = new List<object>();
			foreach (DictionaryEntry dictionaryEntry in self)
			{
				var key = dictionaryEntry.Key;
				var value = dictionaryEntry.Value;
				switch (value)
				{
					//删除值为null的数值
					case null:
						toRemoveKeyList.Add(key);
						break;
					default:
					{
						if (value.IsNumber() && value.To<double>() == 0) //删除值为0的数值
							toRemoveKeyList.Add(key);
						else if (value.IsBool() && (bool)value == false) //删除值为false的逻辑值
							toRemoveKeyList.Add(key);
						else if (value.IsString() && ((string)value).IsNullOrWhiteSpace()) //删除值为空的字符串
							toRemoveKeyList.Add(key);
						else if (value is ICollection collection && collection.Count == 0) //删除为null的collection
							toRemoveKeyList.Add(key);
						else if (value is IDictionary dictionary)
							Trim(dictionary);
						break;
					}
				}
			}

			for (var i = 0; i < toRemoveKeyList.Count; i++)
			{
				var toRemoveKey = toRemoveKeyList[i];
				self.Remove(toRemoveKey);
			}
		}

		//删除值为null值、0数值、false逻辑值、空字符串、空集合等数据项
		public static Hashtable ToHashtable(this IDictionary self)
		{
			Hashtable result = new Hashtable();
			foreach (DictionaryEntry dictionaryEntry in self)
			{
				var key = dictionaryEntry;
				result[key] = self[key];
			}
			return result;
		}

		public static void Combine(this IDictionary self, IDictionary another)
		{
			foreach (DictionaryEntry anotherDictionaryEntry in another)
			{
				var anotherKey = anotherDictionaryEntry.Key;
				if (!self.Contains(anotherKey))
					self[anotherKey] = another[anotherKey];
			}
		}

		public static void RemoveByFunc(this IDictionary self, Func<object, object, bool> func)
		{
			List<object> toRemoveKeyList = new List<object>();
			foreach (DictionaryEntry dictionaryEntry in self)
			{
				var key = dictionaryEntry.Key;
				var value = dictionaryEntry.Value;
				if (func(key, value))
					toRemoveKeyList.Add(key);
			}

			for (var i = 0; i < toRemoveKeyList.Count; i++)
			{
				var toRemoveKey = toRemoveKeyList[i];
				self.Remove(toRemoveKey);
			}
		}

		//////////////////////////////////////////////////////////////////////
		// Diff相关
		//////////////////////////////////////////////////////////////////////
		// 必须和ApplyDiff使用
		// 以new为基准，获取new相对于old不一样的部分
		// local diff = table.GetDiff(old, new)
		//  table.ApplyDiff(old, diff)
		// 这样old的就变成和new一模一样的数据
		public static LinkedHashtable GetDiff(this IDictionary oldDict, IDictionary newDict)
		{
			return IDictionaryUtil.GetDiff(oldDict, newDict);
		}

		// table.ApplyDiff(old, diff)
		// 将diff中的东西应用到old中
		public static void ApplyDiff(this IDictionary oldDict, LinkedHashtable diffDict)
		{
			IDictionaryUtil.ApplyDiff(oldDict, diffDict);
		}

		// 必须和ApplyDiff使用
		// 以new为基准，获取new中有，但old中没有的
		// local diff = table.GetNotExist(old, new)
		// table.ApplyDiff(old, diff)
		// 这样old就有new中的字段
		public static LinkedHashtable GetNotExist(this IDictionary oldDict, IDictionary newDict)
		{
			return IDictionaryUtil.GetNotExist(oldDict, newDict);
		}

		//两个table是否不一样
		public static bool IsDiff(this IDictionary oldDict, IDictionary newDict)
		{
			return IDictionaryUtil.IsDiff(oldDict, newDict);
		}
	}
}