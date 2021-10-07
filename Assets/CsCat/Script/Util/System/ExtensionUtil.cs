using System;
using System.Collections.Generic;
using System.Reflection;

namespace CsCat
{
    public partial class ExtensionUtil
    {
        public static MethodInfo[] GetExtensionMethodInfos(Type extendedType, Assembly assembly = null)
        {
            if (assembly == null)
                assembly = Assembly.GetExecutingAssembly();
            var list = new List<MethodInfo>();
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsGenericType || type.IsNested) continue;
                foreach (var methodInfo in type.GetMethods(BindingFlags.Static
                                                           | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (methodInfo.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false) &&
                        methodInfo.GetParameters()[0].ParameterType == extendedType)
                        list.Add(methodInfo);
                }
            }

            return list.ToArray();
        }


        public static MethodInfo GetExtensionMethodInfo2(Type type, string methodName)
        {
            return ReflectionUtil.GetMethodInfo2(type, methodName, BindingFlagsConst.All,
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

        public static MethodInfo GetExtensionMethodInfo(Type type, string methodName,
            params Type[] sourceParameterTypes)
        {
            return ReflectionUtil.GetMethodInfo(type, methodName, BindingFlagsConst.All,
                () => GetExtensionMethodInfos(type), sourceParameterTypes);
        }


        public static MethodInfo GetExtensionMethodInfo(Type type, string methodName,
            params object[] sourceParameters)
        {
            return ReflectionUtil.GetMethodInfo(type, methodName, BindingFlagsConst.All,
                () => GetExtensionMethodInfos(type), sourceParameters);
        }

        public static MethodInfo GetExtensionMethodInfo(string fullClassPath, string methodName,
            string dllName = null, params object[] sourceParameters)
        {
            return ReflectionUtil.GetMethodInfo(fullClassPath, methodName, BindingFlagsConst.All,
                () => GetExtensionMethodInfos(TypeUtil.GetType(fullClassPath, dllName)), dllName,
                sourceParameters);
        }

        public static MethodInfo GetExtensionMethodInfo(string fullClassPath, string methodName,
            Assembly assembly = null, params object[] sourceParameters)
        {
            Type type = null;
            type = assembly != null ? assembly.GetType(fullClassPath) : TypeUtil.GetType(fullClassPath);
            return type == null
                ? null
                : ReflectionUtil.GetMethodInfo(fullClassPath, methodName, BindingFlagsConst.All,
                    () => GetExtensionMethodInfos(type), assembly, sourceParameters);
        }

        public static T InvokeExtension<T>(object obj, string fullClassPath, string methodInfoString,
            bool isMissNotInvoke = true, params object[] parameters)
        {
            parameters = parameters.ToList().AddFirst(obj).ToArray();
            MethodInfo methodInfo = GetExtensionMethodInfo(fullClassPath, methodInfoString,
                ReflectionUtil.GetReflectionType(obj).Assembly, parameters);
            return methodInfo == null && isMissNotInvoke
                ? default(T)
                : ReflectionUtil.Invoke<T>(obj, methodInfo, parameters);
        }

        public static T InvokeExtension<T>(object obj, string methodName, bool isMissNotInvoke = true,
            params object[] parameters)
        {
            return InvokeExtension<T>(obj, ReflectionUtil.GetReflectionType(obj).FullName, methodName,
                isMissNotInvoke,
                parameters);
        }

        public static void InvokeExtension(object obj, string fullClassPath, string methodInfoString,
            bool isMissNotInvoke = true, params object[] parameters)
        {
            parameters = parameters.ToList().AddFirst(obj).ToArray();
            MethodInfo methodInfo = GetExtensionMethodInfo(fullClassPath, methodInfoString,
                ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
            if (methodInfo == null && isMissNotInvoke)
                return;
            ReflectionUtil.Invoke<object>(obj, methodInfo, parameters);
        }

        public static void InvokeExtension(object obj, string methodName, bool isMissNotInvoke = true,
            params object[] parameters)
        {
            InvokeExtension(obj, ReflectionUtil.GetReflectionType(obj).FullName, methodName, isMissNotInvoke,
                ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
        }
    }
}