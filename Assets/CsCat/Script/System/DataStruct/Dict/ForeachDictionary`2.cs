using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class ForeachDictionary<K, V>
	{
		private Dictionary<K, V> dict;
		private List<K> tmpToCheckKeyList = new List<K>();
		private Dictionary<K, bool> tmpCheckedKeyDict = new Dictionary<K, bool>();
		public ForeachDictionary(Dictionary<K, V> dict)
		{
			this.dict = dict;
		}

		//to_check_key_list 有些特殊的要求可能用到，如外部需要按照to_check_key_list的顺序来检测
		public IEnumerable<V> ForeachValues(List<K> toCheckKeyList = null)
		{
			this.tmpToCheckKeyList.Clear();
			this.tmpCheckedKeyDict.Clear();
			if (toCheckKeyList == null)
				this.tmpToCheckKeyList.AddRange(dict.Keys);
			else
				this.tmpToCheckKeyList.AddRange(toCheckKeyList);
			while (true)
			{
				while (tmpToCheckKeyList.Count > 0)
				{
					var tmpToCheckKey = tmpToCheckKeyList.RemoveFirst();
					if (!dict.ContainsKey(tmpToCheckKey))//中途yield return value;可能有删除其他的节点，所以可能会出现null,所以要检测是否需要忽略
						continue;
					yield return dict[tmpToCheckKey];
					tmpCheckedKeyDict[tmpToCheckKey] = true;
				}

				//再次检测，是否有新生成的，如果有则加入tmp_to_check_key_list，然后再次调用上面的步骤，否则全部checked，跳出所有循环
				bool isCanBreak = true;
				var keys = dict.Keys;
				foreach (var key in keys)
				{
					if (tmpCheckedKeyDict.ContainsKey(key))
						continue;
					tmpToCheckKeyList.Add(key);
					isCanBreak = false;
				}
				if (isCanBreak)
					break;
			}
		}
	}
}