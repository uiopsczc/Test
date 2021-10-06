using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CsCat
{
    /// <summary>
    ///   AOPHandle处理
    /// </summary>
    public class AOPHandler : ISingleton
    {

        /// <summary>
        ///   所有类中aopMethod的缓存
        ///   key是被切面的方法
        ///   value是AOPAttributeMethodInfoProxyCategory列表
        ///   AOPAttributeMethodInfoProxyCategory的东西可以参考AOPAttributeMethodInfoProxyCategory的类说明
        /// </summary>
        private readonly Dictionary<MethodBase, List<AOPAttributeMethodInfoProxyCategory>>
            aopAttributeMethodInfoProxyCategory_cache_dict =
                new Dictionary<MethodBase, List<AOPAttributeMethodInfoProxyCategory>>();
        
        

        public static AOPHandler instance => SingletonFactory.instance.Get<AOPHandler>();

        public void SingleInit()
        {
        }

        #region public method

        #region AOP_Handle

        /// <summary>
        ///   前处理
        /// </summary>
        /// <param name="source_method_owner"></param>
        /// <param name="source_methodBase"></param>
        /// <param name="source_method_args"></param>
        public void Pre_AOP_Handle(object source_method_owner, MethodBase source_methodBase,
            params object[] source_method_args)
        {
            var aopAttributeMethodInfoProxyCategory_list =
                GetAOPAttributeMethodInfoProxyCategoryList(source_methodBase);
            aopAttributeMethodInfoProxyCategory_list.ForEach(
                aopAttributeMethodCategory =>
                {
                    InvokeAOPAttributeMethod(aopAttributeMethodCategory, AOPMethodType.Pre_AOP_Handle,
                        source_method_owner,
                        source_methodBase, source_method_args);
                }
            );
        }

        /// <summary>
        ///   后处理
        /// </summary>
        /// <param name="source_method_owner"></param>
        /// <param name="source_methodBase"></param>
        /// <param name="source_method_args"></param>
        public void Post_AOP_Handle(object source_method_owner, MethodBase source_methodBase,
            params object[] source_method_args)
        {
            var aopAttributeMethodCategory_list = GetAOPAttributeMethodInfoProxyCategoryList(source_methodBase);
            aopAttributeMethodCategory_list.ForEach(
                aopAttributeMethodCategory =>
                {
                    InvokeAOPAttributeMethod(aopAttributeMethodCategory, AOPMethodType.Post_AOP_Handle,
                        source_method_owner,
                        source_methodBase, source_method_args);
                }
            );
        }

        #endregion

        /// <summary>
        ///   获取特定的被切面的方法的AOPAttributeMethodInfoProxyCategoryList
        /// </summary>
        /// <param name="source_methodBase"></param>
        /// <returns></returns>
        private List<AOPAttributeMethodInfoProxyCategory> GetAOPAttributeMethodInfoProxyCategoryList(
            MethodBase source_methodBase)
        {
            var aopAttributeMethodInfoProxyCategories = from a in source_methodBase.GetCustomAttributes(true)
                where a is IAOPAttribute
                select new AOPAttributeMethodInfoProxyCategory(a as IAOPAttribute); //linq会延迟处理的，用到的时候才会调用
            var result = aopAttributeMethodInfoProxyCategory_cache_dict.GetOrAddDefault(source_methodBase,
                () => new List<AOPAttributeMethodInfoProxyCategory>(aopAttributeMethodInfoProxyCategories));
            return result;
        }

        /// <summary>
        ///   查找并调用被切面的方法的其中一个AOPAttribute属性中的AOP处理方法
        /// </summary>
        /// <param name="aopAttributeMethodInfoProxyCategory"></param>
        /// <param name="aopMethodType"></param>
        /// <param name="source_method_owner"></param>
        /// <param name="source_methodBase"></param>
        /// <param name="source_method_args"></param>
        private void InvokeAOPAttributeMethod(AOPAttributeMethodInfoProxyCategory aopAttributeMethodInfoProxyCategory,
            AOPMethodType aopMethodType, object source_method_owner, MethodBase source_methodBase,
            params object[] source_method_args)
        {
            //获取目标函数
            var target_methodInfoProxy = GetTargetMethodInfoProxy(aopAttributeMethodInfoProxyCategory, aopMethodType,
                source_method_owner, source_methodBase, source_methodBase.GetParameterTypes());

            //缓存目标函数以备下次使用
            aopAttributeMethodInfoProxyCategory.aop_methodInfo_dict[aopMethodType] = target_methodInfoProxy;

            //调用目标函数
            var target_methodInfo = target_methodInfoProxy.GetTargetMethodInfo();
            var target_method_args = GetTargetMethodArgs(target_methodInfoProxy.is_target_method_add_self_arg,
                source_method_owner,
                target_methodInfoProxy.is_target_method_with_source_args, source_method_args);
            target_methodInfo.Invoke(aopAttributeMethodInfoProxyCategory.aopAttribute, target_method_args);
        }

        /// <summary>
        ///   获取目标函数
        /// </summary>
        /// <param name="aopAttributeMethodInfoProxyCategory"></param>
        /// <param name="aopMethodType"></param>
        /// <param name="source_method_owner"></param>
        /// <param name="source_methodBase"></param>
        /// <param name="source_method_parameter_types"></param>
        /// <returns></returns>
        private MethodInfoProxy GetTargetMethodInfoProxy(
            AOPAttributeMethodInfoProxyCategory aopAttributeMethodInfoProxyCategory,
            AOPMethodType aopMethodType, object source_method_owner, MethodBase source_methodBase,
            params Type[] source_method_parameter_types)
        {
            //如果缓存中有对应的aop处理函数，则不用查找，直接使用
            if (aopAttributeMethodInfoProxyCategory.aop_methodInfo_dict.ContainsKey(aopMethodType))
                return aopAttributeMethodInfoProxyCategory.aop_methodInfo_dict[aopMethodType];

            //根据优先顺序查找
            var aop_attriubte_type = aopAttributeMethodInfoProxyCategory.aopAttribute.GetType();
            var source_method_name = source_methodBase.Name;
            return AOPUtil.SeachTargetMethodInfoProxy(aop_attriubte_type, source_methodBase.DeclaringType,
                source_method_name,
                aopMethodType, source_method_parameter_types);
        }

        /// <summary>
        ///   获取目标函数时实际需要传入的参数
        /// </summary>
        /// <param name="is_target_method_add_self_arg"></param>
        /// <param name="source_method_owner"></param>
        /// <param name="is_target_method_with_source_args"></param>
        /// <param name="source_method_args"></param>
        /// <returns></returns>
        private object[] GetTargetMethodArgs(bool is_target_method_add_self_arg, object source_method_owner,
            bool is_target_method_with_source_args, params object[] source_method_args)
        {
            var result = new List<object>();

            if (is_target_method_add_self_arg)
                result.Add(source_method_owner);

            if (is_target_method_with_source_args && source_method_args != null && source_method_args.Length > 0)
                result.AddRange(source_method_args);

            return result.ToArray();
        }

        #endregion

        
    }
}