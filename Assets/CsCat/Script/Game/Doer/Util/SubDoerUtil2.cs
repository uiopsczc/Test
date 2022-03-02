using System;
using System.Collections;
using System.Collections.Generic;


namespace CsCat
{
	//里面的结构是  dict<id,List<Doer>>
	public class SubDoerUtil2
	{
		public static void DoReleaseSubDoer<T>(Doer parentDoer, string subDoerKey, Action<T> releaseSubDoerFunc = null)
			where T : Doer
		{
			//销毁
			var subDoers = GetSubDoers<T>(parentDoer, subDoerKey);
			for (int i = subDoers.Length - 1; i >= 0; i--)
			{
				var subDoer = subDoers[i];
				releaseSubDoerFunc?.Invoke(subDoer);
				subDoer.SetEnv(null);
				subDoer.Destruct();
			}

			GetSubDoerDict_ToEdit(parentDoer, subDoerKey).Clear();
		}

		/////////////////////////////////容器/////////////////////////////////
		public static T[] GetSubDoers<T>(Doer parentDoer, string subDoerKey, string id = null,
			Func<T, bool> filterFunc = null) where T : Doer
		{
			var dict = GetSubDoerDict_ToEdit(parentDoer, subDoerKey);
			List<T> result = new List<T>();
			if (id == null)
			{
				if (filterFunc == null)
				{
					foreach (DictionaryEntry keyValue in dict)
					{
						var subDoerList = keyValue.Value as ArrayList;
						for (var i = 0; i < subDoerList.Count; i++)
						{
							var subDoer = subDoerList[i];
							result.Add(subDoer as T);
						}
					}
				}
				else
				{
					foreach (DictionaryEntry keyValue in dict)
					{
						var subDoerList = keyValue.Value as ArrayList;
						for (var i = 0; i < subDoerList.Count; i++)
						{
							var subDoer = subDoerList[i];
							if (filterFunc(subDoer as T))
								result.Add(subDoer as T);
						}
					}
				}

				return result.ToArray();
			}

			var list = GetSubDoers_ToEdit(parentDoer, subDoerKey, id);
			for (var i = 0; i < list.Count; i++)
			{
				var subDoer = list[i];
				if (filterFunc == null || filterFunc(subDoer as T))
					result.Add(subDoer as T);
			}

			return result.ToArray();
		}

		public static Hashtable GetSubDoerDict_ToEdit(Doer parentDoer, string subDoerKey) //进行直接修改
		{
			var dict = parentDoer.GetOrAddTmp(subDoerKey, () => new Hashtable());
			return dict;
		}

		public static ArrayList GetSubDoers_ToEdit(Doer parentDoer, string subDoerKey, string id) //进行直接修改
		{
			var dict = GetSubDoerDict_ToEdit(parentDoer, subDoerKey);
			var list = dict.GetOrAddDefault2(id, () => new ArrayList());
			return list;
		}

		public static T GetSubDoer<T>(Doer parentDoer, string subDoerKey, string idOrRid) where T : Doer
		{
			bool isId = IdUtil.IsId(idOrRid);
			string id = isId ? idOrRid : IdUtil.RidToId(idOrRid);
			var dict = GetSubDoerDict_ToEdit(parentDoer, subDoerKey);
			if (dict.ContainsKey(id) && !(dict[id] as ArrayList).IsNullOrEmpty())
			{
				var arrayList = dict[id] as ArrayList;
				for (var i = 0; i < arrayList.Count; i++)
				{
					var subDoer = (T) arrayList[i];
					if (isId)
						return subDoer;
					if (subDoer.GetRid().Equals(idOrRid))
						return subDoer;
				}
			}

			return null;
		}

		//doer中sub_doer_key的子doers
		public static bool HasSubDoers<T>(Doer parentDoer, string subDoerKey, string id = null,
			Func<Doer, bool> filterFunc = null) where T : Doer
		{
			return !GetSubDoers(parentDoer, subDoerKey, id, filterFunc).IsNullOrEmpty();
		}

		//获取doer中的sub_doer_key的子doer数量  并不是sub_doer:GetCount()累加，而是sub_doers的个数
		public static int GetSubDoersCount<T>(Doer parentDoer, string subDoerKey, string id = null,
			Func<T, bool> filterFunc = null) where T : Doer
		{
			return GetSubDoers(parentDoer, subDoerKey, id, filterFunc).Length;
		}

		// 获取doer中的sub_doer_key的子doer数量  sub_doer:GetCount()累加
		public static int GetSubDoerCount<T>(Doer parentDoer, string subDoerKey, string id = null,
			Func<T, bool> filterFunc = null) where T : Doer
		{
			var subDoers = GetSubDoers(parentDoer, subDoerKey, id, filterFunc);
			int count = 0;
			for (var i = 0; i < subDoers.Length; i++)
			{
				var subDoer = subDoers[i];
				count = count + subDoer.GetCount();
			}

			return count;
		}

