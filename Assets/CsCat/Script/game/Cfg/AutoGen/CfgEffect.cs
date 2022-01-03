//AutoGen. DO NOT EDIT!!!
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat
{
	public class CfgEffect
	{
		protected CfgEffect() { }
		public static CfgEffect Instance => instance;
		protected static CfgEffect instance = new CfgEffect();
		protected CfgEffectRoot root;
		public void Parse(string jsonStr) { this.root = JsonMapper.ToObject<CfgEffectRoot>(jsonStr); }
		public List<CfgEffectData> All() { return this.root.data_list; }
		public CfgEffectData Get(int index) { return this.root.data_list[index]; }
		public CfgEffectData get_by_id(string id)
		{
			string key = id.ToString();
			return this.Get(this.root.index_dict.unique.id[key]);
		}
		public bool contain_key_by_id(string id)
		{
			string key = id.ToString();
			return this.root.index_dict.unique.id.ContainsKey(key);
		}
	}
	public class CfgEffectRoot
	{
		public List<CfgEffectData> data_list { get; set; }
		public CfgEffectIndexData index_dict { get; set; }
	}
	public partial class CfgEffectData
	{
		/*id*/
		public string id { get; set; }
		/*名字*/
		public string name { get; set; }
		/*预设路径*/
		public string prefab_path { get; set; }
		/*时长*/
		public float duration { get; set; }
		/*插座1*/
		public string socket_name_1 { get; set; }
		/*插座2*/
		public string socket_name_2 { get; set; }
	}
	public class CfgEffectIndexData
	{
		public CfgEffectIndexUniqueData unique { get; set; }
	}
	public class CfgEffectIndexUniqueData
	{
		public Dictionary<string, int> id { get; set; }
	}
}