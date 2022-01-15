using System.Collections.Generic;

namespace CsCat
{
	public partial class Unit
	{
		public PropertyComp propertyComp;

		public void OnPropertyChanged(Dictionary<string, float> oldCalcPropDict,
		  Dictionary<string, float> newCalcPropDict, LinkedHashtable calcPropDictDiff)
		{
			foreach (var key in calcPropDictDiff.Keys)
			{
				if (key.Equals("技能冷却减少百分比") || key.Equals("攻击速度"))
					this.OnSpellCooldownRateChange();
				else if (key.Equals("移动速度"))
					this.OnSpeedChange(oldCalcPropDict.Get<float>(key), newCalcPropDict.Get<float>(key));
				else if (key.Equals("生命上限"))
					this.OnMaxHpChange(oldCalcPropDict.Get<int>(key), newCalcPropDict.Get<int>(key));
			}
		}

		public float GetCalcPropValue(string propertyKey, float defaultValue = 0)
		{
			return this.propertyComp.GetCalcPropValue(propertyKey, defaultValue);
		}


	}
}