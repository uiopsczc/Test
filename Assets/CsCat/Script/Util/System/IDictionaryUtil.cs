using System;
using System.Collections;

namespace CsCat
{
	public static class IDictionaryUtil
	{
		//////////////////////////////////////////////////////////////////////
		// Diff相关
		//////////////////////////////////////////////////////////////////////
		// 必须和ApplyDiff使用
		// 以new为基准，获取new相对于old不一样的部分
		// local diff = table.GetDiff(old, new)
		//  table.ApplyDiff(old, diff)
		// 这样old的就变成和new一模一样的数据
		public static LinkedHashtable GetDiff(IDictionary oldDict, IDictionary newDict)
		{
			var diff = new LinkedHashtable();
			foreach (DictionaryEntry dictionaryEntry in newDict)
			{
				var newKey = dictionaryEntry.Key;
				var newValue = dictionaryEntry.Value;
				bool isOldDictContainsNewKey = oldDict.Contains(newKey);
				var oldValue = isOldDictContainsNewKey ? oldDict[newKey] : null;
				if (newValue is IDictionary newValueDict)
				{
					int newValueDictCount = newValueDict.Count;
					if (newValueDictCount == 0)
					{
						if ((!isOldDictContainsNewKey) || oldValue.GetType() != newValueDict.GetType() ||
						    (oldValue is IDictionary oldValueDict &&
						     (oldValueDict.Count != 0)))
						{
							diff[newKey] = StringConst.String_New_In_Table + newValueDict.GetType();
							continue;
						}
					}

					if (isOldDictContainsNewKey && oldValue is IDictionary oldValueDict2)
					{
						diff[newKey] = GetDiff(oldValueDict2, newValueDict);
						continue;
					}

					if (!isOldDictContainsNewKey || !newValueDict.Equals(oldValue))
					{
						diff[newKey] = CloneUtil.CloneDeep(newValue);
						continue;
					}
				}

				if (newValue is IList list && isOldDictContainsNewKey && oldValue is IList oldValueList)
				{
					diff[newKey] = ListUtil.GetDiff(oldValueList, list);
					continue;
				}

				if (!isOldDictContainsNewKey || !newValue.Equals(oldValue))
				{
					diff[newKey] = newValue;
					continue;
				}
			}

			foreach (DictionaryEntry oldDictionaryEntry in oldDict)
			{
				var key = oldDictionaryEntry.Key;
				if (!newDict.Contains(key))
					diff[key] = StringConst.String_Nil_In_Table;
			}

			if (diff.Count == 0)
				diff = null;
			return diff;
		}

		// table.ApplyDiff(old, diff)
		// 将diff中的东西应用到old中
		public static void ApplyDiff(IDictionary oldDict, LinkedHashtable diffDict)
		{
			if (diffDict == null)
			{
				return;
			}

			foreach (DictionaryEntry dictionaryEntry in diffDict)
			{
				var key = dictionaryEntry.Key;
				var value = dictionaryEntry.Value;
				if (StringConst.String_Nil_In_Table.Equals(value))
				{
					oldDict.Remove(key);
					continue;
				}

				var valueString = value.ToString();
				if (valueString.StartsWith(StringConst.String_New_In_Table))
				{
					string typeString = valueString.Substring(StringConst.String_New_In_Table.Length);
					Type type = TypeUtil.GetType(typeString);
					oldDict[key] = type.CreateInstance<object>();
					continue;
				}

				if (oldDict.Contains(key))
				{
					var oldValue = oldDict[key];
					if (oldValue is IDictionary oldValueDict && value is LinkedHashtable hashtable)
					{
						ApplyDiff(oldValueDict, hashtable);
						continue;
					}

					if (oldValue is IList oldValueList && value is LinkedHashtable linkedHashtable)
					{
						oldDict[key] = ListUtil.ApplyDiff(oldValueList, linkedHashtable);
						continue;
					}
				}

				oldDict[key] = value;
			}
		}

		// 必须和ApplyDiff使用
		// 以new为基准，获取new中有，但old中没有的
		// local diff = table.GetNotExist(old, new)
		// table.ApplyDiff(old, diff)
		// 这样old就有new中的字段
		public static LinkedHashtable GetNotExist(IDictionary oldDict, IDictionary newDict)
		{
			var diff = new LinkedHashtable();
			foreach (DictionaryEntry dictionaryEntry in newDict)
			{
				var newK = dictionaryEntry.Key;
				var newV = dictionaryEntry.Value;
				if (!oldDict.Contains(newK))
					diff[newK] = newV;
				else
				{
					var oldValue = oldDict[newK];
					if (newV is IDictionary dictionary && oldValue is IDictionary dictionary1)
						diff[newK] = GetNotExist(dictionary1, dictionary);
					else if (newV is IList list && oldValue is IList list1)
						diff[newK] = ListUtil.GetNotExist(list1, list);
					//其他情况不用处理
				}
			}

			return diff;
		}

		//两个table是否不一样
		public static bool IsDiff(IDictionary oldDict, IDictionary newDict)
		{
			foreach (DictionaryEntry dictionaryEntry in oldDict)
			{
				var key = dictionaryEntry.Key;
				if (!newDict.Contains(key))
					return true;
			}

			foreach (DictionaryEntry dictionaryEntry in newDict)
			{
				var newKey = dictionaryEntry.Key;
				var newValue = dictionaryEntry.Value;
				if (!oldDict.Contains(newKey))
					return false;
				var oldValue = oldDict[newKey];
				switch (newValue)
				{
					case IDictionary _ when !(oldValue is IDictionary):
						return false;
					case IDictionary dictionary when IsDiff((IDictionary) oldValue, dictionary):
						return true;
					case IDictionary _:
						break;
					case IList _ when !(oldValue is IList):
						return false;
					case IList list when ListUtil.IsDiff(oldValue as IList, list):
						return true;
					case IList _:
						break;
					default:
					{
						if (!newValue.Equals(oldDict[newKey]))
							return true;
						break;
					}
				}
			}

			return false;
		}
	}
}