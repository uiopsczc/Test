using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditorInternal;

#endif

namespace CsCat
{
	public static class IListExtension
	{
		public static bool ContainsIndex(this IList self, int index)
		{
			return index < self.Count && index >= 0;
		}

		/// <summary>
		///   ��Ϊ��Ӧ��ArrayList
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ArrayList ToArrayList(this IList self)
		{
			var list = new ArrayList();
			for (int i = 0; i < self.Count; i++)
				list.Add(self[i]);
			return list;
		}

		public static void BubbleSort(this IList self, Func<object, object, bool> compareFunc)
		{
			SortUtil.BubbleSort(self, compareFunc);
		}

		public static void BubbleSortWithCompareRules(this IList self, IList<Comparison<object>> compareRules)
		{
			SortUtil.BubbleSortWithCompareRules(self, compareRules);
		}

		public static void MergeSort(this IList self, Func<object, object, bool> compareFunc)
		{
			SortUtil.MergeSort(self, compareFunc);
		}

		public static void MergeSortWithCompareRules(this IList self, IList<Comparison<object>> compareRules)
		{
			SortUtil.MergeSortWithCompareRules(self, compareRules);
		}

		public static void QuickSortWithCompareRules(this IList self, IList<Comparison<object>> compareRules)
		{
			SortUtil.QuickSortWithCompareRules(self, compareRules);
		}

		public static void QuickSort(this IList self, Func<object, object, bool> compareFunc)
		{
			SortUtil.QuickSort(self, compareFunc);
		}


		//////////////////////////////////////////////////////////////////////
		// Diff���
		//////////////////////////////////////////////////////////////////////
		// �����ApplyDiffʹ��
		// ��newΪ��׼����ȡnew�����old��һ���Ĳ���
		// local diff = table.GetDiff(old, new)
		//  table.ApplyDiff(old, diff)
		// ����old�ľͱ�ɺ�newһģһ��������
		public static LinkedHashtable GetDiff(this IList oldList, IList newList)
		{
			return ListUtil.GetDiff(oldList, newList);
		}

		// table.ApplyDiff(old, diff)
		// ��diff�еĶ���Ӧ�õ�old��
		// ��Ҫ����ΪArray��ʱ����Ҫ���¸�ֵ��List��ʱ�򣬿��Բ���Ҫ���¸�ֵ
		public static IList ApplyDiff(this IList oldList, LinkedHashtable diffDict)
		{
			return ListUtil.ApplyDiff(oldList, diffDict);
		}

		// �����ApplyDiffʹ��
		// ��newΪ��׼����ȡnew���У���old��û�е�
		// local diff = table.GetNotExist(old, new)
		// table.ApplyDiff(old, diff)
		// ����old����new�е��ֶ�
		public static LinkedHashtable GetNotExist(this IList oldList, IList newList)
		{
			return ListUtil.GetNotExist(oldList, newList);
		}

		//����table�Ƿ�һ��
		public static bool IsDiff(this IList oldList, IList newList)
		{
			return ListUtil.IsDiff(oldList, newList);
		}

#if UNITY_EDITOR
		public static void ToReorderableList(this IList toReorderList, ref ReorderableList reorderableList)
		{
			ReorderableListUtil.ToReorderableList(toReorderList, ref reorderableList);
		}
#endif
	}
}