using System.Collections;
using System.Collections.Generic;

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
            int curIndex = 0;
            while (curIndex <= index)
            {
                self.MoveNext();
                curIndex++;
            }

            return self;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="curIndex">从-1开始</param>
        /// <returns></returns>
        public static bool MoveNext(this IEnumerator self, ref int curIndex)
        {
            curIndex++;
            var result = self.MoveNext();
            return result;
        }

        public static EnumeratorScope<T> Scope<T>(this IEnumerator<T> self)
        {
            return new EnumeratorScope<T>(self);
        }
    }
}