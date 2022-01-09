//AutoGen. DO NOT EDIT!!!
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat
{
	public class CfgQuality
	{
		protected CfgQuality() { }
		public static CfgQuality Instance => instance;
		protected static CfgQuality instance = new CfgQuality();
		protected CfgQualityRoot root;
		public void Parse(string jsonStr) { this.root = JsonMapper.ToObject<CfgQualityRoot>(jsonStr); }
		public List<CfgQualityData> All() { return this.root.data_list; }
		public CfgQualityData Get(int index) { return this.root.data_list[index]; }
		public CfgQualityData get_by_id(string id)
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
	public class CfgQualityRoot
	{
		public List<CfgQualityData> data_list { get; set; }
		public CfgQualityIndexData index_dict { get; set; }
	}
	public partial class CfgQualityData
	{
		/*id*/
		public string id { get; set; }
		/*名字*/
		public string name { get; set; }
		/*图片路径*/
		public string icon_path { get; set; }
	}
	public class CfgQualityIndexData
	{
		public CfgQualityIndexUniqueData unique { get; set; }
	}
	public class CfgQualityIndexUniqueData
	{
		public Dictionary<string, int> id { get; set; }
	}
}