using System;
using System.Collections.Generic;

namespace CsCat
{
  public class ResLoadDataInfo
  {
    public ResLoadData resLoadData;
    public Dictionary<object, bool> callback_cause_dict = new Dictionary<object, bool>();
    private bool is_not_check_destroy;

    public ResLoadDataInfo(ResLoadData resLoadData, bool is_not_check_destroy)
    {
      this.resLoadData = resLoadData;
      this.is_not_check_destroy = is_not_check_destroy;
    }

    public void AddCallbackCause(object callback_cause)
    {
      if (this.callback_cause_dict.ContainsKey(callback_cause.GetNotNullKey()))//不重复
        return;
      this.callback_cause_dict[callback_cause] = true;
    }

    //callback_cause==null时是全部删除
    public void RemoveCallbackCause(object callback_cause)
    {
      this.callback_cause_dict.Remove(callback_cause.GetNotNullKey());
      this.resLoadData.assetCat.RemoveCallback(callback_cause.GetNullableKey());
      if (!is_not_check_destroy)
        CheckDestroy();
    }

    public void RemoveAllCallbackCauses()
    {
      foreach (var callback_cuase in callback_cause_dict.Keys)
        this.resLoadData.assetCat.RemoveCallback(callback_cuase.GetNullableKey());
      this.callback_cause_dict.Clear();
      if (!is_not_check_destroy)
        CheckDestroy();
    }



    void CheckDestroy()
    {
      if (this.callback_cause_dict.Count == 0)
        resLoadData.Destroy();
    }

    public void Destroy()
    {
      foreach (var callback_cuase in callback_cause_dict.Keys)
        this.resLoadData.assetCat.RemoveCallback(callback_cuase.GetNullableKey());
      this.callback_cause_dict.Clear();
      this.resLoadData.Destroy();
    }
  }
}