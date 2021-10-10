using System;
using System.Collections;

namespace CsCat
{
    public static class IDictionaryUtil
    {
        //////////////////////////////////////////////////////////////////////
        // Diff相关
        //////////////////////////////////////////////////////////////////////
        // 必须和ApplyDiff使用
        // 以new为基准，获取new相对于old不一样的部分
        // local diff = table.GetDiff(old, new)
        //  table.ApplyDiff(old, diff)
        // 这样old的就变成和new一模一样的数据
        public static LinkedHashtable GetDiff(IDictionary oldDict, IDictionary newDict)
        {
            var diff = new LinkedHashtable();
            foreach (DictionaryEntry dictionaryEntry in newDict)
            {
                var newK = dictionaryEntry.Key;
                var newV = dictionaryEntry.Value;
                if (newV is IDictionary newVDict)
                {
                    switch (newVDict.Count)
                    {
                        case 0 when (!oldDict.Contains(newK) || oldDict[newK].GetType() != newVDict.GetType() ||
                                     (oldDict[newK] is IDictionary &&
                                      ((IDictionary) oldDict[newK]).Count != 0)):
                            diff[newK] = StringConst.String_New_In_Table + newVDict.GetType();
                            break;
                        default:
                        {
                            if (oldDict.Contains(newK) && oldDict[newK] is IDictionary)
                                diff[newK] = GetDiff((IDictionary) oldDict[newK], newVDict);
                            else if (!oldDict.Contains(newK) || !newVDict.Equals(oldDict[newK]))
                                diff[newK] = CloneUtil.CloneDeep(newV);
                            break;
                        }
                    }
                }
                else if (newV is IList list && oldDict.Contains(newK) && oldDict[newK] is IList)
                    diff[newK] = ListUtil.GetDiff((IList) oldDict[newK], list);
                else if (!oldDict.Contains(newK) || !newV.Equals(oldDict[newK]))
                    diff[newK] = newV;
            }

            foreach (var key in oldDict.Keys)
            {
                if (!newDict.Contains(key))
                    diff[key] = StringConst.String_Nil_In_Table;
            }

            if (diff.Count == 0)
                diff = null;
            return diff;
        }

        // table.ApplyDiff(old, diff)
        // 将diff中的东西应用到old中
        public static void ApplyDiff(IDictionary oldDict, LinkedHashtable diffDict)
        {
            if (diffDict == null)
            {
                return;
            }

            foreach (DictionaryEntry dictionaryEntry in diffDict)
            {
                var k = dictionaryEntry.Key;
                var v = dictionaryEntry.Value;
                if (v.Equals(StringConst.String_Nil_In_Table))
                    oldDict.Remove(k);
                else if (v.ToString().StartsWith(StringConst.String_New_In_Table))
                {
                    string typeString = v.ToString().Substring(StringConst.String_New_In_Table.Length);
                    Type type = TypeUtil.GetType(typeString);
                    oldDict[k] = type.CreateInstance<object>();
                }
                else if (oldDict.Contains(k) && oldDict[k] is IDictionary && v is LinkedHashtable hashtable)
                    ApplyDiff((IDictionary) oldDict[k], hashtable);
                else if (oldDict.Contains(k) && oldDict[k] is IList && v is LinkedHashtable linkedHashtable)
                    oldDict[k] = ListUtil.ApplyDiff((IList) oldDict[k], linkedHashtable);
                else
                    oldDict[k] = v;
            }
        }

        // 必须和ApplyDiff使用
        // 以new为基准，获取new中有，但old中没有的
        // local diff = table.GetNotExist(old, new)
        // table.ApplyDiff(old, diff)
        // 这样old就有new中的字段
        public static LinkedHashtable GetNotExist(IDictionary oldDict, IDictionary newDict)
        {
            var diff = new LinkedHashtable();
            foreach (DictionaryEntry dictionaryEntry in newDict)
            {
                var newK = dictionaryEntry.Key;
                var newV = dictionaryEntry.Value;
                if (!oldDict.Contains(newK))
                    diff[newK] = newV;
                else
                {
                    if (newV is IDictionary dictionary && oldDict[newK] is IDictionary)
                        diff[newK] = GetNotExist((IDictionary) oldDict[newK], dictionary);
                    else if (newV is IList list && oldDict[newK] is IList)
                        diff[newK] = ListUtil.GetNotExist((IList) oldDict[newK], list);
                    //其他情况不用处理
                }
            }

            return diff;
        }

        //两个table是否不一样
        public static bool IsDiff(IDictionary oldDict, IDictionary newDict)
        {
            foreach (var key in oldDict.Keys)
            {
                if (!newDict.Contains(key))
                    return true;
            }

            foreach (DictionaryEntry dictionaryEntry in newDict)
            {
                var newKey = dictionaryEntry.Key;
                var newValue = dictionaryEntry.Value;
                if (!oldDict.Contains(newKey))
                    return false;
                switch (newValue)
                {
                    case IDictionary dictionary when !(oldDict[newKey] is IDictionary):
                        return false;
                    case IDictionary dictionary when IsDiff((IDictionary) oldDict[newKey], dictionary):
                        return true;
                    case IDictionary dictionary:
                        break;
                    case IList list when !(oldDict[newKey] is IList):
                        return false;
                    case IList list when ListUtil.IsDiff(oldDict[newKey] as IList, list):
                        return true;
                    case IList list:
                        break;
                    default:
                    {
                        if (!newValue.Equals(oldDict[newKey]))
                            return true;
                        break;
                    }
                }
            }

            return false;
        }
    }
}