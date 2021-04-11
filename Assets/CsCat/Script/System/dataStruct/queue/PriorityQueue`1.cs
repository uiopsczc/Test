using System;
using System.Collections.Generic;

namespace CsCat
{
  /// <summary>
  /// 优先权队列  优先权最大，越放在前面
  /// IComparer 用于比较大小  里面的每个元素都是IComparer<T> 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class PriorityQueue<T>
  {
    #region field

    IComparer<T> comparer;
    T[] heap;

    #endregion

    #region property

    public int Count { get; private set; }

    #endregion

    #region ctor

    public PriorityQueue(int capacity, IComparer<T> comparer)
    {
      this.comparer = (comparer == null) ? Comparer<T>.Default : comparer;
      this.heap = new T[capacity];
    }

    public PriorityQueue(IComparer<T> comparer) : this(16, comparer)
    {
    }

    public PriorityQueue(int capacity) : this(capacity, null)
    {
    }

    public PriorityQueue() : this(null)
    {
    }

    #endregion

    #region public method

    public void Push(T v)
    {
      if (Count >= heap.Length)
        Array.Resize(ref heap, Count * 2);
      heap[Count] = v;
      SiftUp(Count++);
    }

    public T Pop()
    {
      var v = Top();
      heap[0] = heap[--Count];
      if (Count > 0) SiftDown(0);
      return v;
    }

    public T Top()
    {
      if (Count > 0)
        return heap[0];
      throw new InvalidOperationException("优先队列为空");
    }

    #endregion

    #region private method

    void SiftUp(int n)
    {
      var v = heap[n];
      for (var n2 = n / 2; n > 0 && comparer.Compare(v, heap[n2]) > 0; n = n2, n2 /= 2)
        heap[n] = heap[n2];
      heap[n] = v;
    }

    void SiftDown(int n)
    {
      var v = heap[n];
      for (var n2 = n * 2; n2 < Count; n = n2, n2 *= 2)
      {
        if (n2 + 1 < Count && comparer.Compare(heap[n2 + 1], heap[n2]) > 0)
          n2++;
        if (comparer.Compare(v, heap[n2]) >= 0)
          break;
        heap[n] = heap[n2];
      }

      heap[n] = v;
    }

    #endregion

  }
}
