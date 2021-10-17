using System;
using System.Collections.Generic;

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

        public static T[] GetArrayByIndexList<T>(T[] array, List<int> indexList)
        {
            int indexListCount = indexList.Count;
            T[] result = new T[indexListCount];
            for (int i = 0; i < indexListCount; i++)
            {
                int index = indexList[i];
                result[i] = array[index];
            }

            return result;
        }

        public static T[] GetArrayByIndexes<T>(T[] array, int[] indexes, int? indexesLength)
        {
            int indexesLengthValue = indexesLength.GetValueOrDefault(indexes.Length);
            T[] result = new T[indexesLengthValue];
            for (int i = 0; i < indexesLengthValue; i++)
            {
                int index = indexes[i];
                result[i] = array[index];
            }

            return result;
        }

        public static T[] AddRangeByIndexes<T>(T[] array1, T[] array2, int[] array2Indexes, int? indexes2Length)
        {
            int array1Length = array1.Length;
            var indexesLengthValue = indexes2Length.GetValueOrDefault(array2Indexes.Length);
            var result = array1.AddCapacity(indexesLengthValue);
            for (int i = 0; i < indexesLengthValue; i++)
            {
                int index = array2Indexes[i];
                result[array1Length + i] = array2[index];
            }

            return result;
        }

        public static T[] AddRangeByIndexList<T>(T[] array1, List<T> list2, int[] list2Indexes, int? indexesLength)
        {
            int array1Length = array1.Length;
            var indexesLengthValue = indexesLength.GetValueOrDefault(list2Indexes.Length);
            var result = array1.AddCapacity(indexesLengthValue);
            for (int i = 0; i < indexesLengthValue; i++)
            {
                int index = list2Indexes[i];
                result[array1Length + i] = list2[index];
            }

            return result;
        }
    }
}