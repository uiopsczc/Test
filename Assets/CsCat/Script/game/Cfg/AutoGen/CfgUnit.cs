//AutoGen. DO NOT EDIT!!!
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat
{
	public class CfgUnit
	{
		protected CfgUnit() { }
		public static CfgUnit Instance => instance;
		protected static CfgUnit instance = new CfgUnit();
		protected CfgUnitRoot root;
		public void Parse(string jsonStr) { this.root = JsonMapper.ToObject<CfgUnitRoot>(jsonStr); }
		public List<CfgUnitData> All() { return this.root.data_list; }
		public CfgUnitData Get(int index) { return this.root.data_list[index]; }
		public CfgUnitData get_by_id(string id)
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
	public class CfgUnitRoot
	{
		public List<CfgUnitData> data_list { get; set; }
		public CfgUnitIndexData index_dict { get; set; }
	}
	public partial class CfgUnitData
	{
		/*ID*/
		public string id { get; set; }
		/*名字*/
		public string name { get; set; }
		/*类型*/
		public string type { get; set; }
		/*y轴偏移*/
		public float offset_y { get; set; }
		/*半径*/
		public float radius { get; set; }
		/*缩放*/
		public float scale { get; set; }
		/*一轮走路动画走多远*/
		public float walk_step_length { get; set; }
		/*模型路径*/
		public string model_path { get; set; }
		/*普攻ids*/
		public LitJson.JsonData normal_attack_ids { get; set; }
		private string[] __normal_attack_ids;
		public string[] _normal_attack_ids
		{
			get
			{
				if (__normal_attack_ids == default(string[])) __normal_attack_ids = normal_attack_ids.To<string[]>();
				return __normal_attack_ids;
			}
		}
		/*技能ids*/
		public LitJson.JsonData skill_ids { get; set; }
		private string[] __skill_ids;
		public string[] _skill_ids
		{
			get
			{
				if (__skill_ids == default(string[])) __skill_ids = skill_ids.To<string[]>();
				return __skill_ids;
			}
		}
		/*ai实现类(lua)*/
		public string ai_class_path_lua { get; set; }
		/*ai实现类(cs)*/
		public string ai_class_path_cs { get; set; }
		/*死亡后是否保留尸体*/
		public bool is_keep_dead_body { get; set; }
		/*死亡后多少秒才销毁尸体*/
		public float dead_body_dealy { get; set; }
		/*死亡时候触发的特效id*/
		public string death_effect_id { get; set; }
		/*被动buff ids*/
		public LitJson.JsonData passive_buff_ids { get; set; }
		private string[] __passive_buff_ids;
		public string[] _passive_buff_ids
		{
			get
			{
				if (__passive_buff_ids == default(string[])) __passive_buff_ids = passive_buff_ids.To<string[]>();
				return __passive_buff_ids;
			}
		}
	}
	public class CfgUnitIndexData
	{
		public CfgUnitIndexUniqueData unique { get; set; }
	}
	public class CfgUnitIndexUniqueData
	{
		public Dictionary<string, int> id { get; set; }
	}
}