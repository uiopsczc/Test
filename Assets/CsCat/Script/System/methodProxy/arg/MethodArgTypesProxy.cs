using System;
using System.Collections.Generic;

namespace CsCat
{
  public class MethodArgTypesProxy
  {
    public MethodArgTypesProxy(Type source_type, bool is_target_method_add_self_arg,
      bool is_target_method_with_source_args,
      params Type[] sourceMethodArgTypes)
    {
      this.is_target_method_add_self_arg = is_target_method_add_self_arg;
      this.is_target_method_with_source_args = is_target_method_with_source_args;

      source_method_arg_types = sourceMethodArgTypes;
      this.source_type = source_type;

      SetTargetMethodArgTypes();
    }

    /// <summary>
    ///   是否目标参数带有原函数所在的类的引用
    /// </summary>
    public bool is_target_method_add_self_arg { get; }

    /// <summary>
    ///   是否目标参数带有原函数的参数列表
    /// </summary>
    public bool is_target_method_with_source_args { get; }

    /// <summary>
    ///   目标函数参数类型列表
    /// </summary>
    public Type[] target_method_arg_types { get; private set; }

    /// <summary>
    ///   原函数参数类型列表
    /// </summary>
    public Type[] source_method_arg_types { get; }

    /// <summary>
    ///   原函数所在类
    /// </summary>
    public Type source_type { get; }

    /// <summary>
    ///   设置目标参数类型列表
    /// </summary>
    private void SetTargetMethodArgTypes()
    {
      var result = new List<Type>();
      if (is_target_method_add_self_arg)
        result.Add(source_type);
      if (is_target_method_with_source_args && source_method_arg_types != null && source_method_arg_types.Length > 0)
        result.AddRange(source_method_arg_types);
      target_method_arg_types = result.ToArray();
    }
  }
}