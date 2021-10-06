using System.Collections.Generic;
using System.Reflection;
using Type = System.Type;

namespace CsCat
{
    public partial class ReflectionUtil
    {
        static string GetGenericTypesString(Type[] genericTypes)
        {
            return genericTypes.Join(_splitString);
        }

        //////////////////////////////////////////////////////////////////////
        // MethodInfoCache
        //////////////////////////////////////////////////////////////////////
        public static bool IsContainsGenericMethodInfoCache(Type type, string methodName, Type[] genericTypes,
            params Type[] paramerTypes)
        {
            if (!_cacheDict.ContainsKey(type))
                return false;
            string mainKey = _methodInfoString + _splitString + methodName + _splitString +
                              GetGenericTypesString(genericTypes);
            if (!_cacheDict[type].ContainsKey(mainKey))
                return false;
            var subKey = new Args(paramerTypes);
            return _cacheDict[type][mainKey].ContainsKey(subKey);
        }

        public static void SetGenericMethodInfoCache(Type type, string methodName, Type[] genericTypes,
            Type[] paramerTypes, MethodInfo methodInfo)
        {
            if (!_cacheDict.ContainsKey(type))
                _cacheDict[type] = new Dictionary<string, Dictionary<object, object>>();
            string mainKey = _methodInfoString + _splitString + methodName + _splitString +
                              GetGenericTypesString(genericTypes);
            ;
            if (!_cacheDict[type].ContainsKey(mainKey))
                _cacheDict[type][mainKey] = new Dictionary<object, object>();
            var subKey = new Args(paramerTypes);
            _cacheDict[type][mainKey][subKey] = methodInfo;
        }

        public static MethodInfo GetGenericMethodInfoCache(Type type, string methodName, Type[] genericTypes,
            params Type[] paramerTypes)
        {
            if (!_cacheDict.ContainsKey(type))
                return null;
            string mainKey = _methodInfoString + _splitString + methodName + _splitString +
                              GetGenericTypesString(genericTypes);
            if (!_cacheDict[type].ContainsKey(mainKey))
                return null;
            var subKey = new Args(paramerTypes);
            return _cacheDict[type][mainKey][subKey] as MethodInfo;
        }

        public static bool IsContainsGenericMethodInfoCache2(Type type, string methodName, Type[] genericTypes)
        {
            if (!_cacheDict.ContainsKey(type))
                return false;
            string mainKey = _methodInfoString + _splitString + methodName + _splitString +
                              GetGenericTypesString(genericTypes);
            if (!_cacheDict[type].ContainsKey(mainKey))
                return false;
            var subKey = _defaultMethodInfoSubKey;
            return _cacheDict[type][mainKey].ContainsKey(subKey);
        }

        public static void SetGenericMethodInfoCache2(Type type, string method_name, Type[] generic_types,
            MethodInfo methodInfo)
        {
            if (!_cacheDict.ContainsKey(type))
                _cacheDict[type] = new Dictionary<string, Dictionary<object, object>>();
            string main_key = _methodInfoString + _splitString + method_name + _splitString +
                              GetGenericTypesString(generic_types);
            if (!_cacheDict[type].ContainsKey(main_key))
                _cacheDict[type][main_key] = new Dictionary<object, object>();
            var sub_key = _defaultMethodInfoSubKey;
            _cacheDict[type][main_key][sub_key] = methodInfo;
        }

        public static MethodInfo GetGenericMethodInfoCache2(Type type, string method_name, Type[] generic_types)
        {
            if (!_cacheDict.ContainsKey(type))
                return null;
            string main_key = _methodInfoString + _splitString + method_name + _splitString +
                              GetGenericTypesString(generic_types);
            if (!_cacheDict[type].ContainsKey(main_key))
                return null;
            var sub_key = _defaultMethodInfoSubKey;
            return _cacheDict[type][main_key][sub_key] as MethodInfo;
        }

        //////////////////////////////////////////////////////////////////////
        // FieldInfoCache
        //////////////////////////////////////////////////////////////////////
        public static bool IsContainsGenericFieldInfoCache(Type type, string field_name, Type[] generic_types)
        {
            if (!_cacheDict.ContainsKey(type))
                return false;
            string main_key = _filedInfoString + _splitString + field_name + _splitString +
                              GetGenericTypesString(generic_types);
            if (!_cacheDict[type].ContainsKey(main_key))
                return false;
            var sub_key = "";
            return _cacheDict[type][main_key].ContainsKey(sub_key);
        }

        public static void SetFieldInfoCache(Type type, string fieldName, Type[] generic_types, FieldInfo fieldInfo)
        {
            if (!_cacheDict.ContainsKey(type))
                _cacheDict[type] = new Dictionary<string, Dictionary<object, object>>();
            string main_key = _filedInfoString + _splitString + fieldName + _splitString +
                              GetGenericTypesString(generic_types);
            if (!_cacheDict[type].ContainsKey(main_key))
                _cacheDict[type][main_key] = new Dictionary<object, object>();
            var sub_key = "";
            _cacheDict[type][main_key][sub_key] = fieldInfo;
        }

        public static FieldInfo GetFieldInfoCache(Type type, string fieldName, Type[] generic_types)
        {
            if (!_cacheDict.ContainsKey(type))
                return null;
            string main_key = _filedInfoString + _splitString + fieldName + _splitString +
                              GetGenericTypesString(generic_types);
            if (!_cacheDict[type].ContainsKey(main_key))
                return null;
            var sub_key = "";
            return _cacheDict[type][main_key][sub_key] as FieldInfo;
        }

        //////////////////////////////////////////////////////////////////////
        // PropertyInfoCache
        //////////////////////////////////////////////////////////////////////
        public static bool IsContainsPropertyInfoCache(Type type, string propertyName, Type[] generic_types)
        {
            if (!_cacheDict.ContainsKey(type))
                return false;
            string main_key = _propertyInfoString + _splitString + propertyName + _splitString +
                              GetGenericTypesString(generic_types);
            if (!_cacheDict[type].ContainsKey(main_key))
                return false;
            var sub_key = "";
            return _cacheDict[type][main_key].ContainsKey(sub_key);
        }

        public static void SetPropertyInfoCache(Type type, string propertyName, Type[] generic_types,
            PropertyInfo propertyInfo)
        {
            if (!_cacheDict.ContainsKey(type))
                _cacheDict[type] = new Dictionary<string, Dictionary<object, object>>();
            string main_key = _propertyInfoString + _splitString + propertyName + _splitString +
                              GetGenericTypesString(generic_types);
            if (!_cacheDict[type].ContainsKey(main_key))
                _cacheDict[type][main_key] = new Dictionary<object, object>();
            var sub_key = "";
            _cacheDict[type][main_key][sub_key] = propertyInfo;
        }

        public static PropertyInfo GetPropertyInfoCache(Type type, string propertyName, Type[] generic_types)
        {
            if (!_cacheDict.ContainsKey(type))
                return null;
            string main_key = _propertyInfoString + _splitString + propertyName + _splitString +
                              GetGenericTypesString(generic_types);
            if (!_cacheDict[type].ContainsKey(main_key))
                return null;
            var sub_key = "";
            return _cacheDict[type][main_key][sub_key] as PropertyInfo;
        }
    }
}