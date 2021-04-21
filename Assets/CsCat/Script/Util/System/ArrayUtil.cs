using System;

namespace CsCat
{
  public class ArrayUtil
  {
    public static Array AddFirst(Array source_array, params object[] to_adds)
    {
      var element_type = source_array != null ? source_array.GetType().GetElementType() : to_adds.GetType().GetElementType();
      var source_array_length = source_array == null ? 0 : source_array.Length;
      var array = Array.CreateInstance(element_type, source_array_length + 1);
      if (source_array != null && source_array.Length > 0)
        Array.Copy(source_array, 0, array, to_adds.Length, source_array_length);
      Array.Copy(to_adds, 0, array, 0, to_adds.Length);
      return array;
    }
    public static T[] AddFirst<T>(Array source_array, params object[] to_adds)
    {
      return AddFirst(source_array, to_adds).ToArray<T>();
    }
    public static Array AddLast(Array source_array, params object[] to_adds)
    {
      var element_type = source_array != null ? source_array.GetType().GetElementType() : to_adds.GetType().GetElementType();
      var source_array_length = source_array == null ? 0 : source_array.Length;
      var array = Array.CreateInstance(element_type, source_array_length + 1);
      if (source_array != null && source_array.Length > 0)
        Array.Copy(source_array, array, source_array_length);
      Array.Copy(to_adds, 0, array, source_array_length, to_adds.Length);
      return array;
    }
    public static T[] AddLast<T>(Array source_array, params object[] to_adds)
    {
      return AddLast(source_array, to_adds).ToArray<T>();
    }

    public static Array Remove(Array source_array, object o)
    {
      var element_type = source_array != null ? source_array.GetType().GetElementType() : o.GetType();
      var source_array_length = source_array == null ? 0 : source_array.Length;
      if (source_array_length == null || source_array_length == 0)
        return source_array;
      int to_remove_index = -1;
      for (int i = 0; i < source_array_length; i++)
      {
        if (source_array.GetValue(i).Equals(o))
        {
          to_remove_index = i;
          break;
        }
      }

      if (to_remove_index == -1)
        return source_array;


      var array = Array.CreateInstance(element_type, source_array_length - 1);
      if (to_remove_index != 0)
        Array.Copy(source_array, 0, array, 0, to_remove_index);
      if (to_remove_index != source_array_length - 1)
        Array.Copy(source_array, to_remove_index + 1, array, to_remove_index, source_array_length - to_remove_index - 1);
      return array;
    }

    public static T[] Remove<T>(Array source_array, object o)
    {
      return Remove(source_array, o).ToArray<T>();
    }

    public static Array RemoveAt(Array source_array, int index)
    {
      var element_type = source_array.GetType().GetElementType();
      var source_array_length = source_array == null ? 0 : source_array.Length;
      if (source_array_length == null || source_array_length == 0)
        return source_array;
      int to_remove_index = index;
      if (to_remove_index < 0 || to_remove_index >= source_array_length)
      {
        LogCat.LogError("index out of boundary");
        return source_array;
      }
      var array = Array.CreateInstance(element_type, source_array_length - 1);
      if (to_remove_index != 0)
        Array.Copy(source_array, 0, array, 0, to_remove_index);
      if (to_remove_index != source_array_length - 1)
        Array.Copy(source_array, to_remove_index + 1, array, to_remove_index, source_array_length - to_remove_index - 1);
      return array;
    }

    public static T[] RemoveAt<T>(Array source_array, int index)
    {
      return RemoveAt(source_array, index).ToArray<T>();
    }



  }
}