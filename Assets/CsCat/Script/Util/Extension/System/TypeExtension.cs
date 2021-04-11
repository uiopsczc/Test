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
    public static bool IsSuperTypeOf(this Type self, Type sub_type)
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
      return self.IsAssignableFrom(sub_type);
    }

    /// <summary>
    ///   type是否是parentType的子类
    /// </summary>
    /// <param name="self"></param>
    /// <param name="parent_type"></param>
    /// <returns></returns>
    public static bool IsSubTypeOf(this Type self, Type parent_type)
    {
      return parent_type.IsAssignableFrom(self);
    }


    public static string GetDescription(this Type self, string field_name)
    {
      var memberInfo = self.GetMember(field_name);
      var attributes = (DescriptionAttribute[]) memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
      return attributes == null ? null : attributes[0].Description;
    }

    public static string GetDescription(this Type self, int enum_value)
    {
      var memberInfo = self.GetMember(Enum.GetName(self, enum_value));
      var attributes = (DescriptionAttribute[]) memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
      return attributes == null ? null : attributes[0].Description;
    }


    public static string GetLastName(this Type self)
    {
      var type = self.ToString();
      var spe_index = type.IndexOf("`");
      if (spe_index != -1) type = type.Substring(0, spe_index);
      var index = type.LastIndexOf('.');
      if (index != -1) type = type.Substring(index + 1);

      return type;
    }


    public static object DefaultValue(this Type self)
    {
      if (self.IsValueType)
        return Activator.CreateInstance(self);

      return null;
    }


    #region 反射相关
    public static MethodInfo GetMethodInfo2(this Type self, string method_name,
      BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      return ReflectionUtil.GetMethodInfo2(self, method_name, bindingFlags);
    }

    public static MethodInfo GetMethodInfo(this Type self, string method_name,
      BindingFlags bindingFlags = BindingFlagsConst.All, params Type[] sourceParameterTypes)
    {
      return ReflectionUtil.GetMethodInfo( self,method_name, bindingFlags,null, sourceParameterTypes);
    }

    public static MethodInfo GetGenericMethodInfo2(this Type self, string method_name, Type[] generic_types,

      BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      return ReflectionUtil.GetGenericMethodInfo2(self, method_name, generic_types, bindingFlags);
    }

    public static MethodInfo GetGenericMethodInfo(this Type self, string method_name, Type[] generic_types,

      BindingFlags bindingFlags = BindingFlagsConst.All, params Type[] sourceParameterTypes)
    {
      return ReflectionUtil.GetGenericMethodInfo(self, method_name, generic_types, bindingFlags, null, sourceParameterTypes);
    }
    //////////////////////////ExtensionMethod//////////////////////////////
    public static MethodInfo GetExtensionMethodInfo2(this Type self, string method_name)
    {
      return ExtensionUtil.GetExtensionMethodInfo2(self, method_name);
    }

    public static MethodInfo GetExtensionMethodInfo(this Type self, string method_name, params Type[] sourceParameterTypes)
    {
      return ExtensionUtil.GetExtensionMethodInfo(self, method_name, sourceParameterTypes);
    }

    public static MethodInfo GetExtensionGenericMethodInfo2(this Type self, string method_name, Type[] generic_types)
    {
      return ExtensionUtil.GetExtensionGenericMethodInfo2(self, method_name, generic_types);
    }

    public static MethodInfo GetExtensionGenericMethodInfo(this Type self, string method_name, Type[] generic_types, params Type[] sourceParameterTypes)
    {
      return ExtensionUtil.GetExtensionGenericMethodInfo(self, method_name, generic_types, sourceParameterTypes);
    }

    public static PropertyInfo GetPropertyInfo(this Type self, string property_name,
      BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      return ReflectionUtil.GetPropertyInfo(self, property_name, bindingFlags);
    }

    public static FieldInfo GetFieldInfo(this Type self, string field_name,
      BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      return ReflectionUtil.GetFieldInfo(self, field_name, bindingFlags);
    }

    public static object CreateInstance(this Type self, params object[] args)
    {
      var obj = Activator.CreateInstance(self, args); //根据类型创建实例
      return obj; //类型转换并返回
    }
    public static T CreateInstance<T>(this Type self, params object[] args)
    {
      return (T)CreateInstance(self, args);
    }

    #endregion
  }
}