using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class ForeachDictionary<K, V>
	{
		private Dictionary<K, V> dict;
		private List<K> tmp_to_check_key_list = new List<K>();
		private Dictionary<K, bool> tmp_checked_key_dict = new Dictionary<K, bool>();
		public ForeachDictionary(Dictionary<K, V> dict)
		{
			this.dict = dict;
		}

		//to_check_key_list 有些特殊的要求可能用到，如外部需要按照to_check_key_list的顺序来检测
		public IEnumerable<V> ForeachValues(List<K> to_check_key_list = null)
		{
			this.tmp_to_check_key_list.Clear();
			this.tmp_checked_key_dict.Clear();
			if (to_check_key_list == null)
				this.tmp_to_check_key_list.AddRange(dict.Keys);
			else
				this.tmp_to_check_key_list.AddRange(to_check_key_list);
			while (true)
			{
				while (tmp_to_check_key_list.Count > 0)
				{
					var tmp_to_check_key = tmp_to_check_key_list.RemoveFirst();
					if (!dict.ContainsKey(tmp_to_check_key))//中途yield return value;可能有删除其他的节点，所以可能会出现null,所以要检测是否需要忽略
						continue;
					yield return dict[tmp_to_check_key];
					tmp_checked_key_dict[tmp_to_check_key] = true;
				}

				//再次检测，是否有新生成的，如果有则加入tmp_to_check_key_list，然后再次调用上面的步骤，否则全部checked，跳出所有循环
				bool is_can_break = true;
				var keys = dict.Keys;
				foreach (var key in keys)
				{
					if (tmp_checked_key_dict.ContainsKey(key))
						continue;
					tmp_to_check_key_list.Add(key);
					is_can_break = false;
				}
				if (is_can_break)
					break;
			}
		}
	}
}