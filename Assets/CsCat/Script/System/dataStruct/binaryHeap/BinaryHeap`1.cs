#region Copyright

// ******************************************************************************************
//
// 							SimplePath, Copyright © 2011, Alex Kring
//
// ******************************************************************************************
// NOTE: This class was NOT created by me (Alex Kring); someone else created it through some
// free license, but I can't remember where I got the code from :[. Whoever made it, thank you!

#endregion

using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  /// <summary>
  ///   最小二叉堆（最小值在最上面） 注意一定要设置Capacity的数量，否则会出现bug
  ///   A binary heap, useful for sorting data and priority queues.
  /// </summary>
  public class BinaryHeap<T> : ICollection<T> where T : IComparable<T>
  {
    #region field

    private const int DEFAULT_SIZE = 4;
    private T[] _data = new T[DEFAULT_SIZE];
    private int _capacity = DEFAULT_SIZE;
    private bool _sorted;

    #endregion

    #region property

    /// <summary>
    ///   Gets the number of values in the heap.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    ///   Gets or sets the capacity of the heap.
    /// </summary>
    public int Capacity
    {
      get => _capacity;
      set
      {
        var previousCapacity = _capacity;
        _capacity = Math.Max(value, Count);
        if (_capacity != previousCapacity)
        {
          var temp = new T[_capacity];
          Array.Copy(_data, temp, Count);
          _data = temp;
        }
      }
    }

    /// <summary>
    ///   Gets whether or not the binary heap is readonly.
    /// </summary>
    public bool IsReadOnly => false;

    #endregion

    #region ctor

    /// <summary>
    ///   Creates a new binary heap.
    /// </summary>
    private BinaryHeap(T[] data, int count)
    {
      Capacity = count;
      Count = count;
      Array.Copy(data, _data, count);
    }

    /// <summary>
    ///   Creates a new binary heap.
    /// </summary>
    public BinaryHeap()
    {
    }

    #endregion

    #region static method

    private static int Parent(int index)
    //helper function that calculates the parent of a node
    {
      return (index - 1) >> 1;
    }

    private static int Child1(int index)
    //helper function that calculates the first child of a node
    {
      return (index << 1) + 1;
    }

    private static int Child2(int index)
    //helper function that calculates the second child of a node
    {
      return (index << 1) + 2;
    }

    #endregion

    #region public method

    /// <summary>
    ///   Gets the first value in the heap without removing it.
    /// </summary>
    /// <returns>The lowest value of type TValue.</returns>
    public T Peek()
    {
      return _data[0];
    }

    /// <summary>
    ///   Removes all items from the heap.
    /// </summary>
    public void Clear()
    {
      Count = 0;
      _data = new T[_capacity];
    }

    /// <summary>
    ///   Adds a key and value to the heap.
    /// </summary>
    /// <param name="item">The item to add to the heap.</param>
    public void Add(T item)
    {
      //if (Count >= Capacity) return;
      if (Count == _capacity)
      {
        Capacity *= 2;
      }

      _data[Count] = item;
      UpHeap();
      Count++;
    }

    /// <summary>
    ///   Removes and returns the first item in the heap.
    /// </summary>
    /// <returns>The next value in the heap.</returns>
    public T Remove()
    {
      if (Count == 0) throw new InvalidOperationException("Cannot remove item, heap is empty.");
      var v = _data[0];
      Count--;
      _data[0] = _data[Count];
      _data[Count] = default; //Clears the Last Node
      DownHeap();
      return v;
    }

    /// <summary>
    ///   Creates a new instance of an identical binary heap.
    /// </summary>
    /// <returns>A BinaryHeap.</returns>
    public BinaryHeap<T> Copy()
    {
      return new BinaryHeap<T>(_data, Count);
    }

    /// <summary>
    ///   Gets an enumerator for the binary heap.
    /// </summary>
    /// <returns>An IEnumerator of type T.</returns>
    public IEnumerator<T> GetEnumerator()
    {
      EnsureSort();
      for (var i = 0; i < Count; i++) yield return _data[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    /// <summary>
    ///   Checks to see if the binary heap contains the specified item.
    /// </summary>
    /// <param name="item">The item to search the binary heap for.</param>
    /// <returns>A boolean, true if binary heap contains item.</returns>
    public bool Contains(T item)
    {
      EnsureSort();
      return Array.BinarySearch(_data, 0, Count, item) >= 0;
    }

    /// <summary>
    ///   Copies the binary heap to an array at the specified index.
    /// </summary>
    /// <param name="array">One dimensional array that is the destination of the copied elements.</param>
    /// <param name="arrayIndex">The zero-based index at which copying begins.</param>
    public void CopyTo(T[] array, int arrayIndex)
    {
      EnsureSort();
      //edit by czq
      //Array.Copy(_data, array, _count);
      _data.CopyTo(array, arrayIndex);
    }

    /// <summary>
    ///   Removes an item from the binary heap. This utilizes the type T's Comparer and will not remove duplicates.
    /// </summary>
    /// <param name="item">The item to be removed.</param>
    /// <returns>Boolean true if the item was removed.</returns>
    public bool Remove(T item)
    {
      EnsureSort();
      var i = Array.BinarySearch(_data, 0, Count, item);
      if (i < 0) return false;
      Array.Copy(_data, i + 1, _data, i, Count - i);
      _data[Count] = default;
      Count--;
      return true;
    }

    #endregion

    #region private method

    private void UpHeap()
    //helper function that performs up-heap bubbling
    {
      _sorted = false;
      var p = Count;
      var item = _data[p];
      var par = Parent(p);
      while (par > -1 && item.CompareTo(_data[par]) < 0)
      {
        _data[p] = _data[par]; //Swap nodes
        p = par;
        par = Parent(p);
      }

      _data[p] = item;
    }

    private void DownHeap()
    //helper function that performs down-heap bubbling
    {
      _sorted = false;
      int n;
      var p = 0;
      var item = _data[p];
      while (true)
      {
        var ch1 = Child1(p);
        if (ch1 >= Count) break;
        var ch2 = Child2(p);
        if (ch2 >= Count)
          n = ch1;
        else
          n = _data[ch1].CompareTo(_data[ch2]) < 0 ? ch1 : ch2;
        if (item.CompareTo(_data[n]) > 0)
        {
          _data[p] = _data[n]; //Swap nodes
          p = n;
        }
        else
        {
          break;
        }
      }

      _data[p] = item;
    }

    private void EnsureSort()
    {
      if (_sorted) return;
      Array.Sort(_data, 0, Count);
      _sorted = true;
    }

    #endregion
  }
}