		public static string[] GetSubDoerIds(Doer parentDoer, string subDoerKey)
		{
			var dict = GetSubDoerDict_ToEdit(parentDoer, subDoerKey);
			List<string> result = new List<string>();
			foreach (DictionaryEntry keyVlaue in dict)
			{
				string id = (string)keyVlaue.Key;
				result.Add(id);
			}
			return result.ToArray();
		}

		public static void AddSubDoers(Doer parentDoer, string subDoerKey, Doer addSubDoer)
		{
			addSubDoer.SetOwner(parentDoer);
			string id = addSubDoer.GetId();
			bool canFold = addSubDoer.IsHasMethod("IsCanFold") && addSubDoer.InvokeMethod<bool>("IsCanFold");
			var subDoers = GetSubDoers_ToEdit(parentDoer, subDoerKey, id);
			if (canFold)
			{
				if (subDoers.IsNullOrEmpty())
					subDoers.Add(addSubDoer);
				else
				{
					(subDoers[0] as Doer).AddCount(addSubDoer.GetCount());
					addSubDoer.SetEnv(null);
					addSubDoer.Destruct();
				}
			}
			else
				subDoers.Add(addSubDoer);
		}

		public static T[] RemoveSubDoers<T>(Doer parentDoer, string subDoerKey, string id, int count,
			DoerFactory subDoerFactory) where T : Doer
		{
			var subDoers = GetSubDoers_ToEdit(parentDoer, subDoerKey, id);
			int currentCount = 0;
			List<T> result = new List<T>();
			if (subDoers.IsNullOrEmpty())
				return result.ToArray();
			if (count == Int32.MaxValue) //全部删除
			{
				for (int i = subDoers.Count - 1; i >= 0; i--)
				{
					var subDoer = subDoers[i] as T;
					subDoers.RemoveAt(i);
					subDoer.SetEnv(null);
					result.Add(subDoer);
				}

				result.Reverse();
				return result.ToArray();
			}

			bool canFold = (subDoers[0] as T).IsHasMethod("IsCanFold") &&
			               (subDoers[0] as T).InvokeMethod<bool>("IsCanFold");
			for (int i = subDoers.Count - 1; i >= 0; i--)
			{
				var subDoer = subDoers[i] as T;
				if (!canFold) //不可折叠的
				{
					subDoers.RemoveAt(i);
					subDoer.SetEnv(null);
					currentCount = currentCount + 1;
					result.Add(subDoer);
					if (currentCount == count)
						return result.ToArray();
				}
				else //可折叠的
				{
					int subDoerCount = subDoer.GetCount();
					if (subDoerCount > count) //有多
					{
						subDoer.AddCount(-count);
						T cloneSubDoer = subDoerFactory.NewDoer(subDoer.GetId()) as T;
						cloneSubDoer.SetCount(count);
						result.Add(cloneSubDoer);
					}
					else //不够或者相等
					{
						subDoers.RemoveAt(i);
						subDoer.SetEnv(null);
						result.Add(subDoer);
					}

					return result.ToArray();
				}
			}

			return result.ToArray();
		}

		public static bool CanRemoveSubDoers(Doer parentDoer, string subDoerKey, string id, int count)
		{
			int currentCount = GetSubDoerCount<Doer>(parentDoer, subDoerKey, id, null);
			return currentCount >= count;
		}

		public static T RemoveSubDoer<T>(Doer parentDoer, string subDoerKey, T subDoer) where T : Doer
		{
			var id = subDoer.GetId();
			var subDoers = GetSubDoers_ToEdit(parentDoer, subDoerKey, id);
			for (int i = subDoers.Count - 1; i >= 0; i--)
			{
				var curSubDoer = subDoers[i] as T;
				if (curSubDoer == subDoer)
				{
					curSubDoer.SetEnv(null);
					subDoers.RemoveAt(i);
					return curSubDoer;
				}
			}

			return null;
		}

		public static T RemoveSubDoer<T>(Doer parentDoer, string subDoerKey, string rid) where T : Doer
		{
			var id = IdUtil.RidToId(rid);
			var subDoers = GetSubDoers_ToEdit(parentDoer, subDoerKey, id);
			for (int i = subDoers.Count - 1; i >= 0; i--)
			{
				var curSubDoer = subDoers[i] as T;
				if (curSubDoer.GetRid().Equals(rid))
				{
					curSubDoer.SetEnv(null);
					subDoers.RemoveAt(i);
					return curSubDoer;
				}
			}

			return null;
		}

		public static void ClearSubDoers<T>(Doer parentDoer, string subDoerKey, Action<T> clearSubDoerFunc = null)
			where T : Doer
		{
			var dict = GetSubDoerDict_ToEdit(parentDoer, subDoerKey);
			foreach (DictionaryEntry keyValue in dict)
			{
				ArrayList subDoerList = (ArrayList) keyValue.Value;
				for (int i = subDoerList.Count - 1; i >= 0; i--)
				{
					var subDoer = subDoerList[i] as T;
					clearSubDoerFunc?.Invoke(subDoer);
					subDoer.SetEnv(null);
					subDoer.Destruct();
				}
			}

			dict.Clear();
		}
	}
}