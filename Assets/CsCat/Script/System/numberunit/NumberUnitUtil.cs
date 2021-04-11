

using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public static class NumberUnitUtil
  {
    private const int max_integer_count = 3; // 有单位时最多显示多少为整数
    private const int max_decimals_count = 1; // 最多显示多少位小数
    private const int init_max_integer_count = 3; // 不使用单位时最多显示多少位

    //根据num和number_unit获取数量
    public static long GetNumber(float num, string number_unit = null,
      Dictionary<string, NumberUnitInfo> Number_Unit_Dict = null)
    {
      Number_Unit_Dict = Number_Unit_Dict ?? NumberUnitConst.Number_Unit_Dict;
      var zhi_shu = 0; // 指数
      if (!number_unit.IsNullOrWhiteSpace())
      {
        var number_unit_info = Number_Unit_Dict[number_unit];
        zhi_shu = number_unit_info.zhi_shu;
      }

      return (long) (num * (Math.Pow(10, zhi_shu)));
    }

    //获取zhi_shu指数对应的单位
    public static string GetNumberUnitInfoByZhiShu(int zhi_shu, List<NumberUnitInfo> Number_Unit_List = null)
    {
      Number_Unit_List = Number_Unit_List ?? NumberUnitConst.Number_Unit_List;
      foreach (var number_unit_info in Number_Unit_List)
      {
        if (number_unit_info.zhi_shu == zhi_shu)
          return number_unit_info.number_unit;
      }

      throw new Exception(string.Format("没有该指数的单位信息 指数:{0}", zhi_shu)); //指数
    }

    //when_show_unit传入的是大于多少开始显示单位
    public static string GetString(long num, int? max_decimals_count, long? when_show_unit,
      List<NumberUnitInfo> Number_Unit_List = null)
    {
      if (when_show_unit.HasValue && num >= when_show_unit)
      {
        long _when_show_unit = when_show_unit.Value;
        int _max_decimals_count = max_decimals_count.GetValueOrDefault(NumberUnitUtil.max_decimals_count);
        var is_fu_shu = num < 0; // 是否是负数
        num = Math.Abs(num);
        var zhi_shu = 0; // 指数
        num = (long) Mathf.Floor(num);
        var get_num = num;
        while (true)
        {
          if (get_num < 10)
            break;
          get_num = (long) Mathf.Floor(get_num / 10f);
          zhi_shu = zhi_shu + 1;
        }

        float show_num;
        string show_unit;
        if ((zhi_shu + 1) <= NumberUnitUtil.init_max_integer_count)
        {
          show_num = num;
          show_unit = "";
        }
        else
        {
          var out_zhi_shu = zhi_shu - NumberUnitUtil.init_max_integer_count;
          var show_wei_shu = out_zhi_shu % NumberUnitUtil.max_integer_count;
          show_num = Mathf.Floor(num / (Mathf.Pow(10, (zhi_shu - show_wei_shu - _max_decimals_count - 1))));
          show_num = Mathf.Floor((show_num + 5) / 10);
          show_num = show_num / (Mathf.Pow(10, _max_decimals_count));
          show_unit = NumberUnitUtil.GetNumberUnitInfoByZhiShu((int) (Mathf.Floor(zhi_shu / 3f) * 3), Number_Unit_List);
        }

        var result = string.Format("{0}{1}", show_num, show_unit);
        if (is_fu_shu) // 如果是负数
          result = string.Format("-{0}", result);
        return result;
      }
      else
        return ((long) Mathf.Floor(num)).ToString();
    }

  }
}