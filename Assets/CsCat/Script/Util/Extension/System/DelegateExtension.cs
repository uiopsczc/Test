using System;

namespace CsCat
{
  public static class DelegateExtension
  {
    public static void InvokeIfNotNull(this Delegate self, params object[] delegation_args)
    {
      self?.DynamicInvoke(delegation_args);

    }

    public static Delegate InsertHead(this Delegate self, Delegate head_delegation)
    {
      return Delegate.Combine(head_delegation, self);
    }


  }
}