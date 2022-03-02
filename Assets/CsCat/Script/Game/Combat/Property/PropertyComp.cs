using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class PropertyComp
	{
		private readonly Dictionary<Key, Dictionary<string, float>> _propSetDict =
			new Dictionary<Key, Dictionary<string, float>>();

		private readonly Dictionary<string, float> _basePropDict = new Dictionary<string, float>();
		private readonly Dictionary<string, float> _calcPropDict = new Dictionary<string, float>();
		private bool _isChanging;
		private Unit _unit;
		private string _unitId;
		private int _level;
		private Hashtable _argDict;

		public PropertyComp(Hashtable argDict)
		{
			this._argDict = argDict;
			this._level = argDict.Get<int>("level");
			this._unitId = argDict.Get<string>("unitId");
			this._argDict = argDict;
		}

		public void OnBuild(Unit unit)
		{
			this._unit = unit;
			this._level = this._unit.GetLevel();
			this._unitId = this._unit.unitId;
		}

		public void AddPropSet(Dictionary<string, float> propSet, string key, string subKey = null)
		{
			_propSetDict.Add(new Key(key, subKey), propSet);
			_CalculateProp();
		}

		public void RemovePropSet(string key, string subKey = null)
		{
			_propSetDict.Remove(new Key(key, subKey));
			_CalculateProp();
		}

		public void _CalculateProp()
		{
			if (_isChanging)
				return;
			var oldCalcPropDict = this._calcPropDict.CloneDeep();
			_basePropDict.Clear();
			//基础属性统计
			var all = CfgProperty.Instance.All();
			for (var i = 0; i < all.Count; i++)
			{
				var cfgPropertyData = all[i];
				_basePropDict[cfgPropertyData.id] = 0;
			}

			foreach (var keyValue in _propSetDict)
			{
				var propSet = keyValue.Value;
				foreach (var keyValue2 in propSet)
				{
					var key = keyValue2.Key;
					float value = _basePropDict.GetOrAddDefault(key, () => 0);
					_basePropDict[key] = value + propSet[key];
				}
			}

			//综合属性计算
			this._calcPropDict.Clear();
			foreach (var keyValue in _basePropDict)
			{
				var key = keyValue.Key;
				_calcPropDict[key] = _basePropDict[key];
			}

			if (this._unit != null)
			{
				var newCalcPropDict = this._calcPropDict;
				var calcPropDictDiff = IDictionaryExtension.GetDiff(oldCalcPropDict, this._calcPropDict);
				this._unit.OnPropertyChanged(oldCalcPropDict, newCalcPropDict, calcPropDictDiff);
			}
		}

		public void StartChange()
		{
			_isChanging = true;
		}

		public void EndChange()
		{
			_isChanging = false;
			_CalculateProp();
		}

		public float GetCalcPropValue(string propertyKey, float defaultValue = 0)
		{
			return this._calcPropDict.GetOrGetDefault(propertyKey, () => defaultValue);
		}


		public (int damageValue, Hashtable specialEffectDict) CalculateOriginalDamageValue(Hashtable argDict)
		{
			return (0, new Hashtable());
		}

		public int CalculateRealDamageValue(int damageValue, Unit targetUnit, Hashtable argDict = null)
		{
			return 0;
		}

		public (int healValue, Hashtable specialEffectDict) CalculateOriginalHealValue(Hashtable argDict)
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