using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public class SortedListSerachUtil
  {
    //is_first_occurr
    //1.null:只要找出来就行，不管在第一个还是第n个
    //2.true:第一个出现的位置
    //3.false:最后一个出现的位置
    public static int BinarySearchCat<T>(IList<T> list, T target_value,
      IndexOccurType indexOccurType = IndexOccurType.Any_Index,
      params Comparison<T>[] compare_rules)
    {
      return BinarySearchCat(list, target_value, 0, list.Count - 1, indexOccurType, compare_rules);
    }

    public static int BinarySearchCat<T>(IList<T> list, T target_value, int left_index, int right_index,
      IndexOccurType indexOccurType = IndexOccurType.Any_Index,
      params Comparison<T>[] compare_rules)
    {
      int result_index = -1;
      ListSorttedType listSorttedType = list.GetListSortedType(compare_rules);
      while (left_index <= right_index)
      {
        int middle_index = (left_index + right_index) / 2;
        T middle_value = list[middle_index];
        int compare_result = CompareUtil.CompareWithRules(target_value, middle_value, compare_rules);
        if (compare_result == 0) //相等的情况
        {
          switch (indexOccurType)
          {
            case IndexOccurType.Any_Index:
              return middle_index;
            default:
              result_index = middle_index;
              __BinarySearchSetLeftRightIndex(ref left_index, ref right_index, compare_result, indexOccurType, listSorttedType);
              break;
          }
        }
        else
            __BinarySearchSetLeftRightIndex(ref left_index, ref right_index, compare_result, indexOccurType, listSorttedType);
      }

      return result_index;
    }


    private static void __BinarySearchSetLeftRightIndex(ref int left_index, ref int right_index,int compare_result,
      IndexOccurType indexOccurType = IndexOccurType.Any_Index, ListSorttedType listSorttedType = ListSorttedType.Increace)
    {
      int middle_index = (left_index + right_index) / 2;
      if (compare_result == 0)
      {
        switch (indexOccurType)
        {
          case IndexOccurType.First_Index:
            right_index = middle_index - 1;
            return;
          case IndexOccurType.Last_Index:
            left_index = middle_index + 1;
            return;
        }
        return;
      }

      switch (listSorttedType)
      {
        case ListSorttedType.Increace:
          if(compare_result>0)
            left_index = middle_index + 1;
          else
            right_index = middle_index - 1;
          break;
        case ListSorttedType.Decrease:
          if (compare_result > 0)
            right_index = middle_index - 1;
          else
            left_index = middle_index + 1;
          break;
        default:
          throw new Exception("Not Support ListSorttedType:" + listSorttedType);
      }
    }
  }
}