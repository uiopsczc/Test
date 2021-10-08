using System;
using System.ComponentModel;
using System.Reflection;

namespace CsCat
{
    public static partial class TypeExtension
    {
        /// <summary>
        ///   type是否是subType的父类
        /// </summary>
        public static bool IsSuperTypeOf(this Type self, Type subType)
        {
            //      if (sub_type == null || self == null)
            //        return false;
            //      if (!sub_type.IsInterface && !self.IsInterface)
            //      {
            //        while (sub_type != null)
            //        {
            //          if (sub_type == self)
            //            return true;
            //          sub_type = sub_type.BaseType;
            //        }
            //      }
            //      else if (self.IsInterface)
            //      {
            //        if (sub_type == self)
            //          return true;
            //        var ts = sub_type.GetInterfaces();
            //        for (var i = 0; i < ts.Length; i++)
            //          if (self.IsSuperOf(ts[i]))
            //            return true;
            //        if (!sub_type.IsInterface)
            //          return self.IsSuperOf(sub_type.BaseType);
            //      }
            //
            //      return false;
            return self.IsAssignableFrom(subType);
        }

        /// <summary>
        ///   type是否是parentType的子类
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parentType"></param>
        /// <returns></returns>
        public static bool IsSubTypeOf(this Type self, Type parentType)
        {
            return parentType.IsAssignableFrom(self);
        }


        public static string GetDescription(this Type self, string fieldName)
        {
            var memberInfo = self.GetMember(fieldName);
            var attributes =
                (DescriptionAttribute[]) memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes[0].Description;
        }

        public static string GetDescription(this Type self, int enumValue)
        {
            var memberInfo = self.GetMember(Enum.GetName(self, enumValue));
            var attributes =
                (DescriptionAttribute[]) memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes[0].Description;
        }


        public static string GetLastName(this Type self)
        {
            var type = self.ToString();
            var splitIndex = type.IndexOf(CharConst.Char_Tilde);
            if (splitIndex != -1) type = type.Substring(0, splitIndex);
            var index = type.LastIndexOf(CharConst.Char_Dot);
            if (index != -1) type = type.Substring(index + 1);

            return type;
        }


        public static object DefaultValue(this Type self)
        {
            return self.IsValueType ? Activator.CreateInstance(self) : null;
        }


        #region 反射相关

        public static MethodInfo GetMethodInfo2(this Type self, string methodName,
            BindingFlags bindingFlags = BindingFlagsConst.All)
        {
            return ReflectionUtil.GetMethodInfo2(self, methodName, bindingFlags);
        }

        public static MethodInfo GetMethodInfo(this Type self, string methodName,
            BindingFlags bindingFlags = BindingFlagsConst.All, params Type[] sourceParameterTypes)
        {
            return ReflectionUtil.GetMethodInfo(self, methodName, bindingFlags, null, sourceParameterTypes);
        }

        public static MethodInfo GetGenericMethodInfo2(this Type self, string methodName, Type[] genericTypes,
            BindingFlags bindingFlags = BindingFlagsConst.All)
        {
            return ReflectionUtil.GetGenericMethodInfo2(self, methodName, genericTypes, bindingFlags);
        }

        public static MethodInfo GetGenericMethodInfo(this Type self, string methodName, Type[] genericTypes,
            BindingFlags bindingFlags = BindingFlagsConst.All, params Type[] sourceParameterTypes)
        {
            return ReflectionUtil.GetGenericMethodInfo(self, methodName, genericTypes, bindingFlags, null,
                sourceParameterTypes);
        }

        //////////////////////////ExtensionMethod//////////////////////////////
        public static MethodInfo GetExtensionMethodInfo2(this Type self, string methodName)
        {
            return ExtensionUtil.GetExtensionMethodInfo2(self, methodName);
        }

        public static MethodInfo GetExtensionMethodInfo(this Type self, string methodName,
            params Type[] sourceParameterTypes)
        {
            return ExtensionUtil.GetExtensionMethodInfo(self, methodName, sourceParameterTypes);
        }

        public static MethodInfo GetExtensionGenericMethodInfo2(this Type self, string methodName,
            Type[] genericTypes)
        {
            return ExtensionUtil.GetExtensionGenericMethodInfo2(self, methodName, genericTypes);
        }

        public static MethodInfo GetExtensionGenericMethodInfo(this Type self, string methodName, Type[] genericTypes,
            params Type[] sourceParameterTypes)
        {
            return ExtensionUtil.GetExtensionGenericMethodInfo(self, methodName, genericTypes, sourceParameterTypes);
        }

        public static PropertyInfo GetPropertyInfo(this Type self, string propertyName,
            BindingFlags bindingFlags = BindingFlagsConst.All)
        {
            return ReflectionUtil.GetPropertyInfo(self, propertyName, bindingFlags);
        }

        public static FieldInfo GetFieldInfo(this Type self, string fieldName,
            BindingFlags bindingFlags = BindingFlagsConst.All)
        {
            return ReflectionUtil.GetFieldInfo(self, fieldName, bindingFlags);
        }

        public static object CreateInstance(this Type self, params object[] args)
        {
            var obj = Activator.CreateInstance(self, args); //根据类型创建实例
            return obj; //类型转换并返回
        }

        public static T CreateInstance<T>(this Type self, params object[] args)
        {
            return (T) CreateInstance(self, args);
        }

        #endregion
    }
}