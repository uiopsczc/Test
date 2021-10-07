using System;
using System.Reflection;

namespace CsCat
{
    public partial class ExtensionUtil
    {
        public static MethodInfo GetExtensionGenericMethodInfo2(Type type, string methodName, Type[] genericTypes)
        {
            return ReflectionUtil.GetGenericMethodInfo2(type, methodName, genericTypes, BindingFlagsConst.All,
                () =>
                {
                    var methodInfos = GetExtensionMethodInfos(type);
                    if (methodInfos.IsNullOrEmpty()) return null;
                    foreach (var methodInfo in methodInfos)
                    {
                        if (methodInfo.Name.Equals(methodName))
                            return methodInfo;
                    }
                    return null;
                });
        }

        public static MethodInfo GetExtensionGenericMethodInfo(Type type, string methodName, Type[] genericTypes,
            params Type[] sourceParameterTypes)
        {
            return ReflectionUtil.GetGenericMethodInfo(type, methodName, genericTypes, BindingFlagsConst.All,
                () => GetExtensionMethodInfos(type), sourceParameterTypes);
        }


        public static MethodInfo GetExtensionGenericMethodInfo(Type type, string methodName, Type[] genericTypes,
            params object[] sourceParameters)
        {
            return ReflectionUtil.GetGenericMethodInfo(type, methodName, genericTypes, BindingFlagsConst.All,
                () => GetExtensionMethodInfos(type), sourceParameters);
        }

        public static MethodInfo GetExtensionGenericMethodInfo(string fullClassPath, string methodName,
            Type[] genericTypes,
            string dllName = null, params object[] sourceParameters)
        {
            return ReflectionUtil.GetGenericMethodInfo(fullClassPath, methodName, genericTypes,
                BindingFlagsConst.All,
                () => GetExtensionMethodInfos(TypeUtil.GetType(fullClassPath, dllName)), dllName,
                sourceParameters);
        }

        public static MethodInfo GetExtensionGenericMethodInfo(string fullClassPath, string methodName,
            Type[] genericTypes,
            Assembly assembly = null, params object[] sourceParameters)
        {
            Type type = null;
            type = assembly != null ? assembly.GetType(fullClassPath) : TypeUtil.GetType(fullClassPath);
            return type == null
                ? null
                : ReflectionUtil.GetGenericMethodInfo(fullClassPath, methodName, genericTypes,
                    BindingFlagsConst.All,
                    () => GetExtensionMethodInfos(type), assembly, sourceParameters);
        }

        public static T InvokeExtensionGeneric<T>(object obj, string fullClassPath, string methodInfoString,
            Type[] genericTypes,
            bool isMissNotInvoke = true, params object[] parameters)
        {
            parameters = parameters.ToList().AddFirst(obj).ToArray();
            MethodInfo methodInfo = GetExtensionGenericMethodInfo(fullClassPath, methodInfoString, genericTypes,
                ReflectionUtil.GetReflectionType(obj).Assembly, parameters);
            return methodInfo == null && isMissNotInvoke
                ? default(T)
                : ReflectionUtil.Invoke<T>(obj, methodInfo, parameters);
        }

        public static T InvokeExtensionGeneric<T>(object obj, string methodName, Type[] genericTypes,
            bool isMissNotInvoke = true,
            params object[] parameters)
        {
            return InvokeExtensionGeneric<T>(obj, ReflectionUtil.GetReflectionType(obj).FullName, methodName,
                genericTypes, isMissNotInvoke,
                parameters);
        }

        public static void InvokeExtensionGeneric(object obj, string fullClassPath, string methodInfoString,
            Type[] genericTypes,
            bool isMissNotInvoke = true, params object[] parameters)
        {
            parameters = parameters.ToList().AddFirst(obj).ToArray();
            MethodInfo methodInfo = GetExtensionGenericMethodInfo(fullClassPath, methodInfoString, genericTypes,
                ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
            if (methodInfo == null && isMissNotInvoke) return;
            ReflectionUtil.Invoke<object>(obj, methodInfo, parameters);
        }

        public static void InvokeExtensionGeneric(object obj, string methodName, Type[] genericTypes,
            bool isMissNotInvoke = true,
            params object[] parameters)
        {
            InvokeExtensionGeneric(obj, ReflectionUtil.GetReflectionType(obj).FullName, methodName, genericTypes,
                isMissNotInvoke,
                ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
        }
    }
}