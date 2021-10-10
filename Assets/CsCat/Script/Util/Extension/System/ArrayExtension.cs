using System;

namespace CsCat
{
    public static class ArrayExtension
    {
        public static Array Resize_Array(this Array self, int length)
        {
            Type elementType = self.GetType().GetElementType();
            Array newArray = Array.CreateInstance(elementType, length);
            Array.Copy(self, 0, newArray, 0, Math.Min(self.Length, length));
            return newArray;
        }

        public static Array Insert_Array(this Array self, int index, object value)
        {
            int newArrayLength = index < self.Length ? self.Length + 1 : index + 1;

            Type elementType = self.GetType().GetElementType();
            Array newArray = Array.CreateInstance(elementType, newArrayLength);
            Array.Copy(self, 0, newArray, 0, Math.Min(newArrayLength, self.Length));
            newArray.SetValue(value, index);
            if (index < self.Length)
                Array.Copy(self, index, newArray, index + 1, self.Length - index);
            return newArray;
        }

        public static Array RemoveAt_Array(this Array self, int index)
        {
            Type elementType = self.GetType().GetElementType();
            Array newArray = Array.CreateInstance(elementType, self.Length - 1);
            Array.Copy(self, 0, newArray, 0, index);
            Array.Copy(self, index + 1, newArray, index, self.Length - index - 1);
            return newArray;
        }

        
    }
}