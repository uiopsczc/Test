using System;

namespace CsCat
{
    public static class EnumExtension
    {
        /// <summary>
        /// 用于每个枚举都是左移的枚举类型
        /// </summary>
        /// <param name="self"></param>
        /// <param name="toBeContained"></param>
        /// <returns></returns>
        public static bool Contains(this Enum self, Enum toBeContained)
        {
            int containerInt = self.ToInt();
            int toBeContainedInt = toBeContained.ToInt();
            //只要包含，一定有一位为1，只要不包含，一定全部位都是0
            return (containerInt & toBeContainedInt) > 0;
        }

        public static int ToInt(this Enum self)
        {
            return Convert.ToInt32(self);
        }

        //转为不同的enum
        public static T ToEnum<T>(this Enum self)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be enum Type");
            var value = self.ToInt();
            return value.ToEnum<T>();
        }
    }
}