//AutoGen. DO NOT EDIT!!!
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat
{
	public class CfgSummonbeastguardskill
	{
		protected CfgSummonbeastguardskill() { }
		public static CfgSummonbeastguardskill Instance => instance;
		protected static CfgSummonbeastguardskill instance = new CfgSummonbeastguardskill();
		protected CfgSummonbeastguardskillRoot root;
		public void Parse(string jsonStr) { this.root = JsonMapper.ToObject<CfgSummonbeastguardskillRoot>(jsonStr); }
		public List<CfgSummonbeastguardskillData> All() { return this.root.data_list; }
		public CfgSummonbeastguardskillData Get(int index) { return this.root.data_list[index]; }
		public CfgSummonbeastguardskillData get_by_id(string id)
		{
			string key = id.ToString();
			return this.Get(this.root.index_dict.unique.id[key]);
		}
		public bool contain_key_by_id(string id)
		{
			string key = id.ToString();
			return this.root.index_dict.unique.id.ContainsKey(key);
		}
		public CfgSummonbeastguardskillData get_by_summonbeast_id_and_star(int summonbeast_id, int star)
		{
			string[] keys = { summonbeast_id.ToString(), star.ToString() };
			string key = string.Join(".", keys);
			return this.Get(this.root.index_dict.unique.summonbeast_id_and_star[key]);
		}
		public bool contain_key_by_summonbeast_id_and_star(int summonbeast_id, int star)
		{
			string[] keys = { summonbeast_id.ToString(), star.ToString() };
			string key = string.Join(".", keys);
			return this.root.index_dict.unique.summonbeast_id_and_star.ContainsKey(key);
		}
		public List<CfgSummonbeastguardskillData> get_by_summonbeast_id(int summonbeast_id)
		{
			string key = summonbeast_id.ToString();
			List<CfgSummonbeastguardskillData> result = new List<CfgSummonbeastguardskillData>();
			List<int> indexes = this.root.index_dict.multiple.summonbeast_id[key];
			foreach (int index in indexes) { result.Add(this.Get(index)); }
			return result;
		}
		public bool contain_key_by_summonbeast_id(int summonbeast_id)
		{
			string key = summonbeast_id.ToString();
			return this.root.index_dict.multiple.summonbeast_id.ContainsKey(key);
		}
		public List<CfgSummonbeastguardskillData> get_by_summonbeast_id_and_star_fighting(int summonbeast_id, int star_fighting)
		{
			string[] keys = { summonbeast_id.ToString(), star_fighting.ToString() };
			string key = string.Join(".", keys);
			List<CfgSummonbeastguardskillData> result = new List<CfgSummonbeastguardskillData>();
			List<int> indexes = this.root.index_dict.multiple.summonbeast_id_and_star_fighting[key];
			foreach (int index in indexes) { result.Add(this.Get(index)); }
			return result;
		}
		public bool contain_key_by_summonbeast_id_and_star_fighting(int summonbeast_id, int star_fighting)
		{
			string[] keys = { summonbeast_id.ToString(), star_fighting.ToString() };
			string key = string.Join(".", keys);
			return this.root.index_dict.multiple.summonbeast_id_and_star_fighting.ContainsKey(key);
		}
	}
	public class CfgSummonbeastguardskillRoot
	{
		public List<CfgSummonbeastguardskillData> data_list { get; set; }
		public CfgSummonbeastguardskillIndexData index_dict { get; set; }
	}
	public partial class CfgSummonbeastguardskillData
	{
		/*id*/
		public string id { get; set; }
		/*召唤兽id*/
		public int summonbeast_id { get; set; }
		/*星级*/
		public int star { get; set; }
		/*星级战力*/
		public int star_fighting { get; set; }
		/*技能描述1*/
		public string skill_desc_lang1 { get; set; }
		/*技能描述参数列表1*/
		public LitJson.JsonData skill_desc_args1 { get; set; }
		/*技能描述2*/
		public string skill_desc_lang2 { get; set; }
		/*技能描述参数列表2*/
		public LitJson.JsonData skill_desc_args2 { get; set; }
		/*技能描述3*/
		public string skill_desc_lang3 { get; set; }
		/*技能描述参数列表3*/
		public LitJson.JsonData skill_desc_args3 { get; set; }
		/*技能描述4(长描述)*/
		public string skill_desc_lang4 { get; set; }
		/*技能描述参数列表4*/
		public LitJson.JsonData skill_desc_args4 { get; set; }
	}
	public class CfgSummonbeastguardskillIndexData
	{
		public CfgSummonbeastguardskillIndexUniqueData unique { get; set; }
		public CfgSummonbeastguardskillIndexMultipleData multiple { get; set; }
	}
	public class CfgSummonbeastguardskillIndexUniqueData
	{
		public Dictionary<string, int> id { get; set; }
		public Dictionary<string, int> summonbeast_id_and_star { get; set; }
	}
	public class CfgSummonbeastguardskillIndexMultipleData
	{
		public Dictionary<string, List<int>> summonbeast_id { get; set; }
		public Dictionary<string, List<int>> summonbeast_id_and_star_fighting { get; set; }
	}
}