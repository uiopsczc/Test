using System;

namespace CsCat
{
  public static class EnumExtension
  {

    /// <summary>
    /// 用于每个枚举都是左移的枚举类型
    /// </summary>
    /// <param name="self"></param>
    /// <param name="to_be_contained"></param>
    /// <returns></returns>
    public static bool Contains(this Enum self, Enum to_be_contained)
    {
      int container_int = self.ToInt();
      int to_be_contained_int = to_be_contained.ToInt();
      //只要包含，一定有一位为1，只要不包含，一定全部位都是0
      return (container_int & to_be_contained_int) > 0;
    }

    public static int ToInt(this Enum self)
    {
      return Convert.ToInt32(self);
    }

    //转为不同的enum
    public static T ToEnum<T>(this Enum self)
    {
      if (!typeof(T).GetType().IsEnum)
        throw new ArgumentException("T must be enum Type");
      int value = self.ToInt();
      return (T) value.ToEnum<T>();
    }
  }
}