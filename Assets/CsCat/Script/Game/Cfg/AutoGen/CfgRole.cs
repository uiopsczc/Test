//AutoGen. DO NOT EDIT!!!
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat
{
	public class CfgRole
	{
		protected CfgRole() { }
		public static CfgRole Instance => instance;
		protected static CfgRole instance = new CfgRole();
		protected CfgRoleRoot root;
		public void Parse(string jsonStr) { this.root = JsonMapper.ToObject<CfgRoleRoot>(jsonStr); }
		public List<CfgRoleData> All() { return this.root.data_list; }
		public CfgRoleData Get(int index) { return this.root.data_list[index]; }
		public CfgRoleData get_by_id(string id)
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
	public class CfgRoleRoot
	{
		public List<CfgRoleData> data_list { get; set; }
		public CfgRoleIndexData index_dict { get; set; }
	}
	public partial class CfgRoleData
	{
		/*ID*/
		public string id { get; set; }
		/*名字*/
		public string name { get; set; }
		/*class_path_lua*/
		public string class_path_lua { get; set; }
		/*class_path_cs*/
		public string class_path_cs { get; set; }
	}
	public class CfgRoleIndexData
	{
		public CfgRoleIndexUniqueData unique { get; set; }
	}
	public class CfgRoleIndexUniqueData
	{
		public Dictionary<string, int> id { get; set; }
	}
}