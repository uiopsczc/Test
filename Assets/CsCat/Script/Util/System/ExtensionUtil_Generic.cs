using System;
using System.Reflection;

namespace CsCat
{
  public partial class ExtensionUtil
  {

    #region GetExtensionMethodInfo

    public static MethodInfo GetExtensionGenericMethodInfo2(Type type, string method_name, Type[] generic_types)
    {
      return ReflectionUtil.GetGenericMethodInfo2(type, method_name, generic_types, BindingFlagsConst.All,
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

    public static MethodInfo GetExtensionGenericMethodInfo(Type type, string method_name, Type[] generic_types,
      params Type[] source_parameter_types)
    {
      return ReflectionUtil.GetGenericMethodInfo(type, method_name, generic_types, BindingFlagsConst.All,
        () => GetExtensionMethodInfos(type), source_parameter_types);
    }


    public static MethodInfo GetExtensionGenericMethodInfo(Type type, string method_name, Type[] generic_types,
      params object[] source_parameters)
    {
      return ReflectionUtil.GetGenericMethodInfo(type, method_name, generic_types, BindingFlagsConst.All,
        () => GetExtensionMethodInfos(type), source_parameters);
    }

    public static MethodInfo GetExtensionGenericMethodInfo(string full_class_path, string method_name, Type[] generic_types,
      string dll_name = null, params object[] source_parameters)
    {
      return ReflectionUtil.GetGenericMethodInfo(full_class_path, method_name, generic_types, BindingFlagsConst.All,
        () => GetExtensionMethodInfos(TypeUtil.GetType(full_class_path, dll_name)), dll_name, source_parameters);
    }

    public static MethodInfo GetExtensionGenericMethodInfo(string full_class_path, string method_name, Type[] generic_types,
        Assembly assembly = null, params object[] source_parameters)
    {
      Type type = null;
      if (assembly != null)
        type = assembly.GetType(full_class_path);
      else
        type = TypeUtil.GetType(full_class_path);
      if (type == null)
        return null;
      return ReflectionUtil.GetGenericMethodInfo(full_class_path, method_name, generic_types, BindingFlagsConst.All,
        () => GetExtensionMethodInfos(type), assembly, source_parameters);
    }

    #endregion





    #region InvokeExtension
    public static T InvokeExtensionGeneric<T>(object obj, string full_class_path, string methodInfo_string, Type[] generic_types,
      bool is_miss_not_invoke = true, params object[] parameters)
    {
      parameters = parameters.ToList().AddFirst(obj).ToArray();
      MethodInfo methodInfo = GetExtensionGenericMethodInfo(full_class_path, methodInfo_string, generic_types,
        ReflectionUtil.GetReflectionType(obj).Assembly, parameters);
      if (methodInfo == null && is_miss_not_invoke)
        return default(T);
      return ReflectionUtil.Invoke<T>(obj, methodInfo, parameters);
    }

    public static T InvokeExtensionGeneric<T>(object obj, string method_name, Type[] generic_types, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      return InvokeExtensionGeneric<T>(obj, ReflectionUtil.GetReflectionType(obj).FullName, method_name, generic_types, is_miss_not_invoke,
        parameters);
    }

    public static void InvokeExtensionGeneric(object obj, string full_class_path, string methodInfo_string, Type[] generic_types,
      bool is_miss_not_invoke = true, params object[] parameters)
    {
      parameters = parameters.ToList().AddFirst(obj).ToArray();
      MethodInfo methodInfo = GetExtensionGenericMethodInfo(full_class_path, methodInfo_string, generic_types,
        ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
      if (methodInfo == null && is_miss_not_invoke)
        return;
      ReflectionUtil.Invoke<object>(obj, methodInfo, parameters);
    }

    public static void InvokeExtensionGeneric(object obj, string method_name, Type[] generic_types, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      InvokeExtensionGeneric(obj, ReflectionUtil.GetReflectionType(obj).FullName, method_name, generic_types, is_miss_not_invoke,
        ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
    }

    #endregion

  }
}