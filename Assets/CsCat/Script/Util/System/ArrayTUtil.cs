using System;

namespace CsCat
{
    public class ArrayTUtil
    {
        public static void Swap<T>(T[] array1, int index1, T[] array2, int index2)
        {
            var c = array1[index1];
            array1[index1] = array2[index2];
            array2[index2] = c;
        }
    }
}