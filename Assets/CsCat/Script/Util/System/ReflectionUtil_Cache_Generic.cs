using System.Collections.Generic;
using System.Reflection;
using Type = System.Type;

namespace CsCat
{
    public partial class ReflectionUtil
    {
        static string _GetGenericTypesString(Type[] genericTypes)
        {
            return genericTypes.Join(_splitString);
        }

        //////////////////////////////////////////////////////////////////////
        // MethodInfoCache
        //////////////////////////////////////////////////////////////////////
        public static bool IsContainsGenericMethodInfoCache(Type type, string methodName, Type[] genericTypes,
            params Type[] parameterTypes)
        {
            if (!_cacheDict.ContainsKey(type))
                return false;
            string mainKey = _methodInfoString + _splitString + methodName + _splitString +
                              _GetGenericTypesString(genericTypes);
            if (!_cacheDict[type].ContainsKey(mainKey))
                return false;
            var subKey = new Args(parameterTypes);
            return _cacheDict[type][mainKey].ContainsKey(subKey);
        }

        public static void SetGenericMethodInfoCache(Type type, string methodName, Type[] genericTypes,
            Type[] parameterTypes, MethodInfo methodInfo)
        {
            if (!_cacheDict.ContainsKey(type))
                _cacheDict[type] = new Dictionary<string, Dictionary<object, object>>();
            string mainKey = _methodInfoString + _splitString + methodName + _splitString +
                              _GetGenericTypesString(genericTypes);
            ;
            if (!_cacheDict[type].ContainsKey(mainKey))
                _cacheDict[type][mainKey] = new Dictionary<object, object>();
            var subKey = new Args(parameterTypes);
            _cacheDict[type][mainKey][subKey] = methodInfo;
        }

        public static MethodInfo GetGenericMethodInfoCache(Type type, string methodName, Type[] genericTypes,
            params Type[] parameterTypes)
        {
            if (!_cacheDict.ContainsKey(type))
                return null;
            string mainKey = _methodInfoString + _splitString + methodName + _splitString +
                              _GetGenericTypesString(genericTypes);
            if (!_cacheDict[type].ContainsKey(mainKey))
                return null;
            var subKey = new Args(parameterTypes);
            return _cacheDict[type][mainKey][subKey] as MethodInfo;
        }

        public static bool IsContainsGenericMethodInfoCache2(Type type, string methodName, Type[] genericTypes)
        {
            if (!_cacheDict.ContainsKey(type))
                return false;
            string mainKey = _methodInfoString + _splitString + methodName + _splitString +
                              _GetGenericTypesString(genericTypes);
            if (!_cacheDict[type].ContainsKey(mainKey))
                return false;
            var subKey = _defaultMethodInfoSubKey;
            return _cacheDict[type][mainKey].ContainsKey(subKey);
        }

        public static void SetGenericMethodInfoCache2(Type type, string methodName, Type[] genericTypes,
            MethodInfo methodInfo)
        {
            if (!_cacheDict.ContainsKey(type))
                _cacheDict[type] = new Dictionary<string, Dictionary<object, object>>();
            string mainKey = _methodInfoString + _splitString + methodName + _splitString +
                              _GetGenericTypesString(genericTypes);
            if (!_cacheDict[type].ContainsKey(mainKey))
                _cacheDict[type][mainKey] = new Dictionary<object, object>();
            var subKey = _defaultMethodInfoSubKey;
            _cacheDict[type][mainKey][subKey] = methodInfo;
        }

        public static MethodInfo GetGenericMethodInfoCache2(Type type, string methodName, Type[] genericTypes)
        {
            if (!_cacheDict.ContainsKey(type))
                return null;
            string mainKey = _methodInfoString + _splitString + methodName + _splitString +
                              _GetGenericTypesString(genericTypes);
            if (!_cacheDict[type].ContainsKey(mainKey))
                return null;
            var subKey = _defaultMethodInfoSubKey;
            return _cacheDict[type][mainKey][subKey] as MethodInfo;
        }

        //////////////////////////////////////////////////////////////////////
        // FieldInfoCache
        //////////////////////////////////////////////////////////////////////
        public static bool IsContainsGenericFieldInfoCache(Type type, string fieldName, Type[] genericTypes)
        {
            if (!_cacheDict.ContainsKey(type))
                return false;
            string mainKey = _filedInfoString + _splitString + fieldName + _splitString +
                              _GetGenericTypesString(genericTypes);
            if (!_cacheDict[type].ContainsKey(mainKey))
                return false;
            var subKey = StringConst.String_Empty;
            return _cacheDict[type][mainKey].ContainsKey(subKey);
        }

        public static void SetFieldInfoCache(Type type, string fieldName, Type[] genericTypes, FieldInfo fieldInfo)
        {
            if (!_cacheDict.ContainsKey(type))
                _cacheDict[type] = new Dictionary<string, Dictionary<object, object>>();
            string mainKey = _filedInfoString + _splitString + fieldName + _splitString +
                              _GetGenericTypesString(genericTypes);
            if (!_cacheDict[type].ContainsKey(mainKey))
                _cacheDict[type][mainKey] = new Dictionary<object, object>();
            var subKey = StringConst.String_Empty;
            _cacheDict[type][mainKey][subKey] = fieldInfo;
        }

        public static FieldInfo GetFieldInfoCache(Type type, string fieldName, Type[] genericTypes)
        {
            if (!_cacheDict.ContainsKey(type))
                return null;
            string mainKey = _filedInfoString + _splitString + fieldName + _splitString +
                              _GetGenericTypesString(genericTypes);
            if (!_cacheDict[type].ContainsKey(mainKey))
                return null;
            var subKey = StringConst.String_Empty;
            return _cacheDict[type][mainKey][subKey] as FieldInfo;
        }

        //////////////////////////////////////////////////////////////////////
        // PropertyInfoCache
        //////////////////////////////////////////////////////////////////////
        public static bool IsContainsPropertyInfoCache(Type type, string propertyName, Type[] genericTypes)
        {
            if (!_cacheDict.ContainsKey(type))
                return false;
            string mainKey = _propertyInfoString + _splitString + propertyName + _splitString +
                              _GetGenericTypesString(genericTypes);
            if (!_cacheDict[type].ContainsKey(mainKey))
                return false;
            var subKey = StringConst.String_Empty;
            return _cacheDict[type][mainKey].ContainsKey(subKey);
        }

        public static void SetPropertyInfoCache(Type type, string propertyName, Type[] genericTypes,
            PropertyInfo propertyInfo)
        {
            if (!_cacheDict.ContainsKey(type))
                _cacheDict[type] = new Dictionary<string, Dictionary<object, object>>();
            string mainKey = _propertyInfoString + _splitString + propertyName + _splitString +
                              _GetGenericTypesString(genericTypes);
            if (!_cacheDict[type].ContainsKey(mainKey))
                _cacheDict[type][mainKey] = new Dictionary<object, object>();
            var subKey = StringConst.String_Empty;
            _cacheDict[type][mainKey][subKey] = propertyInfo;
        }

        public static PropertyInfo GetPropertyInfoCache(Type type, string propertyName, Type[] genericTypes)
        {
            if (!_cacheDict.ContainsKey(type))
                return null;
            string mainKey = _propertyInfoString + _splitString + propertyName + _splitString +
                              _GetGenericTypesString(genericTypes);
            if (!_cacheDict[type].ContainsKey(mainKey))
                return null;
            var subKey = StringConst.String_Empty;
            return _cacheDict[type][mainKey][subKey] as PropertyInfo;
        }
    }
}