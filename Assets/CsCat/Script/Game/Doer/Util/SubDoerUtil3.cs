using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	//里面的结构是  Dict<rid,Doer>
	public class SubDoerUtil3
	{
		public static void DoReleaseSubDoer<T>(Doer parentDoer, string subDoerKey, Action<T> relaseSubDoerFunc = null)
		  where T : Doer
		{
			//销毁
			var subDoers = GetSubDoers<T>(parentDoer, subDoerKey, null, null);
			for (int i = subDoers.Length - 1; i >= 0; i--)
			{
				var subDoer = subDoers[i];
				relaseSubDoerFunc?.Invoke(subDoer);
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
				foreach (var subDoer in dict.Values)
				{
					if (filterFunc == null || filterFunc(subDoer as T))
						result.Add(subDoer as T);
				}

				return result.ToArray();
			}

			foreach (T subDoer in dict.Values)
			{
				if (subDoer.GetId().Equals(id))
				{
					if (filterFunc == null || filterFunc(subDoer))
						result.Add(subDoer);
				}
			}

			return result.ToArray();
		}

		public static Hashtable GetSubDoerDict_ToEdit(Doer parentDoer, string subDoerKey) //可以直接插入删除
		{
			return parentDoer.GetOrAddTmp(subDoerKey, () => new Hashtable());
		}


		public static bool HasSubDoers<T>(Doer parentDoer, string subDoerKey, string id = null,
		  Func<T, bool> filterFunc = null) where T : Doer
		{
			return !GetSubDoers<T>(parentDoer, subDoerKey, id, filterFunc).IsNullOrEmpty();
		}

		//获取doer中的sub_doer_key的子doer数量
		public static int GetSubDoersCount<T>(Doer parentDoer, string subDoerKey, string id = null,
		  Func<T, bool> filterFunc = null) where T : Doer
		{
			return GetSubDoers<T>(parentDoer, subDoerKey, id, filterFunc).Length;
		}

		public static T GetSubDoer<T>(Doer parentDoer, string subDoerKey, string idOrRid) where T : Doer
		{
			if (IdUtil.IsId(idOrRid)) //id的情况
			{
				string id = idOrRid;
				var ts = GetSubDoers<T>(parentDoer, subDoerKey, null, null);
				for (var i = 0; i < ts.Length; i++)
				{
					var subDoer = ts[i];
					if (subDoer.GetId().Equals(id))
						return subDoer;
				}

				return null;
			}
			//rid的情况
			string rid = idOrRid;
			if (GetSubDoerDict_ToEdit(parentDoer, subDoerKey).ContainsKey(rid))
				return GetSubDoerDict_ToEdit(parentDoer, subDoerKey)[rid] as T;
			return null;
		}

		public static void ClearSubDoers<T>(Doer parentDoer, string subDoerKey, Action<T> clearSubDoerFunc = null)
		  where T : Doer
		{
			var list = GetSubDoers<T>(parentDoer, subDoerKey, null, null);
			for (int i = list.Length - 1; i >= 0; i--)
			{
				var subDoer = list[i];
				clearSubDoerFunc?.Invoke(subDoer);
				subDoer.SetEnv(null);
				subDoer.Destruct();
			}

			GetSubDoerDict_ToEdit(parentDoer, subDoerKey).Clear();
		}

	}
}