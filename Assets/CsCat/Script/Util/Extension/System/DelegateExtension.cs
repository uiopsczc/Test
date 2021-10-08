using System;

namespace CsCat
{
    public static class DelegateExtension
    {
        public static void InvokeIfNotNull(this Delegate self, params object[] delegationArgs)
        {
            self?.DynamicInvoke(delegationArgs);
        }

        public static Delegate InsertFirst(this Delegate self, Delegate firstDelegation)
        {
            return Delegate.Combine(firstDelegation, self);
        }
    }
}