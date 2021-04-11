using System.Collections.Generic;

namespace CsCat
{
  public partial class Unit
  {
    public PropertyComp propertyComp;

    public void OnPropertyChanged(Dictionary<string, float> old_calc_prop_dict,
      Dictionary<string, float> new_calc_prop_dict, LinkedHashtable calc_prop_dict_diff)
    {
      foreach (var key in calc_prop_dict_diff.Keys)
      {
        if (key.Equals("技能冷却减少百分比") || key.Equals("攻击速度"))
          this.OnSpellCooldownRateChange();
        else if (key.Equals("移动速度"))
          this.OnSpeedChange(old_calc_prop_dict.Get<float>(key), new_calc_prop_dict.Get<float>(key));
        else if (key.Equals("生命上限"))
          this.OnMaxHpChange(old_calc_prop_dict.Get<int>(key), new_calc_prop_dict.Get<int>(key));
      }
    }

    public float GetCalcPropValue(string property_key, float default_value = 0)
    {
      return this.propertyComp.GetCalcPropValue(property_key, default_value);
    }


  }
}