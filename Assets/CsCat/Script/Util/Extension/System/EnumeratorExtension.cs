using System.Collections;

namespace CsCat
{
  public static class EnumeratorExtension
  {

    /// <summary>
    /// 去到指定index
    /// </summary>
    /// <param name="self"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static IEnumerator GoToIndex(this IEnumerator self, int index)
    {
      self.Reset();
      int cur_index = 0;
      while (cur_index <= index)
      {
        self.MoveNext();
        cur_index++;
      }

      return self;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="cur_index">从-1开始</param>
    /// <returns></returns>
    public static bool MoveNext(this IEnumerator self, ref int cur_index)
    {
      cur_index++;
      bool result = self.MoveNext();
      return result;
    }


  }
}