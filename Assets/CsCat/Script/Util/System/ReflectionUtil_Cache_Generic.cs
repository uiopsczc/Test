using System.Collections.Generic;
using System.Reflection;
using Type = System.Type;

namespace CsCat
{
  public partial class ReflectionUtil
  {
    static string GetGenericTypesString(Type[] generic_types)
    {
      return generic_types.Join(split_string);
    }
    //////////////////////////////////////////////////////////////////////
    // MethodInfoCache
    //////////////////////////////////////////////////////////////////////
    public static bool IsContainsGenericMethodInfoCache(Type type, string method_name, Type[] generic_types, params Type[] paramer_types)
    {
      if (!cache_dict.ContainsKey(type))
        return false;
      string main_key = methodInfo_string + split_string + method_name + split_string + GetGenericTypesString(generic_types);
      if (!cache_dict[type].ContainsKey(main_key))
        return false;
      var sub_key = new Args(paramer_types);
      return cache_dict[type][main_key].ContainsKey(sub_key);
    }

    public static void SetGenericMethodInfoCache(Type type, string method_name, Type[] generic_types, Type[] paramer_types, MethodInfo methodInfo)
    {
      if (!cache_dict.ContainsKey(type))
        cache_dict[type] = new Dictionary<string, Dictionary<object, object>>();
      string main_key = methodInfo_string + split_string + method_name + split_string + GetGenericTypesString(generic_types); ;
      if (!cache_dict[type].ContainsKey(main_key))
        cache_dict[type][main_key] = new Dictionary<object, object>();
      var sub_key = new Args(paramer_types);
      cache_dict[type][main_key][sub_key] = methodInfo;
    }

    public static MethodInfo GetGenericMethodInfoCache(Type type, string method_name, Type[] generic_types, params Type[] paramer_types)
    {
      if (!cache_dict.ContainsKey(type))
        return null;
      string main_key = methodInfo_string + split_string + method_name + split_string + GetGenericTypesString(generic_types);
      if (!cache_dict[type].ContainsKey(main_key))
        return null;
      var sub_key = new Args(paramer_types);
      return cache_dict[type][main_key][sub_key] as MethodInfo;
    }

    public static bool IsContainsGenericMethodInfoCache2(Type type, string method_name, Type[] generic_types)
    {
      if (!cache_dict.ContainsKey(type))
        return false;
      string main_key = methodInfo_string + split_string + method_name + split_string + GetGenericTypesString(generic_types);
      if (!cache_dict[type].ContainsKey(main_key))
        return false;
      var sub_key = default_methodInfo_sub_key;
      return cache_dict[type][main_key].ContainsKey(sub_key);
    }

    public static void SetGenericMethodInfoCache2(Type type, string method_name, Type[] generic_types, MethodInfo methodInfo)
    {
      if (!cache_dict.ContainsKey(type))
        cache_dict[type] = new Dictionary<string, Dictionary<object, object>>();
      string main_key = methodInfo_string + split_string + method_name + split_string + GetGenericTypesString(generic_types);
      if (!cache_dict[type].ContainsKey(main_key))
        cache_dict[type][main_key] = new Dictionary<object, object>();
      var sub_key = default_methodInfo_sub_key;
      cache_dict[type][main_key][sub_key] = methodInfo;
    }

    public static MethodInfo GetGenericMethodInfoCache2(Type type, string method_name, Type[] generic_types)
    {
      if (!cache_dict.ContainsKey(type))
        return null;
      string main_key = methodInfo_string + split_string + method_name + split_string + GetGenericTypesString(generic_types);
      if (!cache_dict[type].ContainsKey(main_key))
        return null;
      var sub_key = default_methodInfo_sub_key;
      return cache_dict[type][main_key][sub_key] as MethodInfo;
    }

    //////////////////////////////////////////////////////////////////////
    // FieldInfoCache
    //////////////////////////////////////////////////////////////////////
    public static bool IsContainsGenericFieldInfoCache(Type type, string field_name, Type[] generic_types)
    {
      if (!cache_dict.ContainsKey(type))
        return false;
      string main_key = filedInfo_string + split_string + field_name + split_string + GetGenericTypesString(generic_types);
      if (!cache_dict[type].ContainsKey(main_key))
        return false;
      var sub_key = "";
      return cache_dict[type][main_key].ContainsKey(sub_key);
    }

    public static void SetFieldInfoCache(Type type, string field_name, Type[] generic_types, FieldInfo fieldInfo)
    {
      if (!cache_dict.ContainsKey(type))
        cache_dict[type] = new Dictionary<string, Dictionary<object, object>>();
      string main_key = filedInfo_string + split_string + field_name + split_string + GetGenericTypesString(generic_types);
      if (!cache_dict[type].ContainsKey(main_key))
        cache_dict[type][main_key] = new Dictionary<object, object>();
      var sub_key = "";
      cache_dict[type][main_key][sub_key] = fieldInfo;
    }

    public static FieldInfo GetFieldInfoCache(Type type, string field_name, Type[] generic_types)
    {
      if (!cache_dict.ContainsKey(type))
        return null;
      string main_key = filedInfo_string + split_string + field_name + split_string + GetGenericTypesString(generic_types);
      if (!cache_dict[type].ContainsKey(main_key))
        return null;
      var sub_key = "";
      return cache_dict[type][main_key][sub_key] as FieldInfo;
    }

    //////////////////////////////////////////////////////////////////////
    // PropertyInfoCache
    //////////////////////////////////////////////////////////////////////
    public static bool IsContainsPropertyInfoCache(Type type, string property_name, Type[] generic_types)
    {
      if (!cache_dict.ContainsKey(type))
        return false;
      string main_key = propertyInfo_string + split_string + property_name + split_string + GetGenericTypesString(generic_types);
      if (!cache_dict[type].ContainsKey(main_key))
        return false;
      var sub_key = "";
      return cache_dict[type][main_key].ContainsKey(sub_key);
    }

    public static void SetPropertyInfoCache(Type type, string property_name, Type[] generic_types, PropertyInfo propertyInfo)
    {
      if (!cache_dict.ContainsKey(type))
        cache_dict[type] = new Dictionary<string, Dictionary<object, object>>();
      string main_key = propertyInfo_string + split_string + property_name + split_string + GetGenericTypesString(generic_types);
      if (!cache_dict[type].ContainsKey(main_key))
        cache_dict[type][main_key] = new Dictionary<object, object>();
      var sub_key = "";
      cache_dict[type][main_key][sub_key] = propertyInfo;
    }

    public static PropertyInfo GetPropertyInfoCache(Type type, string property_name, Type[] generic_types)
    {
      if (!cache_dict.ContainsKey(type))
        return null;
      string main_key = propertyInfo_string + split_string + property_name + split_string + GetGenericTypesString(generic_types);
      if (!cache_dict[type].ContainsKey(main_key))
        return null;
      var sub_key = "";
      return cache_dict[type][main_key][sub_key] as PropertyInfo;
    }
  }
}