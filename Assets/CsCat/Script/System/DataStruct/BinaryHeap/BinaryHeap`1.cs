using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	/// <summary>
	///   最小堆（最小值在最上面）或者最大堆(最大值在最上面) 
	///   https://www.youtube.com/watch?v=t0Cq6tVNRBA&t=107s
	///   deletable heap: http://www.mathcs.emory.edu/~cheung/Courses/171/Syllabus/9-BinTree/heap-delete.html
	/// </summary>
	public class BinaryHeap<T>
	{
		private Comparison<T>[] compareRules;
		private int capacity;
		private T[] datas;
		private int size;
		private bool isCanRemoveSpecificData;
		private Dictionary<T, int> indexDict = new Dictionary<T, int>();

		public int Size => this.size;
		protected T this[int index]
		{
			set
			{
				this.datas[index] = value;
				if (isCanRemoveSpecificData)
					this.indexDict[value] = index;
			}
		}


		//默认comparison返回-1，则排在上面，即默认是按c#的最小排在前面
		//是否能删除特定位置的数据
		public BinaryHeap(int? capacity, bool isCanRemoveSpecificData, params Comparison<T>[] compareRules)
		{
			this.compareRules = compareRules;
			this.capacity = capacity.GetValueOrDefault(BinaryHeapConst.Default_Capacity);
			this.isCanRemoveSpecificData = isCanRemoveSpecificData;
			datas = new T[this.capacity];
		}

		public void Clear()
		{
			Array.Clear(datas, 0, size);
			indexDict.Clear();
			size = 0;
		}


		//需要is_need_get_index为true才能生效
		public int GetIndex(T data)
		{
			if (indexDict.ContainsKey(data))
				return indexDict[data];
			return -1;
		}

		//需要is_need_get_index为true才能生效
		public bool Remove(T data)
		{
			var removeIndex = GetIndex(data);
			if (removeIndex < 0)
				return false;
			return RemoveAt(removeIndex);
		}

		private bool RemoveAt(int toRemoveIndex)
		{
			if (toRemoveIndex < 0 || toRemoveIndex >= size)
				return false;
			this[toRemoveIndex] = datas[size - 1];
			datas[size - 1] = default;
			if (isCanRemoveSpecificData)
				indexDict.Remove(datas[size - 1]);
			size--;
			if (HasParent(toRemoveIndex) && CompareWithRules(datas[toRemoveIndex], GetParentData(toRemoveIndex)) < 0)
				HeapifyUp(toRemoveIndex);
			else
				HeapifyDown(toRemoveIndex);
			return true;
		}

		private int CompareWithRules(T data1, T data2)
		{
			return CompareUtil.CompareWithRules(data1, data2, this.compareRules);
		}

		public void Push(T data)
		{
			size++;
			EnsureEnoughCapacity(size);
			this[size - 1] = data;
			HeapifyUp(size - 1);
		}

		public T Pop()
		{
			if (size == 0)
				throw new Exception("heapCat size is 0,can not pop!!!");
			T result = datas[0];
			this[0] = datas[size - 1];
			datas[size - 1] = default;
			if (isCanRemoveSpecificData)
				indexDict.Remove(datas[size - 1]);
			size--;
			HeapifyDown(0);
			return result;
		}

		void HeapifyUp(int start_index)
		{
			int curIndex = start_index;
			while (HasParent(curIndex) && CompareWithRules(GetData(curIndex), GetParentData(curIndex)) < 0)
			{
				Swap(curIndex, GetParentIndex(curIndex));
				curIndex = GetParentIndex(curIndex);
			}
		}


		void HeapifyDown(int startIndex)
		{
			int curIndex = startIndex;
			while (HasLeftChild(curIndex))//没有左子节点，必定就没有右子节点，所以判断是否有左子节点就可以了
			{
				int toSwapChildIndex;
				if (HasRightChild(curIndex) && CompareWithRules(GetRightChildData(curIndex), GetLeftChildData(curIndex)) < 0)
					toSwapChildIndex = GetRightChildIndex(curIndex);
				else
					toSwapChildIndex = GetLeftChildIndex(curIndex);
				if (CompareWithRules(GetData(toSwapChildIndex), GetData(curIndex)) < 0)
				{
					Swap(curIndex, toSwapChildIndex);
					curIndex = toSwapChildIndex;
				}
				else
					break;
			}
		}

		private void Swap(int index1, int index2)
		{
			T tmp = datas[index1];
			this[index1] = datas[index2];
			this[index2] = tmp;
		}

		//确保有足够的容量
		private void EnsureEnoughCapacity(int needSize)
		{
			if (capacity < needSize)
			{
				capacity = capacity * 2;//翻倍
				T[] newDatas = new T[capacity];
				Array.Copy(datas, newDatas, datas.Length);
				datas = newDatas;
			}
		}

		private int GetLeftChildIndex(int index)
		{
			return index * 2 + 1;
		}

		private int GetRightChildIndex(int index)
		{
			return index * 2 + 2;
		}

		private int GetParentIndex(int index)
		{
			return (index - 1) / 2;
		}

		private T GetData(int index)
		{
			return datas[index];
		}

		private T GetLeftChildData(int index)
		{
			return datas[GetLeftChildIndex(index)];
		}

		private T GetRightChildData(int index)
		{
			return datas[GetRightChildIndex(index)];
		}

		private T GetParentData(int index)
		{
			return datas[GetParentIndex(index)];
		}

		private bool HasLeftChild(int index)
		{
			return GetLeftChildIndex(index) < size;
		}

		private bool HasRightChild(int index)
		{
			return GetRightChildIndex(index) < size;
		}

		private bool HasParent(int index)
		{
			return GetParentIndex(index) >= 0;
		}
	}
}