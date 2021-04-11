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

    public static T CreateInstance<T>(string full_class_path, string dll_name = null, params object[] parameters)
    {
      Type type = TypeUtil.GetType(full_class_path, dll_name);
      object obj = Activator.CreateInstance(type, parameters); //根据类型创建实例
      return (T) obj; //类型转换并返回
    }

    #endregion

    #region   GetType

    /// <summary>
    /// 用于反射，
    /// 如果obj是class，则表示是调用的是静态方法，返回null，
    /// 否则表示调用的是成员方法，返回本身
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static object GetReflectionObject(object obj)
    {

      if (obj is Type)
        return null;
      else
        return obj;
    }

    public static Type GetReflectionType(object obj)
    {
      if (obj is Type)
        return (Type) obj;
      else
        return obj.GetType();
    }

    #endregion

    #region GetMethodInfo

    public static MethodInfo GetMethodInfo2(Type type, string method_name, 
      BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo> get_methodInfo_func = null)
    {
      if (IsContainsMethodInfoCache2(type, method_name))
      {
        return GetMethodInfoCache2(type, method_name);
      }
      var result = get_methodInfo_func==null? type.GetMethod(method_name, bindingFlags): get_methodInfo_func();
      SetMethodInfoCache2(type, method_name, result);
      return result;
    }

    public static MethodInfo GetMethodInfo(Type type, string method_name, BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo[]> get_methodInfos_func = null,
      params Type[] source_parameter_types)
    {
      if (IsContainsMethodInfoCache(type, method_name, source_parameter_types))
      {
        return GetMethodInfoCache(type, method_name, source_parameter_types);
      }

      MethodInfo[] methodInfos = get_methodInfos_func == null ? type.GetMethods(bindingFlags): get_methodInfos_func();
      if (methodInfos.IsNullOrEmpty())
      {
        SetMethodInfoCache(type, method_name, source_parameter_types, null);
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
              SetMethodInfoCache(type, method_name, source_parameter_types, methodInfo);
              return methodInfo;
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

              if (!ConvertUtil.CanConvertToType(source_parameter_type, target_parameter_type))
              {
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
            SetMethodInfoCache(type, method_name, source_parameter_types, methodInfo);
            SetMethodInfoCache(type, method_name, methodInfo_parameters.ToList().ConvertAll(parameter => parameter.ParameterType).ToArray(), methodInfo);
            return methodInfo;
          }
        }
      }
      SetMethodInfoCache(type, method_name, source_parameter_types, null);
      return null;
    }


    public static MethodInfo GetMethodInfo(Type type, string method_name, BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo[]> get_methodInfos_func = null,
      params object[] source_parameters)
    {
      List<Type> source_parameter_type_list = source_parameters.ToList().ConvertAll(parameter => parameter?.GetType());
      return ReflectionUtil.GetMethodInfo(type, method_name, bindingFlags, get_methodInfos_func, source_parameter_type_list.ToArray());
    }

    public static MethodInfo GetMethodInfo(string full_class_path, string method_name,
      BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo[]> get_methodInfos_func = null, string dll_name = null, params object[] source_parameters)
    {
      Type type = TypeUtil.GetType(full_class_path, dll_name);

      if (type == null)
        return null;

      return GetMethodInfo(type, method_name, bindingFlags, get_methodInfos_func, source_parameters);
    }

    public static MethodInfo GetMethodInfo(string full_class_path, string method_name,
      BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo[]> get_methodInfos_func = null, Assembly assembly = null, params object[] source_parameters)
    {
      Type type = null;
      if (assembly != null)
        type = assembly.GetType(full_class_path);
      else
        type = TypeUtil.GetType(full_class_path);
      if (type == null)
        return null;
      return  GetMethodInfo(type, method_name, bindingFlags, get_methodInfos_func, source_parameters);
    }

    #endregion

    #region GetPropertyInfo

    public static PropertyInfo GetPropertyInfo(Type type, string property_name,
      BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      if (IsContainsPropertyInfoCache(type, property_name))
      {
        return GetPropertyInfoCache(type, property_name);
      }

      PropertyInfo result = type.GetProperty(property_name, bindingFlags);
      SetPropertyInfoCache(type, property_name, result);
      return result;
    }

    #endregion

    #region GetFieldInfo

    public static FieldInfo GetFieldInfo(Type type, string field_name,
      BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      if (IsContainsFieldInfoCache(type, field_name))
      {
        return GetFieldInfoCache(type, field_name);
      }

      FieldInfo result = type.GetField(field_name, bindingFlags);
      SetFieldInfoCache(type, field_name,result);
      return result;
    }

    #endregion

    #region Invoke


    public static T Invoke<T>(object obj, MethodInfo methodInfo, params object[] parameters)
    {
      List<object> parameter_list_to_send = new List<object>();
      ParameterInfo[] methodInfo_parameters = methodInfo.GetParameters();
      

      if (methodInfo_parameters.Length == 0 && (parameters == null || parameters.Length == 0))
        return (T) methodInfo.Invoke(GetReflectionObject(obj), parameters);

      ParameterInfo final_methodInfo_parameter = methodInfo_parameters[methodInfo_parameters.Length - 1];
      bool is_final_params =
        final_methodInfo_parameter.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0;

      if (!is_final_params && parameters.Length > methodInfo_parameters.Length)
        throw new Exception("args not match");

      for (int i = 0; i < methodInfo_parameters.Length; i++)
      {
        if (i < parameters.Length)
        {
          if (i != methodInfo_parameters.Length - 1)
            parameter_list_to_send.Add(parameters[i]);
          else
          {
            if (is_final_params)
            {
              ArrayList final_parameter_list = new ArrayList();
              for (int j = i; j < parameters.Length; j++)
                final_parameter_list.Add(parameters[j]);
              parameter_list_to_send.Add(
                final_parameter_list.ToArray(final_methodInfo_parameter.ParameterType.GetElementType()));
            }
            else
            {
              parameter_list_to_send.Add(parameters[i]);
            }
          }
        }
        else
        {
          if (methodInfo_parameters[i].IsOptional)
            parameter_list_to_send.Add(methodInfo_parameters[i].DefaultValue);
          if (i == methodInfo_parameters.Length - 1 && is_final_params)
            parameter_list_to_send.Add(null);
        }
      }
      return (T) methodInfo.Invoke(GetReflectionObject(obj), parameter_list_to_send.ToArray());
    }

    public static void Invoke(object obj, MethodInfo methodInfo, params object[] parameters)
    {
      Invoke<object>(obj, methodInfo, parameters);
    }

    public static T Invoke<T>(object obj, string full_class_path, string methodInfo_string,
      bool is_miss_not_invoke = true, params object[] parameters)
    {
      MethodInfo methodInfo = GetMethodInfo(full_class_path, methodInfo_string,
        GetReflectionObject(obj) == null ? BindingFlagsConst.Static : BindingFlagsConst.Instance,null,
        ReflectionUtil.GetReflectionType(obj).Assembly, parameters);
      if (methodInfo == null && is_miss_not_invoke)
        return default(T);
      return Invoke<T>(obj, methodInfo, parameters);
    }

    public static T Invoke<T>(object obj, string method_name, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      return Invoke<T>(obj, ReflectionUtil.GetReflectionType(obj).FullName, method_name, is_miss_not_invoke,
        parameters);
    }

    public static void Invoke(object obj, string full_class_path, string methodInfo_string,
      bool is_miss_not_invoke = true, params object[] parameters)
    {
      MethodInfo methodInfo = GetMethodInfo(full_class_path, methodInfo_string,
        GetReflectionObject(obj) == null ? BindingFlagsConst.Static : BindingFlagsConst.Instance,null,
        ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
      if (methodInfo == null && is_miss_not_invoke)
        return;
      Invoke<object>(obj, methodInfo, parameters);
    }

    public static void Invoke(object obj, string method_name, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      Invoke(obj, ReflectionUtil.GetReflectionType(obj).FullName, method_name, is_miss_not_invoke,
        ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
    }
    #endregion






  }
}