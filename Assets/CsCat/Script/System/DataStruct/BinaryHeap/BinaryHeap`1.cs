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
		private readonly Comparison<T>[] _compareRules;
		private int _capacity;
		private T[] _datas;
		private int _size;
		private readonly bool _isCanRemoveSpecificData;
		private readonly Dictionary<T, int> _indexDict = new Dictionary<T, int>();

		public int Size => this._size;
		protected T this[int index]
		{
			set
			{
				this._datas[index] = value;
				if (_isCanRemoveSpecificData)
					this._indexDict[value] = index;
			}
		}


		//默认comparison返回-1，则排在上面，即默认是按c#的最小排在前面
		//是否能删除特定位置的数据
		public BinaryHeap(int? capacity, bool isCanRemoveSpecificData, params Comparison<T>[] compareRules)
		{
			this._compareRules = compareRules;
			this._capacity = capacity.GetValueOrDefault(BinaryHeapConst.Default_Capacity);
			this._isCanRemoveSpecificData = isCanRemoveSpecificData;
			_datas = new T[this._capacity];
		}

		public void Clear()
		{
			Array.Clear(_datas, 0, _size);
			_indexDict.Clear();
			_size = 0;
		}


		//需要is_need_get_index为true才能生效
		public int GetIndex(T data)
		{
			if (_indexDict.ContainsKey(data))
				return _indexDict[data];
			return -1;
		}

		//需要is_need_get_index为true才能生效
		public bool Remove(T data)
		{
			var removeIndex = GetIndex(data);
			if (removeIndex < 0)
				return false;
			return _RemoveAt(removeIndex);
		}

		private bool _RemoveAt(int toRemoveIndex)
		{
			if (toRemoveIndex < 0 || toRemoveIndex >= _size)
				return false;
			this[toRemoveIndex] = _datas[_size - 1];
			_datas[_size - 1] = default;
			if (_isCanRemoveSpecificData)
				_indexDict.Remove(_datas[_size - 1]);
			_size--;
			if (_HasParent(toRemoveIndex) && _CompareWithRules(_datas[toRemoveIndex], _GetParentData(toRemoveIndex)) < 0)
				_HeapifyUp(toRemoveIndex);
			else
				_HeapifyDown(toRemoveIndex);
			return true;
		}

		private int _CompareWithRules(T data1, T data2)
		{
			return CompareUtil.CompareWithRules(data1, data2, this._compareRules);
		}

		public void Push(T data)
		{
			_size++;
			_EnsureEnoughCapacity(_size);
			this[_size - 1] = data;
			_HeapifyUp(_size - 1);
		}

		public T Pop()
		{
			if (_size == 0)
				throw new Exception("heapCat size is 0,can not pop!!!");
			T result = _datas[0];
			this[0] = _datas[_size - 1];
			_datas[_size - 1] = default;
			if (_isCanRemoveSpecificData)
				_indexDict.Remove(_datas[_size - 1]);
			_size--;
			_HeapifyDown(0);
			return result;
		}

		void _HeapifyUp(int start_index)
		{
			int curIndex = start_index;
			while (_HasParent(curIndex) && _CompareWithRules(_GetData(curIndex), _GetParentData(curIndex)) < 0)
			{
				_Swap(curIndex, _GetParentIndex(curIndex));
				curIndex = _GetParentIndex(curIndex);
			}
		}


		void _HeapifyDown(int startIndex)
		{
			int curIndex = startIndex;
			while (_HasLeftChild(curIndex))//没有左子节点，必定就没有右子节点，所以判断是否有左子节点就可以了
			{
				int toSwapChildIndex;
				if (_HasRightChild(curIndex) && _CompareWithRules(_GetRightChildData(curIndex), _GetLeftChildData(curIndex)) < 0)
					toSwapChildIndex = _GetRightChildIndex(curIndex);
				else
					toSwapChildIndex = _GetLeftChildIndex(curIndex);
				if (_CompareWithRules(_GetData(toSwapChildIndex), _GetData(curIndex)) < 0)
				{
					_Swap(curIndex, toSwapChildIndex);
					curIndex = toSwapChildIndex;
				}
				else
					break;
			}
		}

		private void _Swap(int index1, int index2)
		{
			T tmp = _datas[index1];
			this[index1] = _datas[index2];
			this[index2] = tmp;
		}

		//确保有足够的容量
		private void _EnsureEnoughCapacity(int needSize)
		{
			if (_capacity < needSize)
			{
				_capacity = _capacity * 2;//翻倍
				T[] newDatas = new T[_capacity];
				Array.Copy(_datas, newDatas, _datas.Length);
				_datas = newDatas;
			}
		}

		private int _GetLeftChildIndex(int index)
		{
			return index * 2 + 1;
		}

		private int _GetRightChildIndex(int index)
		{
			return index * 2 + 2;
		}

		private int _GetParentIndex(int index)
		{
			return (index - 1) / 2;
		}

		private T _GetData(int index)
		{
			return _datas[index];
		}

		private T _GetLeftChildData(int index)
		{
			return _datas[_GetLeftChildIndex(index)];
		}

		private T _GetRightChildData(int index)
		{
			return _datas[_GetRightChildIndex(index)];
		}

		private T _GetParentData(int index)
		{
			return _datas[_GetParentIndex(index)];
		}

		private bool _HasLeftChild(int index)
		{
			return _GetLeftChildIndex(index) < _size;
		}

		private bool _HasRightChild(int index)
		{
			return _GetRightChildIndex(index) < _size;
		}

		private bool _HasParent(int index)
		{
			return _GetParentIndex(index) >= 0;
		}
	}
}