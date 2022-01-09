using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class SortUtil
	{
		//////////////////////////////////////////////////////////////////////
		// 冒泡排序
		// O(n^2)
		//////////////////////////////////////////////////////////////////////
		//如：list.BubbleSort(list, (a, b)=>return a.count <= b.count)
		//则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
		public static void BubbleSort<T>(IList<T> list, Func<T, T, bool> func)
		{
			int count = list.Count;
			for (int i = 0; i < count - 1; i++)
			{
				for (int j = 0; j < count - i - 1; j++)
					if (!func(list[j], list[j + 1]))
					{
						var tmp = list[j];
						list[j] = list[j + 1];
						list[j + 1] = tmp;
					}
			}
		}

		public static void BubbleSortWithCompareRules<T>(IList<T> list, IList<Comparison<T>> compareRules)
		{
			BubbleSort(list, (a, b) => CompareUtil.CompareWithRules(a, b, compareRules) < 0);
		}

		//////////////////////////////////////////////////////////////////////////////
		public static void BubbleSort(IList list, Func<object, object, bool> func)
		{
			int count = list.Count;
			for (int i = 0; i < count - 1; i++)
			{
				for (int j = 0; j < count - i - 1; j++)
					if (!func(list[j], list[j + 1]))
					{
						var tmp = list[j];
						list[j] = list[j + 1];
						list[j + 1] = tmp;
					}
			}
		}

		public static void BubbleSortWithCompareRules(IList list, IList<Comparison<object>> compareRules)
		{
			BubbleSort(list, (a, b) => CompareUtil.CompareWithRules(a, b, compareRules) < 0);
		}

		//////////////////////////////////////////////////////////////////////
		// 归并排序
		// https://www.youtube.com/watch?v=TzeBrDU-JaY
		// O(N* logN)
		// 需要另外的N空间来进行排序处理，但稳定 最好的情况需要时间为N*logN,最坏的情况需要时间为N* logN
		//////////////////////////////////////////////////////////////////////
		//如：list.BubbleSort(list, (a, b)=>return a.count <= b.count)
		//则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
		public static void MergeSort<T>(IList<T> list, Func<T, T, bool> func)
		{
			__MergeSort(list, 0, list.Count - 1, func);
		}

		public static void MergeSortWithCompareRules<T>(IList<T> list, IList<Comparison<T>> compareRules)
		{
			MergeSort(list, (a, b) => CompareUtil.CompareWithRules(a, b, compareRules) < 0);
		}

		private static void __MergeSort<T>(IList<T> list, int leftIndex, int rightIndex, Func<T, T, bool> func)
		{
			if (leftIndex >= rightIndex)
				return;
			int middleIndex = (leftIndex + rightIndex) / 2;
			__MergeSort(list, leftIndex, middleIndex, func);
			__MergeSort(list, middleIndex + 1, rightIndex, func);
			__Merge(list, leftIndex, middleIndex, rightIndex, func);
		}

		private static void __Merge<T>(IList<T> list, int leftIndex, int middleIndex, int rightIndex,
			Func<T, T, bool> func)
		{
			List<T> newList = new List<T>(rightIndex - leftIndex); //新的数组(用于存放排序后的元素)
			int leftListCurIndex = leftIndex; //左边的数组的当前index
			int rightListCurIndex = middleIndex + 1; //右边的数组的当前index
			while (leftListCurIndex <= middleIndex && rightListCurIndex <= rightIndex)
			{
				if (func(list[leftListCurIndex], list[rightListCurIndex]))
				{
					newList.Add(list[leftListCurIndex]);
					leftListCurIndex++;
				}
				else
				{
					newList.Add(list[rightListCurIndex]);
					rightListCurIndex++;
				}
			}

			//left_array_cur_index <= mid and right_array_cur_index <= right 其中之一不成立的情况
			//处理没有被完全处理的数组
			for (int i = leftListCurIndex; i <= middleIndex; i++)
				newList.Add(list[i]);
			for (int i = rightListCurIndex; i <= rightIndex; i++)
				newList.Add(list[i]);

			//重新赋值给list
			for (int i = 0; i < newList.Count; i++)
				list[leftIndex + i] = newList[i];
		}

		//////////////////////////////////////////////////////////////////////////////
		public static void MergeSort(IList list, Func<object, object, bool> func)
		{
			_MergeSort(list, 0, list.Count - 1, func);
		}

		public static void MergeSortWithCompareRules(IList list, IList<Comparison<object>> compareRules)
		{
			MergeSort(list, (a, b) => CompareUtil.CompareWithRules(a, b, compareRules) < 0);
		}

		private static void _MergeSort(IList list, int leftIndex, int rightIndex, Func<object, object, bool> func)
		{
			if (leftIndex >= rightIndex)
				return;
			int middle = (leftIndex + rightIndex) / 2;
			_MergeSort(list, leftIndex, middle, func);
			_MergeSort(list, middle + 1, rightIndex, func);
			__Merge(list, leftIndex, middle, rightIndex, func);
		}

		private static void __Merge(IList list, int leftIndex, int middleIndex, int rightIndex,
			Func<object, object, bool> func)
		{
			List<object> newList = new List<object>(rightIndex - leftIndex); //新的数组(用于存放排序后的元素)
			int leftListCurIndex = leftIndex; //左边的数组的当前index
			int rightListCurIndex = middleIndex + 1; //右边的数组的当前index
			while (leftListCurIndex <= middleIndex && rightListCurIndex <= rightIndex)
			{
				if (!func(list[leftListCurIndex], list[rightListCurIndex]))
				{
					newList.Add(list[leftListCurIndex]);
					leftListCurIndex++;
				}
				else
				{
					newList.Add(list[rightListCurIndex]);
					rightListCurIndex++;
				}
			}

			//left_array_cur_index <= mid and right_array_cur_index <= right 其中之一不成立的情况
			//处理没有被完全处理的数组
			for (; leftListCurIndex <= middleIndex; leftListCurIndex++)
				newList.Add(list[leftListCurIndex]);
			for (; rightListCurIndex <= rightIndex; rightListCurIndex++)
				newList.Add(list[rightListCurIndex]);

			//重新赋值给list
			for (int i = 0; i <= newList.Count; i++)
				list[leftIndex + i] = newList[i];
		}


		//////////////////////////////////////////////////////////////////////
		// 快速排序
		// https://www.youtube.com/watch?v=COk73cpQbFQ
		// O(N* logN)
		// 不需要另外的N空间来进行排序处理，但不稳定 最好的情况需要时间为N*logN,最坏的情况需要时间为N^2
		//////////////////////////////////////////////////////////////////////
		//如：list.QuickSort_Array(list, (a, b)=>return a.count <= b.count)
		//则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
		public static void QuickSort(IList list, Func<object, object, bool> func, int? leftIndex = null,
			int? rightIndex = null)
		{
			int leftIndexValue = leftIndex.GetValueOrDefault(0);
			int rightIndexValue = rightIndex.GetValueOrDefault(list.Count - 1);
			if (leftIndexValue >= rightIndexValue)
				return;
			//在partition_index左边的都比index的value小，右边的都比index的value大
			var partitionIndex = Partition(list, func, leftIndexValue, rightIndexValue);
			//对左边单元进行排序
			QuickSort(list, func, leftIndex, partitionIndex - 1);
			//对右边单元进行排序
			QuickSort(list, func, partitionIndex + 1, rightIndex);
		}

		public static void QuickSortWithCompareRules(IList list, IList<Comparison<object>> compareRules)
		{
			QuickSort(list, (a, b) => CompareUtil.CompareWithRules(a, b, compareRules) < 0);
		}

		private static int Partition(IList list, Func<object, object, bool> func, int leftIndex, int rightIndex)
		{
			var pivotValue = list[rightIndex];
			int partitionIndex = leftIndex;
			object temp;
			for (int i = 0; i < rightIndex - 1; i++)
			{
				if (!func(list[i], pivotValue))
				{
					//swap list[i] and list[partition_index]
					temp = list[i];
					list[i] = list[partitionIndex];
					list[partitionIndex] = temp;
					partitionIndex++;
				}
			}

			//swap list[partition_index] and list[right]
			temp = list[partitionIndex];
			list[rightIndex] = list[partitionIndex];
			list[partitionIndex] = temp;

			return partitionIndex;
		}

		///////////////////////////////////////////////////////////////////////////
		public static void QuickSort<T>(IList<T> list, Func<T, T, bool> func, int? leftIndex = null,
			int? rightIndex = null)
		{
			int leftIndexValue = leftIndex.GetValueOrDefault(0);
			int rightIndexValue = rightIndex.GetValueOrDefault(list.Count - 1);
			if (leftIndexValue >= rightIndexValue)
				return;
			//在partition_index左边的都比index的value小，右边的都比index的value大
			var partitionIndex = Partition(list, func, leftIndexValue, rightIndexValue);
			//对左边单元进行排序
			QuickSort(list, func, leftIndex, partitionIndex - 1);
			//对右边单元进行排序
			QuickSort(list, func, partitionIndex + 1, rightIndex);
		}

		public static void QuickSortWithCompareRules<T>(IList<T> list, IList<Comparison<T>> compareRules)
		{
			QuickSort(list, (a, b) => CompareUtil.CompareWithRules(a, b, compareRules) < 0);
		}

		private static int Partition<T>(IList<T> list, Func<T, T, bool> func, int leftIndex, int rightIndex)
		{
			var pivotValue = list[rightIndex];
			int partitionIndex = leftIndex;
			T temp;
			for (int i = partitionIndex; i < rightIndex; i++)
			{
				if (func(list[i], pivotValue))
				{
					//swap list[i] and list[partition_index]
					temp = list[i];
					list[i] = list[partitionIndex];
					list[partitionIndex] = temp;
					partitionIndex++;
				}
			}

			//swap list[partition_index] and list[right]
			temp = list[partitionIndex];
			list[partitionIndex] = list[rightIndex];
			list[rightIndex] = temp;

			return partitionIndex;
		}
	}
}