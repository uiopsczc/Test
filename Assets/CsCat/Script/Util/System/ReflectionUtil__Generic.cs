using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace CsCat
{
  public partial class ReflectionUtil
  {
    #region 反射构造函数

    public static object CreateGenericInstance(Type[] generic_types, string full_class_path, string dll_name = null,
      params object[] parameters)
    {
      Type type = TypeUtil.GetType(full_class_path, dll_name);
      type = type.MakeGenericType(generic_types);
      object obj = Activator.CreateInstance(type, parameters); //根据类型创建实例
      return obj; //类型转换并返回
    }

    #endregion
    

    #region GetMethodInfo

    public static MethodInfo GetGenericMethodInfo2(Type type, string method_name, Type[] generic_types,
    BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo> get_methodInfo_func = null)
    {
      if (IsContainsGenericMethodInfoCache2(type, method_name, generic_types))
      {
        return GetGenericMethodInfoCache2(type, method_name, generic_types);
      }
      var result = get_methodInfo_func==null? type.GetMethod(method_name, bindingFlags) : get_methodInfo_func();
      result = result.MakeGenericMethod(generic_types);
      SetGenericMethodInfoCache2(type, method_name, generic_types, result);
      return result;
    }

    public static MethodInfo GetGenericMethodInfo(Type type, string method_name, Type[] generic_types
,    BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo[]> get_methodInfos_func = null,
      params Type[] source_parameter_types)
    {
      if (IsContainsGenericMethodInfoCache(type, method_name, generic_types, source_parameter_types))
      {
        return GetGenericMethodInfoCache(type, method_name, generic_types, source_parameter_types);
      }
      
      MethodInfo[] methodInfos = get_methodInfos_func == null ? type.GetMethods(bindingFlags) : get_methodInfos_func();
      if (methodInfos.IsNullOrEmpty())
      {
        SetGenericMethodInfoCache(type, method_name, generic_types, source_parameter_types, null);
        return null;
      }

      foreach (MethodInfo methodInfo in methodInfos)
      {
        if (methodInfo.Name.Equals(method_name))
        {
          ParameterInfo[] methodInfo_parameters = methodInfo.GetParameters();
          if (methodInfo_parameters.Length == 0)
          {
            if (source_parameter_types.Length > 0)
              continue;
            else
            {
              var reuslt = methodInfo.MakeGenericMethod(generic_types);
              SetGenericMethodInfoCache(type, method_name, generic_types,source_parameter_types, reuslt);
              return reuslt;
            }
          }

          ParameterInfo final_methodInfo_parameter = methodInfo_parameters[methodInfo_parameters.Length - 1];
          bool is_final_params =
            final_methodInfo_parameter.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0;
          bool is_continue_this_round = false;
          int max_parameters_count = Math.Max(source_parameter_types.Length, methodInfo_parameters.Length);
          Type source_parameter_type;
          Type target_parameter_type;
          ParameterInfo target_parameterInfo;
          if (!is_final_params && source_parameter_types.Length > methodInfo_parameters.Length)
            continue;
          for (int i = 0; i < max_parameters_count; i++)
          {
            if (i < source_parameter_types.Length)
            {
              source_parameter_type = source_parameter_types[i];
              if (i < methodInfo_parameters.Length - 1)
                target_parameterInfo = methodInfo_parameters[i];
              else
                target_parameterInfo = methodInfo_parameters[methodInfo_parameters.Length - 1];
              target_parameter_type = target_parameterInfo.ParameterType;
              if (i >= methodInfo_parameters.Length - 1 && is_final_params)
                target_parameter_type = target_parameter_type.GetElementType();
              if (!target_parameter_type.IsGenericTypeDefinition&&!target_parameter_type.IsGenericType&&!target_parameter_type.IsGenericParameter && !ConvertUtil.CanConvertToType(source_parameter_type, target_parameter_type))
              {
                LogCat.log("77777");
                is_continue_this_round = true;
                break;
              }
            }
            else
            {
              target_parameterInfo = methodInfo_parameters[i];

              if (!target_parameterInfo.IsOptional)
              {
                if (i != methodInfo_parameters.Length - 1 ||
                    (i == methodInfo_parameters.Length - 1 && !is_final_params))
                {
                  is_continue_this_round = true;
                  break;
                }
              }
            }
          }

          if (is_continue_this_round)
            continue;
          else
          {
            var reuslt = methodInfo.MakeGenericMethod(generic_types);
            SetGenericMethodInfoCache(type, method_name, generic_types,source_parameter_types, reuslt);
            SetGenericMethodInfoCache(type, method_name, generic_types,methodInfo_parameters.ToList().ConvertAll(parameter => parameter.ParameterType).ToArray(), reuslt);
            return reuslt;
          }
        }
      }
      SetGenericMethodInfoCache(type, method_name, generic_types,source_parameter_types, null);
      return null;
    }


    public static MethodInfo GetGenericMethodInfo(Type type, string method_name, Type[] generic_types, BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo[]> get_methodInfos_func = null,
      params object[] source_parameters)
    {
      List<Type> source_parameter_type_list = source_parameters.ToList().ConvertAll(parameter => parameter?.GetType());
      return ReflectionUtil.GetGenericMethodInfo(type, method_name, generic_types, bindingFlags, get_methodInfos_func, source_parameter_type_list.ToArray());
    }

    public static MethodInfo GetGenericMethodInfo(string full_class_path, string method_name, Type[] generic_types,
      BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo[]> get_methodInfos_func = null, string dll_name = null, params object[] source_parameters)
    {
      Type type = TypeUtil.GetType(full_class_path, dll_name);

      if (type == null)
        return null;

      return GetGenericMethodInfo(type, method_name, generic_types, bindingFlags, get_methodInfos_func, source_parameters);
    }

    public static MethodInfo GetGenericMethodInfo(string full_class_path, string method_name, Type[] generic_types,
      BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo[]> get_methodInfos_func = null, Assembly assembly = null, params object[] source_parameters)
    {
      Type type = null;
      if (assembly != null)
        type = assembly.GetType(full_class_path);
      else
        type = TypeUtil.GetType(full_class_path);
      if (type == null)
        return null;
      return GetGenericMethodInfo(type, method_name, generic_types, bindingFlags, get_methodInfos_func, source_parameters);
    }

    #endregion

    
    #region Invoke

    public static T InvokeGeneric<T>(object obj, string full_class_path, string methodInfo_string, Type[] generic_types,
      bool is_miss_not_invoke = true, params object[] parameters)
    {
      MethodInfo methodInfo = GetGenericMethodInfo(full_class_path, methodInfo_string, generic_types,
        GetReflectionObject(obj) == null ? BindingFlagsConst.Static : BindingFlagsConst.Instance,null,
        ReflectionUtil.GetReflectionType(obj).Assembly, parameters);
      if (methodInfo == null && is_miss_not_invoke)
        return default(T);
      return Invoke<T>(obj, methodInfo, parameters);
    }

    public static T InvokeGeneric<T>(object obj, string method_name, Type[] generic_types, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      return InvokeGeneric<T>(obj, ReflectionUtil.GetReflectionType(obj).FullName, method_name, generic_types, is_miss_not_invoke,
        parameters);
    }

    public static void InvokeGeneric(object obj, string full_class_path, string methodInfo_string, Type[] generic_types,
      bool is_miss_not_invoke = true, params object[] parameters)
    {
      MethodInfo methodInfo = GetGenericMethodInfo(full_class_path, methodInfo_string, generic_types,
        GetReflectionObject(obj) == null ? BindingFlagsConst.Static : BindingFlagsConst.Instance,null,
        ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
      if (methodInfo == null && is_miss_not_invoke)
        return;
      Invoke<object>(obj, methodInfo, parameters);
    }

    public static void InvokeGeneric(object obj, string method_name, Type[] generic_types, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      InvokeGeneric(obj, ReflectionUtil.GetReflectionType(obj).FullName, method_name, generic_types, is_miss_not_invoke,
        ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
    }
    #endregion
  }
}