using System;
using System.Reflection;

namespace CsCat
{
  public partial class DelegateUtil
  {
    #region 私有方法

    private class DelegateInfo
    {
      public Delegate to_remove = null;
      public Delegate remain = null;

      private DelegateInfo(Delegate remain)
      {
        this.remain = remain;
      }

      //如果Delegate中的参数最后一个参数的名称为remove则该delegate会被删除
      public static DelegateInfo GetDelegateInfo(Delegate sources)
      {
        DelegateInfo result = new DelegateInfo(sources);
        if (sources == null)
          return result;
        for (int i = sources.GetInvocationList().Length - 1; i >= 0; i--)
        {
          SetDelegateInfo(result, sources.GetInvocationList()[i]);
        }

        return result;
      }

      private static void SetDelegateInfo(DelegateInfo delegateInfo, Delegate delegate_to_remove)
      {
        ParameterInfo[] delegate_parameterInfos = delegate_to_remove.Method.GetParameters();
        if (delegate_parameterInfos.Length == 0)
          return;
        //最后一个参数的名称不为remove开始的话，则不用处理
        if (!delegate_parameterInfos[delegate_parameterInfos.Length - 1].Name.StartsWith("remove"))
          return;
        delegateInfo.to_remove = Delegate.Combine(delegateInfo.to_remove, delegate_to_remove);
        delegateInfo.remain = Delegate.Remove(delegateInfo.remain, delegate_to_remove);
      }
    }

    #endregion
  }
}