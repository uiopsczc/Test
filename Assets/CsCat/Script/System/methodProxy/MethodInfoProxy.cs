using System;
using System.Reflection;

namespace CsCat
{
    public class MethodInfoProxy
    {
        /// <summary>
        ///   是否目标参数带有原函数所在的类的引用
        /// </summary>
        public bool isTargetMethodAddSelfArg => methodArgTypesProxy.isTargetMethodAddSelfArg;

        /// <summary>
        ///   是否目标参数带有原函数的参数列表
        /// </summary>
        public bool isTargetMethodWithSourceArgs => methodArgTypesProxy.isTargetMethodWithSourceArgs;

        /// <summary>
        ///   目标函数名称
        /// </summary>
        public string targetMethodName { get; }

        /// <summary>
        ///   目标参数列表代理
        /// </summary>
        public MethodArgTypesProxy methodArgTypesProxy { get; }

        /// <summary>
        ///   目标类
        /// </summary>
        public Type targetType { get; }

        /// <summary>
        ///   原函数所在类
        /// </summary>
        public Type sourceType => methodArgTypesProxy.sourceType;

        public MethodInfoProxy(string targetMethodName, Type targetType, Type sourceType, bool isTargetAddSelfArg,
            bool isTargetWithSourceArgs, params Type[] sourceArgTypes)
        {
            this.targetMethodName = targetMethodName;
            this.targetType = targetType;
            methodArgTypesProxy =
                new MethodArgTypesProxy(sourceType, isTargetAddSelfArg, isTargetWithSourceArgs, sourceArgTypes);
        }


        /// <summary>
        ///   获取目标函数
        /// </summary>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public MethodInfo GetTargetMethodInfo(BindingFlags bindingFlags = BindingFlagsConst.All)
        {
            return targetType.GetMethodInfo(targetMethodName, bindingFlags,
                methodArgTypesProxy.targetMethodArgTypes);
        }
    }
}