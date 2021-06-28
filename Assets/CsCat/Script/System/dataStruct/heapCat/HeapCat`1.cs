using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  /// <summary>
  ///   最小堆（最小值在最上面）或者最大堆(最大值在最上面) 
  ///   https://www.youtube.com/watch?v=t0Cq6tVNRBA&t=107s
  /// </summary>
  public class HeapCat<T>
  {
    private Comparison<T>[] compare_rules;
    private int capacity;
    private T[] datas;
    private int size;

    public int Size =>this.size;

    //默认comparison返回-1，则排在上面，即默认是按c#的最小排在前面
    public HeapCat(int? capacity,params Comparison<T>[] compare_rules)
    {
      this.compare_rules = compare_rules;
      this.capacity = capacity.GetValueOrDefault(HeapCatConst.Default_Capacity);
      datas = new T[this.capacity];
    }

    public bool Contains(T data)
    {
      return GetIndex(data) >= 0;
    }

    public int GetIndex(T data)
    {
      return Array.BinarySearch(datas, 0, size, data);
    }

    public bool Remove(T data)
    {
      var to_remove_index = GetIndex(data);
      if (to_remove_index < 0)
        return false;
      return RemoveAt(to_remove_index);
    }

    public bool RemoveAt(int to_remove_index)
    {
      if (to_remove_index < 0 || to_remove_index >= size)
        return false;
      Array.Copy(datas, to_remove_index + 1, datas, to_remove_index, size - to_remove_index);
      datas[size] = default;
      size--;
      return true;
    }

    public void Clear()
    {
      Array.Clear(datas,0, size);
      size = 0;
    }

    public void Push(T data)
    {
      size = size + 1;
      EnsureEnoughCapacity(size);
      datas[size - 1] = data;
      HeapifyUp();
    }

    public T Pop()
    {
      if (size == 0)
        throw new Exception("heapCat size is 0,can not pop!!!");
      T result = datas[0];
      datas[0] = datas[size - 1];
      datas[size - 1] = default(T);
      size = size - 1;
      HeapifyDown();
      return result;
    }

    void HeapifyUp()
    {
      int cur_index = size - 1;
      while (HasParent(cur_index) && CompareWithRules(GetData(cur_index), GetParentData(cur_index)) < 0)
      {
        Swap(cur_index, GetParentIndex(cur_index));
        cur_index = GetParentIndex(cur_index);
      }
    }

    private int CompareWithRules(T data1, T data2)
    {
      return SortUtil.CompareWithRules(data1, data2, this.compare_rules);
    }


    void HeapifyDown()
    {
      int cur_index = 0;
      while (HasLeftChild(cur_index))//没有左子节点，必定就没有右子节点，所以判断是否有左子节点就可以了
      {
        int to_swap_child_index;
        if (HasRightChild(cur_index) && CompareWithRules(GetRightChildData(cur_index),GetLeftChildData(cur_index)) < 0)
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
      datas[index1] = datas[index2];
      datas[index2] = tmp;
    }

    //确保有足够的容量
    private void EnsureEnoughCapacity(int need_size)
    {
      if (capacity < need_size)
      {
        capacity = capacity * 2;//翻倍
        T[] new_datas = new T[capacity];
        Array.Copy(datas, new_datas,datas.Length);
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
      return GetLeftChildIndex(index) < size;
    }

    private bool HasParent(int index)
    {
      return GetParentIndex(index) >= 0 ;
    }
  }
}