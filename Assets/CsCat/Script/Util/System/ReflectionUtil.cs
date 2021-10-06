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
        public static T CreateInstance<T>(string fullClassPath, string dllName = null, params object[] parameters)
        {
            Type type = TypeUtil.GetType(fullClassPath, dllName);
            object obj = Activator.CreateInstance(type, parameters); //根据类型创建实例
            return (T) obj; //类型转换并返回
        }


        /// <summary>
        /// 用于反射，
        /// 如果obj是class，则表示是调用的是静态方法，返回null，
        /// 否则表示调用的是成员方法，返回本身
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object GetReflectionObject(object obj)
        {
            return obj is Type ? null : obj;
        }

        public static Type GetReflectionType(object obj)
        {
            return obj is Type type ? type : obj.GetType();
        }


        public static MethodInfo GetMethodInfo2(Type type, string methodName,
            BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo> getMethodInfoFunc = null)
        {
            if (IsContainsMethodInfoCache2(type, methodName))
                return GetMethodInfoCache2(type, methodName);

            var result = getMethodInfoFunc == null
                ? type.GetMethod(methodName, bindingFlags)
                : getMethodInfoFunc();
            SetMethodInfoCache2(type, methodName, result);
            return result;
        }

        public static MethodInfo GetMethodInfo(Type type, string methodName,
            BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo[]> getMethodInfosFunc = null,
            params Type[] sourceParameterTypes)
        {
            if (IsContainsMethodInfoCache(type, methodName, sourceParameterTypes))
                return GetMethodInfoCache(type, methodName, sourceParameterTypes);

            MethodInfo[] methodInfos =
                getMethodInfosFunc == null ? type.GetMethods(bindingFlags) : getMethodInfosFunc();
            if (methodInfos.IsNullOrEmpty())
            {
                SetMethodInfoCache(type, methodName, sourceParameterTypes, null);
                return null;
            }

            foreach (MethodInfo methodInfo in methodInfos)
            {
                if (methodInfo.Name.Equals(methodName))
                {
                    ParameterInfo[] methodInfoParameters = methodInfo.GetParameters();
                    if (methodInfoParameters.Length == 0)
                    {
                        if (sourceParameterTypes.Length > 0)
                            continue;
                        SetMethodInfoCache(type, methodName, sourceParameterTypes, methodInfo);
                        return methodInfo;
                    }

                    ParameterInfo finalMethodInfoParameter = methodInfoParameters[methodInfoParameters.Length - 1];
                    bool isFinalParams =
                        finalMethodInfoParameter.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0;
                    bool isContinueThisRound = false;
                    int maxParametersCount = Math.Max(sourceParameterTypes.Length, methodInfoParameters.Length);
                    Type sourceParameterType;
                    Type targetParameterType;
                    ParameterInfo targetParameterInfo;

                    if (!isFinalParams && sourceParameterTypes.Length > methodInfoParameters.Length)
                        continue;

                    for (int i = 0; i < maxParametersCount; i++)
                    {
                        if (i < sourceParameterTypes.Length)
                        {
                            sourceParameterType = sourceParameterTypes[i];
                            targetParameterInfo = i < methodInfoParameters.Length - 1 ? methodInfoParameters[i] : methodInfoParameters[methodInfoParameters.Length - 1];
                            targetParameterType = targetParameterInfo.ParameterType;
                            if (i >= methodInfoParameters.Length - 1 && isFinalParams)
                                targetParameterType = targetParameterType.GetElementType();

                            if (ConvertUtil.CanConvertToType(sourceParameterType, targetParameterType)) continue;
                            isContinueThisRound = true;
                            break;
                        }

                        targetParameterInfo = methodInfoParameters[i];

                        if (targetParameterInfo.IsOptional) continue;
                        if (i == methodInfoParameters.Length - 1 &&
                            (i != methodInfoParameters.Length - 1 || isFinalParams)) continue;
                        isContinueThisRound = true;
                        break;
                    }

                    if (isContinueThisRound)
                        continue;
                    SetMethodInfoCache(type, methodName, sourceParameterTypes, methodInfo);
                    SetMethodInfoCache(type, methodName,
                        methodInfoParameters.ToList().ConvertAll(parameter => parameter.ParameterType).ToArray(),
                        methodInfo);
                    return methodInfo;
                }
            }

            SetMethodInfoCache(type, methodName, sourceParameterTypes, null);
            return null;
        }


        public static MethodInfo GetMethodInfo(Type type, string methodName,
            BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo[]> getMethodInfosFunc = null,
            params object[] sourceParameters)
        {
            List<Type> sourceParameterTypeList =
                sourceParameters.ToList().ConvertAll(parameter => parameter?.GetType());
            return ReflectionUtil.GetMethodInfo(type, methodName, bindingFlags, getMethodInfosFunc,
                sourceParameterTypeList.ToArray());
        }

        public static MethodInfo GetMethodInfo(string fullClassPath, string methodName,
            BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo[]> getMethodInfosFunc = null,
            string dllName = null, params object[] sourceParameters)
        {
            Type type = TypeUtil.GetType(fullClassPath, dllName);

            return type == null ? null : GetMethodInfo(type, methodName, bindingFlags, getMethodInfosFunc, sourceParameters);
        }

        public static MethodInfo GetMethodInfo(string fullClassPath, string methodName,
            BindingFlags bindingFlags = BindingFlagsConst.All, Func<MethodInfo[]> getMethodInfosFunc = null,
            Assembly assembly = null, params object[] sourceParameters)
        {
            Type type = null;
            type = assembly != null ? assembly.GetType(fullClassPath) : TypeUtil.GetType(fullClassPath);
            return type == null ? null : GetMethodInfo(type, methodName, bindingFlags, getMethodInfosFunc, sourceParameters);
        }


        public static PropertyInfo GetPropertyInfo(Type type, string propertyName,
            BindingFlags bindingFlags = BindingFlagsConst.All)
        {
            if (IsContainsPropertyInfoCache(type, propertyName))
                return GetPropertyInfoCache(type, propertyName);

            PropertyInfo result = type.GetProperty(propertyName, bindingFlags);
            SetPropertyInfoCache(type, propertyName, result);
            return result;
        }


        public static FieldInfo GetFieldInfo(Type type, string fieldName,
            BindingFlags bindingFlags = BindingFlagsConst.All)
        {
            if (IsContainsFieldInfoCache(type, fieldName))
                return GetFieldInfoCache(type, fieldName);

            FieldInfo result = type.GetField(fieldName, bindingFlags);
            SetFieldInfoCache(type, fieldName, result);
            return result;
        }


        public static T Invoke<T>(object obj, MethodInfo methodInfo, params object[] parameters)
        {
            List<object> parameterListToSend = new List<object>();
            ParameterInfo[] methodInfoParameters = methodInfo.GetParameters();


            if (methodInfoParameters.Length == 0 && (parameters == null || parameters.Length == 0))
                return (T) methodInfo.Invoke(GetReflectionObject(obj), parameters);

            ParameterInfo finalMethodInfoParameter = methodInfoParameters[methodInfoParameters.Length - 1];
            bool isFinalParams =
                finalMethodInfoParameter.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0;

            if (!isFinalParams && parameters.Length > methodInfoParameters.Length)
                throw new Exception("args not match");

            for (int i = 0; i < methodInfoParameters.Length; i++)
            {
                if (i < parameters.Length)
                {
                    if (i != methodInfoParameters.Length - 1)
                        parameterListToSend.Add(parameters[i]);
                    else
                    {
                        if (isFinalParams)
                        {
                            ArrayList finalParameterList = new ArrayList();
                            for (int j = i; j < parameters.Length; j++)
                                finalParameterList.Add(parameters[j]);
                            parameterListToSend.Add(
                                finalParameterList.ToArray(finalMethodInfoParameter.ParameterType
                                    .GetElementType()));
                        }
                        else
                            parameterListToSend.Add(parameters[i]);
                    }
                }
                else
                {
                    if (methodInfoParameters[i].IsOptional)
                        parameterListToSend.Add(methodInfoParameters[i].DefaultValue);
                    if (i == methodInfoParameters.Length - 1 && isFinalParams)
                        parameterListToSend.Add(null);
                }
            }

            return (T) methodInfo.Invoke(GetReflectionObject(obj), parameterListToSend.ToArray());
        }

        public static void Invoke(object obj, MethodInfo methodInfo, params object[] parameters)
        {
            Invoke<object>(obj, methodInfo, parameters);
        }

        public static T Invoke<T>(object obj, string fullClassPath, string methodInfoString,
            bool isMissNotInvoke = true, params object[] parameters)
        {
            MethodInfo methodInfo = GetMethodInfo(fullClassPath, methodInfoString,
                GetReflectionObject(obj) == null ? BindingFlagsConst.Static : BindingFlagsConst.Instance, null,
                ReflectionUtil.GetReflectionType(obj).Assembly, parameters);
            return methodInfo == null && isMissNotInvoke ? default(T) : Invoke<T>(obj, methodInfo, parameters);
        }

        public static T Invoke<T>(object obj, string methodName, bool isMissNotInvoke = true,
            params object[] parameters)
        {
            return Invoke<T>(obj, ReflectionUtil.GetReflectionType(obj).FullName, methodName, isMissNotInvoke,
                parameters);
        }

        public static void Invoke(object obj, string fullClassPath, string methodInfoString,
            bool isMissNotInvoke = true, params object[] parameters)
        {
            MethodInfo methodInfo = GetMethodInfo(fullClassPath, methodInfoString,
                GetReflectionObject(obj) == null ? BindingFlagsConst.Static : BindingFlagsConst.Instance, null,
                ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
            if (methodInfo == null && isMissNotInvoke)
                return;
            Invoke<object>(obj, methodInfo, parameters);
        }

        public static void Invoke(object obj, string methodName, bool isMissNotInvoke = true,
            params object[] parameters)
        {
            Invoke(obj, ReflectionUtil.GetReflectionType(obj).FullName, methodName, isMissNotInvoke,
                ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
        }
    }
}