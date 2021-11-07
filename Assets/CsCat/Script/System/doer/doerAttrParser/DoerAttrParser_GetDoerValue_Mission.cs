using System.Collections;

namespace CsCat
{
    public partial class DoerAttrParser
    {
        public bool GetDoerValue_Mission(Doer doer, string key, string typeString, out string result)
        {
            bool isBreak = false;
            result = null;
            if (doer is Mission mission)
            {
                if (key.Equals(StringConst.String_status))
                {
                    if (mission.IsReady())
                    {
                        result = ConvertValue(3, typeString); //已就绪,可以被完成
                        return true;
                    }

                    result = ConvertValue(2, typeString); //未完成
                    return true;
                }

                if (key.StartsWith(StringConst.String_items_dot)) // 物品
                {
                    string itemId = key.Substring(StringConst.String_items_dot.Length);
                    Hashtable items = mission.Get<Hashtable>(StringConst.String_items);
                    if (items != null)
                    {
                        int count = items.Get<int>(itemId);
                        result = ConvertValue(count, typeString);
                        return true;
                    }
                }
            }

            return isBreak;
        }
    }
}