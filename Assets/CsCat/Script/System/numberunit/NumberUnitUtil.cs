using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
    public static class NumberUnitUtil
    {
        private const int Max_Integer_Count = 3; // 有单位时最多显示多少为整数
        private const int Max_Decimals_Count = 1; // 最多显示多少位小数
        private const int Init_Max_Integer_Count = 3; // 不使用单位时最多显示多少位

        //根据num和number_unit获取数量
        public static long GetNumber(float num, string numberUnit = null,
            Dictionary<string, NumberUnitInfo> numberUnitDict = null)
        {
            numberUnitDict = numberUnitDict ?? NumberUnitConst.NumberUnitDict;
            var zhiShu = 0; // 指数
            if (!numberUnit.IsNullOrWhiteSpace())
            {
                var numberUnitInfo = numberUnitDict[numberUnit];
                zhiShu = numberUnitInfo.zhiShu;
            }

            return (long) (num * (Math.Pow(10, zhiShu)));
        }

        //获取zhi_shu指数对应的单位
        public static string GetNumberUnitInfoByZhiShu(int zhi_shu, List<NumberUnitInfo> numberUnitList = null)
        {
            numberUnitList = numberUnitList ?? NumberUnitConst.NumberUnitList;
            foreach (var number_unit_info in numberUnitList)
            {
                if (number_unit_info.zhiShu == zhi_shu)
                    return number_unit_info.numberUnit;
            }

            throw new Exception(string.Format("没有该指数的单位信息 指数:{0}", zhi_shu)); //指数
        }

        //when_show_unit传入的是大于多少开始显示单位
        public static string GetString(long num, int? maxDecimalsCount, long? whenShowUnit,
            List<NumberUnitInfo> numberUnitList = null)
        {
            if (whenShowUnit.HasValue && num >= whenShowUnit)
            {
                long whenShowUnitValue = whenShowUnit.Value;
                int maxDecimalsCountValue = maxDecimalsCount.GetValueOrDefault(NumberUnitUtil.Max_Decimals_Count);
                var isFuShu = num < 0; // 是否是负数
                num = Math.Abs(num);
                var zhiShu = 0; // 指数
                num = (long) Mathf.Floor(num);
                var getNum = num;
                while (true)
                {
                    if (getNum < 10)
                        break;
                    getNum = (long) Mathf.Floor(getNum / 10f);
                    zhiShu = zhiShu + 1;
                }

                float showNum;
                string showUnit;
                if ((zhiShu + 1) <= NumberUnitUtil.Init_Max_Integer_Count)
                {
                    showNum = num;
                    showUnit = StringConst.String_Empty;
                }
                else
                {
                    var outZhiShu = zhiShu - NumberUnitUtil.Init_Max_Integer_Count;
                    var showWeiShu = outZhiShu % NumberUnitUtil.Max_Integer_Count;
                    showNum = Mathf.Floor(num / (Mathf.Pow(10, (zhiShu - showWeiShu - maxDecimalsCountValue - 1))));
                    showNum = Mathf.Floor((showNum + 5) / 10);
                    showNum = showNum / (Mathf.Pow(10, maxDecimalsCountValue));
                    showUnit = NumberUnitUtil.GetNumberUnitInfoByZhiShu((int) (Mathf.Floor(zhiShu / 3f) * 3),
                        numberUnitList);
                }

                var result = string.Format("{0}{1}", showNum, showUnit);
                if (isFuShu) // 如果是负数
                    result = string.Format("-{0}", result);
                return result;
            }

            return ((long) Mathf.Floor(num)).ToString();
        }
    }
}