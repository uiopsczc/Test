using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	//里面的结构是  List<Doer>
	public class SubDoerUtil1
	{
		public static void DoReleaseSubDoer<T>(Doer parentDoer, string subDoerKey, Action<T> releaseSubDoerFunc = null)
		  where T : Doer
		{
			//销毁
			var subDoers = GetSubDoers<T>(parentDoer, subDoerKey, null, null);
			for (int i = subDoers.Length - 1; i >= 0; i--)
			{
				var subDoer = subDoers[i];
				releaseSubDoerFunc?.Invoke(subDoer);
				subDoer.SetEnv(null);
				subDoer.Destruct();
			}

			GetSubDoers_ToEdit(parentDoer, subDoerKey).Clear();
		}

		/////////////////////////////////容器/////////////////////////////////
		public static T[] GetSubDoers<T>(Doer parentDoer, string subDoerKey, string id = null,
		  Func<T, bool> filterFunc = null) where T : Doer
		{
			var list = GetSubDoers_ToEdit(parentDoer, subDoerKey);
			List<T> result = new List<T>();
			if (id == null)
			{
				for (var i = 0; i < list.Count; i++)
				{
					var subDoer = list[i];
					if (filterFunc == null || filterFunc(subDoer as T))
						result.Add(subDoer as T);
				}

				return result.ToArray();
			}

			for (var i = 0; i < list.Count; i++)
			{
				var subDoer = (T) list[i];
				if (subDoer.GetId().Equals(id))
				{
					if (filterFunc == null || filterFunc(subDoer))
						result.Add(subDoer);
				}
			}

			return result.ToArray();
		}

		public static ArrayList GetSubDoers_ToEdit(Doer parentDoer, string subDoerKey) //可以直接插入删除
		{
			return parentDoer.GetOrAddTmp(subDoerKey, () => new ArrayList());
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
			var ts = GetSubDoers<T>(parentDoer, subDoerKey, null, null);
			for (var i = 0; i < ts.Length; i++)
			{
				var subDoer = ts[i];
				if (IdUtil.IsIdOrRidEquals(idOrRid, subDoer.GetId(), subDoer.GetRid()))
					return subDoer;
			}

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

			GetSubDoers_ToEdit(parentDoer, subDoerKey).Clear();
		}

	}
}