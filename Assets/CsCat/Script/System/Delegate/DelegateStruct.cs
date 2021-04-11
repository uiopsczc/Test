
using System;

namespace CsCat
{
  /// <summary>
  /// 如何将Delegate作为参数   使用lamba表达式,()=>{} 作为参数要将该delegate设置为Callback等，如 (Action)(()=>{LogCat.Log("helloWorld");})
  /// </summary>
  public class DelegateStruct
  {
    #region field

    public Delegate delegation;
    public object[] args;

    #endregion

    #region ctor

    public DelegateStruct(Delegate delegation, params object[] args)
    {
      this.delegation = delegation;
      this.args = args;
    }

    #endregion

    #region public method

    public object Call()
    {
      return delegation.DynamicInvoke(args);
    }

    #endregion

  }
}


