using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class PropertyComp
	{
		private Dictionary<Key, Dictionary<string, float>> prop_set_dict = new Dictionary<Key, Dictionary<string, float>>();
		private Dictionary<string, float> base_prop_dict = new Dictionary<string, float>();
		private Dictionary<string, float> calc_prop_dict = new Dictionary<string, float>();
		private bool is_changing;
		private Unit unit;
		private string unit_id;
		private int level;
		private Hashtable arg_dict;

		public PropertyComp(Hashtable arg_dict)
		{
			this.arg_dict = arg_dict;
			this.level = arg_dict.Get<int>("level");
			this.unit_id = arg_dict.Get<string>("unit_id");
			this.arg_dict = arg_dict;
		}

		public void OnBuild(Unit unit)
		{
			this.unit = unit;
			this.level = this.unit.GetLevel();
			this.unit_id = this.unit.unit_id;

		}

		public void AddPropSet(Dictionary<string, float> prop_set, string key, string sub_key = null)
		{
			prop_set_dict.Add(new Key(key, sub_key), prop_set);
			__CalculateProp();
		}

		public void RemovePropSet(string key, string sub_key = null)
		{
			prop_set_dict.Remove(new Key(key, sub_key));
			__CalculateProp();
		}

		public void __CalculateProp()
		{
			if (is_changing)
				return;
			var old_calc_prop_dict = this.calc_prop_dict.CloneDeep();
			base_prop_dict.Clear();
			//基础属性统计
			foreach (var cfgPropertyData in CfgProperty.Instance.All())
				base_prop_dict[cfgPropertyData.id] = 0;
			foreach (var prop_set in prop_set_dict.Values)
			{
				foreach (var key in prop_set.Keys)
				{
					float value = base_prop_dict.GetOrAddDefault(key, () => 0);
					base_prop_dict[key] = value + prop_set[key];
				}
			}

			//综合属性计算
			this.calc_prop_dict.Clear();
			foreach (var key in base_prop_dict.Keys)
				calc_prop_dict[key] = base_prop_dict[key];

			if (this.unit != null)
			{
				var new_clac_prop_dict = this.calc_prop_dict;
				var clac_prop_dict_diff = IDictionaryExtension.GetDiff(old_calc_prop_dict, this.calc_prop_dict);
				this.unit.OnPropertyChanged(old_calc_prop_dict, new_clac_prop_dict, clac_prop_dict_diff);
			}
		}

		public void StartChange()
		{
			is_changing = true;
		}

		public void EndChange()
		{
			is_changing = false;
			__CalculateProp();
		}

		public float GetCalcPropValue(string property_key, float default_value = 0)
		{
			return this.calc_prop_dict.GetOrGetDefault(property_key, () => default_value);
		}


		public (int damage_value, Hashtable special_effect_dict) CalculateOriginalDamageValue(Hashtable arg_dict)
		{
			return (0, new Hashtable());
		}

		public int CalculateRealDamageValue(int damage_value, Unit target_unit, Hashtable arg_dict = null)
		{
			return 0;
		}

		public (int heal_value, Hashtable special_effect_dict) CalculateOriginalHealValue(Hashtable arg_dict)
		{
			return (0, new Hashtable());
		}

		public int CalculateRealHealValue(int heal_value, Unit target_unit, Hashtable arg_dict = null)
		{
			return 0;
		}

		public void Destroy()
		{

		}
	}
}
