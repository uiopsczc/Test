//AutoGen. DO NOT EDIT!!!
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat
{
	public class CfgDoerEvent
	{
		protected CfgDoerEvent() { }
		public static CfgDoerEvent Instance => instance;
		protected static CfgDoerEvent instance = new CfgDoerEvent();
		protected CfgDoerEventRoot root;
		public void Parse(string jsonStr) { this.root = JsonMapper.ToObject<CfgDoerEventRoot>(jsonStr); }
		public List<CfgDoerEventData> All() { return this.root.data_list; }
		public CfgDoerEventData Get(int index) { return this.root.data_list[index]; }
		public CfgDoerEventData get_by_id(string id)
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
	public class CfgDoerEventRoot
	{
		public List<CfgDoerEventData> data_list { get; set; }
		public CfgDoerEventIndexData index_dict { get; set; }
	}
	public partial class CfgDoerEventData
	{
		/*ID*/
		public string id { get; set; }
		/*名字*/
		public string name { get; set; }
		/*是否不打开*/
		public bool is_not_open { get; set; }
		/*是否不弹窗提示*/
		public bool is_not_talk { get; set; }
		/*触发条件*/
		public string trigger_condition { get; set; }
		/*触发提示*/
		public string trigger_desc { get; set; }
		/*不触发提示*/
		public string can_not_trigger_desc { get; set; }
		/*class_path_lua*/
		public string class_path_lua { get; set; }
		/*class_path_cs*/
		public string class_path_cs { get; set; }
		/*子步骤ids*/
		public LitJson.JsonData step_ids { get; set; }
		private string[] __step_ids;
		public string[] _step_ids
		{
			get
			{
				if (__step_ids == default(string[])) __step_ids = step_ids.To<string[]>();
				return __step_ids;
			}
		}
	}
	public class CfgDoerEventIndexData
	{
		public CfgDoerEventIndexUniqueData unique { get; set; }
	}
	public class CfgDoerEventIndexUniqueData
	{
		public Dictionary<string, int> id { get; set; }
	}
}