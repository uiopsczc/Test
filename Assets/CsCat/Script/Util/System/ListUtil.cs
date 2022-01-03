using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public static class ListUtil
	{
		/// <summary>
		///   将list1[a]和list2[b]交换
		/// </summary>
		public static void Swap<T>(List<T> list1, int index1, List<T> list2, int index2)
		{
			var c = list1[index1];
			list1[index1] = list2[index2];
			list2[index2] = c;
		}

		//////////////////////////////////////////////////////////////////////
		// Diff相关
		//////////////////////////////////////////////////////////////////////
		// 必须和ApplyDiff使用
		// 以new为基准，获取new相对于old不一样的部分
		// local diff = table.GetDiff(old, new)
		//  table.ApplyDiff(old, diff)
		// 这样old的就变成和new一模一样的数据
		public static LinkedHashtable GetDiff(IList oldList, IList newList)
		{
			var diff = new LinkedHashtable();
			for (int i = newList.Count - 1; i >= 0; i--)
			{
				var newK = i;
				var newV = newList[i];
				switch (newV)
				{
					case IList _:
						{
							var newVList = newV as IList;
							if (newVList.Count == 0 && (!oldList.ContainsIndex(newK) ||
														oldList[newK].GetType() != newV.GetType() ||
														(oldList[newK] is IList && (oldList[newK] as IList).Count != 0)))
								diff[newK] = StringConst.String_New_In_Table + newV.GetType();
							else if (oldList.ContainsIndex(newK) && oldList[newK] is IList)
								diff[newK] = GetDiff(oldList[newK] as IList, newVList);
							else if (!oldList.ContainsIndex(newK) || !newV.Equals(oldList[newK]))
								diff[newK] = CloneUtil.CloneDeep(newV);
							break;
						}
					case IDictionary _ when oldList.ContainsIndex(newK) && oldList[newK] is IDictionary:
						diff[newK] = IDictionaryUtil.GetDiff(oldList[newK] as IDictionary, newV as IDictionary);
						break;
					default:
						{
							if (!oldList.ContainsIndex(newK) || !newV.Equals(oldList[newK]))
								diff[newK] = newV;
							break;
						}
				}
			}

			for (int i = 0; i < oldList.Count; i++)
			{
				if (!newList.ContainsIndex(i))
					diff[i] = StringConst.String_Nil_In_Table;
			}

			diff.Sort((a, b) => a.To<int>() >= b.To<int>());
			if (diff.Count == 0)
				diff = null;
			return diff;
		}

		// table.ApplyDiff(old, diff)
		// 将diff中的东西应用到old中
		// 重要：当为Array的时候，需要重新赋值；List的时候，可以不需要重新赋值
		public static IList ApplyDiff(IList oldList, LinkedHashtable diffDict)
		{
			if (diffDict == null)
				return oldList;

			int oldListCount = oldList.Count;
			foreach (var tmpK in diffDict.Keys)
			{
				var k = tmpK.To<int>();
				var v = diffDict[tmpK];
				if (v.Equals(StringConst.String_Nil_In_Table))
				{
					if (oldList is Array)
						oldList = (oldList as Array).RemoveAt_Array(k);
					else
						oldList.RemoveAt(k);
				}
				else if (v.ToString().StartsWith(StringConst.String_New_In_Table))
				{
					string typeString = v.ToString().Substring(StringConst.String_New_In_Table.Length);
					Type type = TypeUtil.GetType(typeString);
					var value = type.CreateInstance<object>();
					if (k < oldListCount)
						oldList[k] = value;
					else
					{
						if (oldList is Array)
							oldList = (oldList as Array).Insert_Array(oldListCount, v);
						else
							oldList.Insert(oldListCount, v);
					}
				}
				else if (oldList.ContainsIndex(k) && oldList[k] is IList && v is LinkedHashtable hashtable)
					ApplyDiff(oldList[k] as IList, hashtable);
				else if (oldList.ContainsIndex(k) && oldList[k] is IDictionary && v is LinkedHashtable linkedHashtable)
					IDictionaryUtil.ApplyDiff(oldList[k] as IDictionary, linkedHashtable);
				else
				{
					if (k < oldListCount)
						oldList[k] = v;
					else
					{
						if (oldList is Array)
							oldList = (oldList as Array).Insert_Array(oldListCount, v);
						else
							oldList.Insert(oldListCount, v);
					}
				}
			}

			return oldList;
		}

		// 必须和ApplyDiff使用
		// 以new为基准，获取new中有，但old中没有的
		// local diff = table.GetNotExist(old, new)
		// table.ApplyDiff(old, diff)
		// 这样old就有new中的字段
		public static LinkedHashtable GetNotExist(IList oldList, IList newList)
		{
			var diff = new LinkedHashtable();
			for (int i = newList.Count - 1; i >= 0; i--)
			{
				var newK = i;
				var newV = newList[i];

				if (!oldList.ContainsIndex(i))
					diff[newK] = newK;
				else
				{
					switch (oldList[newK])
					{
						case IList _ when newV is IList list:
							diff[newK] = GetDiff(oldList[newK] as IList, list);
							break;
						case IDictionary _ when newV is IDictionary dictionary:
							diff[newK] = IDictionaryUtil.GetNotExist(oldList[newK] as IDictionary, dictionary);
							break;
					}

					//其他情况不用处理
				}
			}

			diff.Sort((a, b) => a.To<int>() >= b.To<int>());
			return diff;
		}

		//两个table是否不一样
		public static bool IsDiff(IList oldList, IList newList)
		{
			if (oldList.Count != newList.Count)
				return true;
			for (int newKey = 0; newKey < newList.Count; newKey++)
			{
				var newValue = newList[newKey];
				switch (newValue)
				{
					case IList list when !(oldList[newKey] is IList):
						return true;
					case IList list when IsDiff(oldList[newKey] as IList, list):
						return true;
					case IList list:
						break;
					case IDictionary _ when !(oldList[newKey] is IDictionary):
					case IDictionary _
						when IDictionaryUtil.IsDiff(oldList[newKey] as IDictionary, newValue as IDictionary):
						return true;
					case IDictionary _:
						break;
					default:
						{
							if (!newValue.Equals(oldList[newKey]))
								return true;
							break;
						}
				}
			}

			return false;
		}
	}
}