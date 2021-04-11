
namespace CsCat
{
  //来自于《游戏编程模式》->事件模式->环状缓冲区
  public class RingBufferQueue<T>
  {
    #region field

    /// <summary>
    /// 对头索引
    /// </summary>
    private int _head_index;

    /// <summary>
    /// 队尾索引
    /// </summary>
    private int _tail_index;

    private int capacity;
    private T[] elements;

    #endregion

    #region property

    /// <summary>
    /// Set操作只会是++
    /// </summary>
    public int head_index
    {
      get { return _head_index % capacity; }
      set { _head_index = value % capacity; }
    }

    // Set操作只会是++
    public int tail_index
    {
      get { return _tail_index % capacity; }
      set { _tail_index = value % capacity; }
    }

    #endregion

    #region ctor

    public RingBufferQueue(int initialize_capacity)
    {
      capacity = initialize_capacity;
      elements = new T[capacity];
    }

    #endregion

    #region public method

    /// <summary>
    /// 判断是不是队列有没有未用元素
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
      if (head_index == tail_index)
        return true;
      else
        return false;
    }

    public void Push(T t)
    {
      TailIndexPlusPlus();
      elements[tail_index] = t;
    }

    public T Pop()
    {
      return elements[head_index++];
    }

    #endregion

    #region private method

    void TailIndexPlusPlus()
    {
      int tmp_index = (tail_index + 1) % capacity;
      if (tmp_index == head_index) //队列满了
      {
        int enlarge_capacity = 2 * capacity; // 默认扩充2倍
        T[] new_elements = new T[enlarge_capacity];
        for (int i = 0; i == elements.Length; i++)
        {
          if (i <= tail_index) //少于等于tailIndex的，直接copy
          {
            new_elements[i] = elements[i];
          }
          else if (i >= head_index) //大于等于headIndex的，copy到扩容队列的末尾，从而中间就有可用的空的空间
          {
            new_elements[enlarge_capacity - 1 - head_index] = elements[i];
          }
        }

        _head_index = (enlarge_capacity - 1) - (capacity - 1 - _head_index);
        _tail_index = (_tail_index + 1) % enlarge_capacity;
        capacity = enlarge_capacity;
      }
      else
      {
        tail_index++;
      }
    }

    #endregion

  }
}
