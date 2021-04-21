using System;

namespace CsCat
{
  public partial class DelegateUtil
  {
    public static Delegate CreateGenericAction(Type[] generic_types, object target, string method_name)
    {
      Type action_type = typeof(Action<>).MakeGenericType(generic_types);

      var target_methodInfo = target.GetGenericMethodInfo2(method_name, generic_types);
      Delegate result = Delegate.CreateDelegate(action_type, (target is Type) ? null : target, target_methodInfo);
      return result;
    }
    public static Delegate CreateGenericFunc(Type[] generic_types, object target, string method_name)
    {
      Type action_type = typeof(Func<>).MakeGenericType(generic_types);

      var target_methodInfo = target.GetGenericMethodInfo2(method_name, generic_types);
      Delegate result = Delegate.CreateDelegate(action_type, (target is Type) ? null : target, target_methodInfo);
      return result;
    }


    /// <summary>
    /// 调用并删除（有先后顺序，由isRemoveBeforeInvoke决定）
    /// 如果Delegate中的参数最后一个参数的名称为remove则该delegate会被删除  代码在 DelegateInfo
    /// </summary>
    public static void InvokeThenRemove(ref Delegate delegation, params object[] delegation_args)
    {
      if (delegation == null)
        return;
      delegation.DynamicInvoke(delegation_args);
      DelegateInfo delegateInfo = DelegateInfo.GetDelegateInfo(delegation);
      RemoveDelegate(ref delegation, delegateInfo.to_remove);
    }


    public static void InvokeThenRemove<T0>(ref Action<T0> delegation)
    {
      Delegate d = delegation;
      InvokeThenRemove(ref d);
    }

    public static void InvokeThenRemove<T0, T1>(ref Action<T0, T1> delegation, T0 args0)
    {
      Delegate d = delegation;
      InvokeThenRemove(ref d, args0);
    }

    public static void InvokeThenRemove<T0, T1, T2>(ref Action<T0, T1, T2> delegation, T0 args0, T1 args1)
    {
      Delegate d = delegation;
      InvokeThenRemove(ref d, args0, args1);
    }

    public static void InvokeThenRemove<T0, T1, T2, T3>(ref Action<T0, T1, T2, T3> delegation, T0 args0, T1 args1,
      T2 args2)
    {
      Delegate d = delegation;
      InvokeThenRemove(ref d, args0, args1, args2);
    }

    private static void RemoveDelegate(ref Delegate delegation, Delegate delegate_to_remove)
    {
      for (int i = delegate_to_remove.GetInvocationList().Length - 1; i >= 0; i--)
      {
        delegation = Delegate.Remove(delegation, delegate_to_remove.GetInvocationList()[i]);
      }
    }
  }
}