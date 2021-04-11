using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace CsCat
{
  public partial class ExtensionUtil
  {
    public static MethodInfo[] GetExtensionMethodInfos(Type extended_type, Assembly assembly = null)
    {
      if (assembly == null)
        assembly = Assembly.GetExecutingAssembly();
      var query = from type in assembly.GetTypes()
        where !type.IsGenericType && !type.IsNested
        from methodInfo in type.GetMethods(BindingFlags.Static
                                           | BindingFlags.Public | BindingFlags.NonPublic)
        where methodInfo.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false)
        where methodInfo.GetParameters()[0].ParameterType == extended_type
        select methodInfo;
      return new List<MethodInfo>(query).ToArray();
    }

    #region GetExtensionMethodInfo

    public static MethodInfo GetExtensionMethodInfo2(Type type, string method_name)
    {
      return ReflectionUtil.GetMethodInfo2(type, method_name, BindingFlagsConst.All,
        () =>
        {
          var methodInfos = GetExtensionMethodInfos(type);
          if (!methodInfos.IsNullOrEmpty())
          {
            foreach (var methodInfo in methodInfos)
            {
              if (methodInfo.Name.Equals(method_name))
                return methodInfo;
            }
          }
          return null;
        });
    }

    public static MethodInfo GetExtensionMethodInfo(Type type, string method_name,
      params Type[] source_parameter_types)
    {
      return ReflectionUtil.GetMethodInfo(type, method_name, BindingFlagsConst.All,
        () => GetExtensionMethodInfos(type), source_parameter_types);
    }


    public static MethodInfo GetExtensionMethodInfo(Type type, string method_name, 
      params object[] source_parameters)
    {
      return ReflectionUtil.GetMethodInfo(type, method_name, BindingFlagsConst.All,
        () => GetExtensionMethodInfos(type), source_parameters);
    }

    public static MethodInfo GetExtensionMethodInfo(string full_class_path, string method_name,
      string dll_name = null, params object[] source_parameters)
    {
      return ReflectionUtil.GetMethodInfo(full_class_path, method_name, BindingFlagsConst.All,
        () => GetExtensionMethodInfos(TypeUtil.GetType(full_class_path,dll_name)), dll_name, source_parameters);
    }

    public static MethodInfo GetExtensionMethodInfo(string full_class_path, string method_name,
        Assembly assembly = null, params object[] source_parameters)
    {
      Type type = null;
      if (assembly != null)
        type = assembly.GetType(full_class_path);
      else
        type = TypeUtil.GetType(full_class_path);
      if (type == null)
        return null;
      return ReflectionUtil.GetMethodInfo(full_class_path, method_name, BindingFlagsConst.All,
        () => GetExtensionMethodInfos(type), assembly, source_parameters);
    }

    #endregion

    

   

    #region InvokeExtension
    public static T InvokeExtension<T>(object obj, string full_class_path, string methodInfo_string,
      bool is_miss_not_invoke = true, params object[] parameters)
    {
      parameters = parameters.ToList().AddFirst(obj).ToArray();
      MethodInfo methodInfo = GetExtensionMethodInfo(full_class_path, methodInfo_string,
        ReflectionUtil.GetReflectionType(obj).Assembly, parameters);
      if (methodInfo == null && is_miss_not_invoke)
        return default(T);
      return ReflectionUtil.Invoke<T>(obj, methodInfo, parameters);
    }

    public static T InvokeExtension<T>(object obj, string method_name, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      return InvokeExtension<T>(obj, ReflectionUtil.GetReflectionType(obj).FullName, method_name, is_miss_not_invoke,
        parameters);
    }

    public static void InvokeExtension(object obj, string full_class_path, string methodInfo_string,
      bool is_miss_not_invoke = true, params object[] parameters)
    {
      parameters = parameters.ToList().AddFirst(obj).ToArray();
      MethodInfo methodInfo = GetExtensionMethodInfo(full_class_path, methodInfo_string,
        ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
      if (methodInfo == null && is_miss_not_invoke)
        return;
      ReflectionUtil.Invoke<object>(obj, methodInfo, parameters);
    }

    public static void InvokeExtension(object obj, string method_name, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      InvokeExtension(obj, ReflectionUtil.GetReflectionType(obj).FullName, method_name, is_miss_not_invoke,
        ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
    }

    #endregion

  }
}