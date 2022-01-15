using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class PropertyComp
	{
		private Dictionary<Key, Dictionary<string, float>> propSetDict =
			new Dictionary<Key, Dictionary<string, float>>();

		private Dictionary<string, float> basePropDict = new Dictionary<string, float>();
		private Dictionary<string, float> calcPropDict = new Dictionary<string, float>();
		private bool isChanging;
		private Unit unit;
		private string unitId;
		private int level;
		private Hashtable argDict;

		public PropertyComp(Hashtable argDict)
		{
			this.argDict = argDict;
			this.level = argDict.Get<int>("level");
			this.unitId = argDict.Get<string>("unit_id");
			this.argDict = argDict;
		}

		public void OnBuild(Unit unit)
		{
			this.unit = unit;
			this.level = this.unit.GetLevel();
			this.unitId = this.unit.unitId;
		}

		public void AddPropSet(Dictionary<string, float> propSet, string key, string subKey = null)
		{
			propSetDict.Add(new Key(key, subKey), propSet);
			__CalculateProp();
		}

		public void RemovePropSet(string key, string subKey = null)
		{
			propSetDict.Remove(new Key(key, subKey));
			__CalculateProp();
		}

		public void __CalculateProp()
		{
			if (isChanging)
				return;
			var oldCalcPropDict = this.calcPropDict.CloneDeep();
			basePropDict.Clear();
			//基础属性统计
			var all = CfgProperty.Instance.All();
			for (var i = 0; i < all.Count; i++)
			{
				var cfgPropertyData = all[i];
				basePropDict[cfgPropertyData.id] = 0;
			}

			foreach (var propSet in propSetDict.Values)
			{
				foreach (var key in propSet.Keys)
				{
					float value = basePropDict.GetOrAddDefault(key, () => 0);
					basePropDict[key] = value + propSet[key];
				}
			}

			//综合属性计算
			this.calcPropDict.Clear();
			foreach (var key in basePropDict.Keys)
				calcPropDict[key] = basePropDict[key];

			if (this.unit != null)
			{
				var newCalcPropDict = this.calcPropDict;
				var calcPropDictDiff = IDictionaryExtension.GetDiff(oldCalcPropDict, this.calcPropDict);
				this.unit.OnPropertyChanged(oldCalcPropDict, newCalcPropDict, calcPropDictDiff);
			}
		}

		public void StartChange()
		{
			isChanging = true;
		}

		public void EndChange()
		{
			isChanging = false;
			__CalculateProp();
		}

		public float GetCalcPropValue(string propertyKey, float defaultValue = 0)
		{
			return this.calcPropDict.GetOrGetDefault(propertyKey, () => defaultValue);
		}


		public (int damageValue, Hashtable specialEffectDict) CalculateOriginalDamageValue(Hashtable arg_dict)
		{
			return (0, new Hashtable());
		}

		public int CalculateRealDamageValue(int damageValue, Unit targetUnit, Hashtable argDict = null)
		{
			return 0;
		}

		public (int healValue, Hashtable specialEffectDict) CalculateOriginalHealValue(Hashtable arg_dict)
		{
			return (0, new Hashtable());
		}

		public int CalculateRealHealValue(int healValue, Unit targetUnit, Hashtable argDict = null)
		{
			return 0;
		}

		public void Destroy()
		{
		}
	}
}