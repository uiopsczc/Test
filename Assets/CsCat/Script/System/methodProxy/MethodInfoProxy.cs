using System;
using System.Reflection;

namespace CsCat
{
  public class MethodInfoProxy
  {
    public MethodInfoProxy(string target_method_name, Type target_type, Type source_type, bool is_target_add_self_arg,
      bool is_target_with_source_args, params Type[] source_arg_types)
    {
      this.target_method_name = target_method_name;
      this.target_type = target_type;
      methodArgTypesProxy =
        new MethodArgTypesProxy(source_type, is_target_add_self_arg, is_target_with_source_args, source_arg_types);
    }

    /// <summary>
    ///   是否目标参数带有原函数所在的类的引用
    /// </summary>
    public bool is_target_method_add_self_arg => methodArgTypesProxy.is_target_method_add_self_arg;

    /// <summary>
    ///   是否目标参数带有原函数的参数列表
    /// </summary>
    public bool is_target_method_with_source_args => methodArgTypesProxy.is_target_method_with_source_args;

    /// <summary>
    ///   目标函数名称
    /// </summary>
    public string target_method_name { get; }

    /// <summary>
    ///   目标参数列表代理
    /// </summary>
    public MethodArgTypesProxy methodArgTypesProxy { get; }

    /// <summary>
    ///   目标类
    /// </summary>
    public Type target_type { get; }

    /// <summary>
    ///   原函数所在类
    /// </summary>
    public Type source_type => methodArgTypesProxy.source_type;

    /// <summary>
    ///   获取目标函数
    /// </summary>
    /// <param name="bindingFlags"></param>
    /// <returns></returns>
    public MethodInfo GetTargetMethodInfo(BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      return target_type.GetMethodInfo(target_method_name, bindingFlags, methodArgTypesProxy.target_method_arg_types);
    }
  }
}