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
		private Comparison<T>[] compare_rules;
		private int capacity;
		private T[] datas;
		private int size;
		private bool is_can_remove_specific_data;
		private Dictionary<T, int> index_dict = new Dictionary<T, int>();

		public int Size => this.size;
		protected T this[int index]
		{
			set
			{
				this.datas[index] = value;
				if (is_can_remove_specific_data)
					this.index_dict[value] = index;
			}
		}


		//默认comparison返回-1，则排在上面，即默认是按c#的最小排在前面
		//是否能删除特定位置的数据
		public BinaryHeap(int? capacity, bool is_can_remove_specific_data, params Comparison<T>[] compare_rules)
		{
			this.compare_rules = compare_rules;
			this.capacity = capacity.GetValueOrDefault(BinaryHeapConst.Default_Capacity);
			this.is_can_remove_specific_data = is_can_remove_specific_data;
			datas = new T[this.capacity];
		}

		public void Clear()
		{
			Array.Clear(datas, 0, size);
			index_dict.Clear();
			size = 0;
		}


		//需要is_need_get_index为true才能生效
		public int GetIndex(T data)
		{
			if (index_dict.ContainsKey(data))
				return index_dict[data];
			return -1;
		}

		//需要is_need_get_index为true才能生效
		public bool Remove(T data)
		{
			var to_remove_index = GetIndex(data);
			if (to_remove_index < 0)
				return false;
			return RemoveAt(to_remove_index);
		}

		private bool RemoveAt(int to_remove_index)
		{
			if (to_remove_index < 0 || to_remove_index >= size)
				return false;
			this[to_remove_index] = datas[size - 1];
			datas[size - 1] = default;
			if (is_can_remove_specific_data)
				index_dict.Remove(datas[size - 1]);
			size--;
			if (HasParent(to_remove_index) && CompareWithRules(datas[to_remove_index], GetParentData(to_remove_index)) < 0)
				HeapifyUp(to_remove_index);
			else
				HeapifyDown(to_remove_index);
			return true;
		}

		private int CompareWithRules(T data1, T data2)
		{
			return CompareUtil.CompareWithRules(data1, data2, this.compare_rules);
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
			if (is_can_remove_specific_data)
				index_dict.Remove(datas[size - 1]);
			size--;
			HeapifyDown(0);
			return result;
		}

		void HeapifyUp(int start_index)
		{
			int cur_index = start_index;
			while (HasParent(cur_index) && CompareWithRules(GetData(cur_index), GetParentData(cur_index)) < 0)
			{
				Swap(cur_index, GetParentIndex(cur_index));
				cur_index = GetParentIndex(cur_index);
			}
		}


		void HeapifyDown(int start_index)
		{
			int cur_index = start_index;
			while (HasLeftChild(cur_index))//没有左子节点，必定就没有右子节点，所以判断是否有左子节点就可以了
			{
				int to_swap_child_index;
				if (HasRightChild(cur_index) && CompareWithRules(GetRightChildData(cur_index), GetLeftChildData(cur_index)) < 0)
					to_swap_child_index = GetRightChildIndex(cur_index);
				else
					to_swap_child_index = GetLeftChildIndex(cur_index);
				if (CompareWithRules(GetData(to_swap_child_index), GetData(cur_index)) < 0)
				{
					Swap(cur_index, to_swap_child_index);
					cur_index = to_swap_child_index;
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
		private void EnsureEnoughCapacity(int need_size)
		{
			if (capacity < need_size)
			{
				capacity = capacity * 2;//翻倍
				T[] new_datas = new T[capacity];
				Array.Copy(datas, new_datas, datas.Length);
				datas = new_datas;
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