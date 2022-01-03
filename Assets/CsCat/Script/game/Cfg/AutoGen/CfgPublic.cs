//AutoGen. DO NOT EDIT!!!
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat
{
	public class CfgPublic
	{
		protected CfgPublic() { }
		public static CfgPublic Instance => instance;
		protected static CfgPublic instance = new CfgPublic();
		protected CfgPublicRoot root;
		public void Parse(string jsonStr) { this.root = JsonMapper.ToObject<CfgPublicRoot>(jsonStr); }
		public List<CfgPublicData> All() { return this.root.data_list; }
		public CfgPublicData Get(int index) { return this.root.data_list[index]; }
		public CfgPublicData get_by_id(string id)
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
	public class CfgPublicRoot
	{
		public List<CfgPublicData> data_list { get; set; }
		public CfgPublicIndexData index_dict { get; set; }
	}
	public partial class CfgPublicData
	{
		/*id*/
		public string id { get; set; }
		/*值*/
		public string value { get; set; }
		/*值dict*/
		public LitJson.JsonData value_dict { get; set; }
		private Dictionary<string, string> __value_dict;
		public Dictionary<string, string> _value_dict
		{
			get
			{
				if (__value_dict == default(Dictionary<string, string>)) __value_dict = value_dict.To<Dictionary<string, string>>();
				return __value_dict;
			}
		}
	}
	public class CfgPublicIndexData
	{
		public CfgPublicIndexUniqueData unique { get; set; }
	}
	public class CfgPublicIndexUniqueData
	{
		public Dictionary<string, int> id { get; set; }
	}
}