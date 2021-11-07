using System;
using System.Collections.Generic;

namespace CsCat
{
    public class MethodArgTypesProxy
    {
        /// <summary>
        ///   是否目标参数带有原函数所在的类的引用
        /// </summary>
        public bool isTargetMethodAddSelfArg { get; }

        /// <summary>
        ///   是否目标参数带有原函数的参数列表
        /// </summary>
        public bool isTargetMethodWithSourceArgs { get; }

        /// <summary>
        ///   目标函数参数类型列表
        /// </summary>
        public Type[] targetMethodArgTypes { get; private set; }

        /// <summary>
        ///   原函数参数类型列表
        /// </summary>
        public Type[] sourceMethodArgTypes { get; }

        /// <summary>
        ///   原函数所在类
        /// </summary>
        public Type sourceType { get; }

        public MethodArgTypesProxy(Type sourceType, bool isTargetMethodAddSelfArg,
            bool isTargetMethodWithSourceArgs,
            params Type[] sourceMethodArgTypes)
        {
            this.isTargetMethodAddSelfArg = isTargetMethodAddSelfArg;
            this.isTargetMethodWithSourceArgs = isTargetMethodWithSourceArgs;

            this.sourceMethodArgTypes = sourceMethodArgTypes;
            this.sourceType = sourceType;

            SetTargetMethodArgTypes();
        }


        /// <summary>
        ///   设置目标参数类型列表
        /// </summary>
        private void SetTargetMethodArgTypes()
        {
            var result = new List<Type>();
            if (isTargetMethodAddSelfArg)
                result.Add(sourceType);
            if (isTargetMethodWithSourceArgs && sourceMethodArgTypes != null && sourceMethodArgTypes.Length > 0)
                result.AddRange(sourceMethodArgTypes);
            targetMethodArgTypes = result.ToArray();
        }
    }
}