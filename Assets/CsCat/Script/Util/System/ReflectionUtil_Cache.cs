using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Google.Protobuf.WellKnownTypes;
using Type = System.Type;

namespace CsCat
{
  public partial class ReflectionUtil
  {
    public static Dictionary<Type, Dictionary<string, Dictionary<object,object>>> cache_dict =
      new Dictionary<Type, Dictionary<string, Dictionary<object, object>>>();

    private const string methodInfo_string = "methodInfo";
    private const string default_methodInfo_sub_key = "methodInfo_sub_key";
    private const string filedInfo_string = "fieldInfo";
    private const string propertyInfo_string = "propertyInfo";
    private const string split_string = "_";

    //////////////////////////////////////////////////////////////////////
    // MethodInfoCache
    //////////////////////////////////////////////////////////////////////
    public static bool IsContainsMethodInfoCache(Type type,string method_name, params Type[] paramer_types)
    {
      if (!cache_dict.ContainsKey(type))
        return false;
      string main_key = methodInfo_string + split_string + method_name;
      if (!cache_dict[type].ContainsKey(main_key))
        return false;
      var sub_key = new Args(paramer_types);
      return cache_dict[type][main_key].ContainsKey(sub_key);
    }

    public static void SetMethodInfoCache(Type type, string method_name, Type[] paramer_types, MethodInfo methodInfo)
    {
      if (!cache_dict.ContainsKey(type))
        cache_dict[type] = new Dictionary<string, Dictionary<object, object>>();
      string main_key = methodInfo_string + split_string + method_name;
      if (!cache_dict[type].ContainsKey(main_key))
        cache_dict[type][main_key] = new Dictionary<object, object>();
      var sub_key = new Args(paramer_types);
      cache_dict[type][main_key][sub_key] = methodInfo;
    }

    public static MethodInfo GetMethodInfoCache(Type type,string method_name, params Type[] paramer_types)
    {
      if (!cache_dict.ContainsKey(type))
        return null;
      string main_key = methodInfo_string + split_string + method_name;
      if (!cache_dict[type].ContainsKey(main_key))
        return null;
      var sub_key = new Args(paramer_types);
      return cache_dict[type][main_key][sub_key] as MethodInfo;
    }

    public static bool IsContainsMethodInfoCache2(Type type, string method_name)
    {
      if (!cache_dict.ContainsKey(type))
        return false;
      string main_key = methodInfo_string + split_string + method_name;
      if (!cache_dict[type].ContainsKey(main_key))
        return false;
      var sub_key = default_methodInfo_sub_key;
      return cache_dict[type][main_key].ContainsKey(sub_key);
    }

    public static void SetMethodInfoCache2(Type type, string method_name, MethodInfo methodInfo)
    {
      if (!cache_dict.ContainsKey(type))
        cache_dict[type] = new Dictionary<string, Dictionary<object, object>>();
      string main_key = methodInfo_string + split_string + method_name;
      if (!cache_dict[type].ContainsKey(main_key))
        cache_dict[type][main_key] = new Dictionary<object, object>();
      var sub_key = default_methodInfo_sub_key;
      cache_dict[type][main_key][sub_key] = methodInfo;
    }

    public static MethodInfo GetMethodInfoCache2(Type type, string method_name)
    {
      if (!cache_dict.ContainsKey(type))
        return null;
      string main_key = methodInfo_string + split_string + method_name;
      if (!cache_dict[type].ContainsKey(main_key))
        return null;
      var sub_key = default_methodInfo_sub_key;
      return cache_dict[type][main_key][sub_key] as MethodInfo;
    }

    //////////////////////////////////////////////////////////////////////
    // FieldInfoCache
    //////////////////////////////////////////////////////////////////////
    public static bool IsContainsFieldInfoCache(Type type, string field_name)
    {
      if (!cache_dict.ContainsKey(type))
        return false;
      string main_key = filedInfo_string + split_string + field_name;
      if (!cache_dict[type].ContainsKey(main_key))
        return false;
      var sub_key = "";
      return cache_dict[type][main_key].ContainsKey(sub_key);
    }

    public static void SetFieldInfoCache(Type type, string field_name, FieldInfo fieldInfo)
    {
      if (!cache_dict.ContainsKey(type))
        cache_dict[type] = new Dictionary<string, Dictionary<object, object>>();
      string main_key = filedInfo_string + split_string + field_name;
      if (!cache_dict[type].ContainsKey(main_key))
        cache_dict[type][main_key] = new Dictionary<object, object>();
      var sub_key = "";
      cache_dict[type][main_key][sub_key] = fieldInfo;
    }

    public static FieldInfo GetFieldInfoCache(Type type, string field_name)
    {
      if (!cache_dict.ContainsKey(type))
        return null;
      string main_key = filedInfo_string + split_string + field_name;
      if (!cache_dict[type].ContainsKey(main_key))
        return null;
      var sub_key = "";
      return cache_dict[type][main_key][sub_key] as FieldInfo;
    }

    //////////////////////////////////////////////////////////////////////
    // PropertyInfoCache
    //////////////////////////////////////////////////////////////////////
    public static bool IsContainsPropertyInfoCache(Type type, string property_name)
    {
      if (!cache_dict.ContainsKey(type))
        return false;
      string main_key = propertyInfo_string + split_string + property_name;
      if (!cache_dict[type].ContainsKey(main_key))
        return false;
      var sub_key = "";
      return cache_dict[type][main_key].ContainsKey(sub_key);
    }

    public static void SetPropertyInfoCache(Type type, string property_name,  PropertyInfo propertyInfo)
    {
      if (!cache_dict.ContainsKey(type))
        cache_dict[type] = new Dictionary<string, Dictionary<object, object>>();
      string main_key = propertyInfo_string + split_string + property_name;
      if (!cache_dict[type].ContainsKey(main_key))
        cache_dict[type][main_key] = new Dictionary<object, object>();
      var sub_key = "";
      cache_dict[type][main_key][sub_key] = propertyInfo;
    }

    public static PropertyInfo GetPropertyInfoCache(Type type, string property_name)
    {
      if (!cache_dict.ContainsKey(type))
        return null;
      string main_key = propertyInfo_string + split_string + property_name;
      if (!cache_dict[type].ContainsKey(main_key))
        return null;
      var sub_key = "";
      return cache_dict[type][main_key][sub_key] as PropertyInfo;
    }
  }
}