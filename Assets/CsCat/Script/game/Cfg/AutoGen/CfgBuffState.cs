//AutoGen. DO NOT EDIT!!!
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat
{
	public class CfgBuffState
	{
		protected CfgBuffState() { }
		public static CfgBuffState Instance => instance;
		protected static CfgBuffState instance = new CfgBuffState();
		protected CfgBuffStateRoot root;
		public void Parse(string jsonStr) { this.root = JsonMapper.ToObject<CfgBuffStateRoot>(jsonStr); }
		public List<CfgBuffStateData> All() { return this.root.data_list; }
		public CfgBuffStateData Get(int index) { return this.root.data_list[index]; }
		public CfgBuffStateData get_by_id(string id)
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
	public class CfgBuffStateRoot
	{
		public List<CfgBuffStateData> data_list { get; set; }
		public CfgBuffStateIndexData index_dict { get; set; }
	}
	public partial class CfgBuffStateData
	{
		/*id*/
		public string id { get; set; }
		/*名字*/
		public string name { get; set; }
	}
	public class CfgBuffStateIndexData
	{
		public CfgBuffStateIndexUniqueData unique { get; set; }
	}
	public class CfgBuffStateIndexUniqueData
	{
		public Dictionary<string, int> id { get; set; }
	}
}