

using System;
using System.Collections.Generic;

namespace CsCat
{
  public class QueueUtil
  {
    //双缓冲处理
    public static void HandleThenSwap<T>(Queue<T> queue1, Queue<T> queue2, Action<T> action,
      Func<T, bool> continueCondition = null)
    {
      while (queue1.Count > 0)
      {
        T element = queue1.Dequeue();
        if (continueCondition != null && continueCondition(element))
          continue;
        queue2.Enqueue(element);
        action(element);
      }

      ObjectUtil.Swap(ref queue1, ref queue2);

    }
  }
}
