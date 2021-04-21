using System;

namespace CsCat
{
  public class MethodInvoker
  {
    private object target;
    public string method_name;
    private object[] args;
    private Delegate delegation;

    public MethodInvoker(object target, string method_name, params object[] args)
    {
      this.target = target;
      this.method_name = method_name;
      this.args = args;
    }

    public MethodInvoker(Delegate delegation)
    {
      this.delegation = delegation;
    }

    public object Invoke(params object[] args)
    {
      if (args.Length != 0)
        this.args = args;
      //二者只会有一个被调用
      if (delegation != null)
        return delegation.DynamicInvoke(new object[] { this.args });
      else
        return this.target.InvokeMethod<object>(this.method_name, false, this.args);
    }

    public T Invoke<T>(params object[] args)
    {
      return (T)Invoke(args);
    }
  }
}