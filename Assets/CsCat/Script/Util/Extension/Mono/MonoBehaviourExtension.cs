

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public static class MonoBehaviourExtensions
  {

    #region IEnumerator

    /// <summary>
    ///  将GetIEnumeratorDict()[ieName]赋值给ieSave，
    ///  然后逻辑跟  StopAndStartCacheIEnumerator(this MonoBehaviour mono, ref IEnumerator ieSave, IEnumerator ieToStart) 一样
    ///  最后需要将StopAndStartCoroutine(this MonoBehaviour mono, ref IEnumerator ieSave, IEnumerator ieToStart)的值赋值给IEnumeratorDict[ieName]，否则IEnumeratorDict[ieName]的值不会变化【被新的值替换】
    ///  例子：  this.StopAndStartCacheIEnumerator("CountNum2", CountNum(100));   CountNum(100)为IEnumerator函数
    /// </summary>
    /// <param name="self"></param>
    /// <param name="ie_name"></param>
    /// <param name="to_start_IE"></param>
    public static void StopAndStartCacheIEnumerator(this MonoBehaviour self, string ie_name, IEnumerator to_start_IE)
    {
      MonoBehaviourUtil.StopAndStartCacheIEnumerator(self, ie_name, to_start_IE);
    }

    /// <summary>
    /// 如果ieSave不为null或者没执行完，则停掉ieSave，然后将ieToStart赋值给ieSave，然后开始ieToStart
    /// </summary>
    /// <param name="self"></param>
    /// <param name="save_IE"></param>
    /// <param name="to_start_IE"></param>
    /// <returns></returns>
    public static IEnumerator StopAndStartIEnumerator(this MonoBehaviour self, ref IEnumerator save_IE,
      IEnumerator to_start_IE)
    {
      return MonoBehaviourUtil.StopAndStartIEnumerator(self, ref save_IE, to_start_IE);
    }



    /// <summary>
    /// 停止所有在GetIEnumeratorDict中的IEnumerator
    /// </summary>
    /// <param name="self"></param>
    public static void StopCacheIEnumeratorDict(this MonoBehaviour self)
    {
      MonoBehaviourUtil.StopCacheIEnumeratorDict(self);
    }

    public static void StopCacheIEnumerator(this MonoBehaviour self, string name)
    {
      MonoBehaviourUtil.StopCacheIEnumerator(self, name);
    }

    public static void RemoveCacheIEnumerator(this MonoBehaviour self, string name)
    {
      MonoBehaviourUtil.RemoveCacheIEnumerator(self, name);
    }

    #endregion

    #region PausableCoroutine

    public static PausableCoroutine StartPausableCoroutine(this MonoBehaviour self, IEnumerator to_start_IE)
    {
      return MonoBehaviourUtil.StartPausableCoroutine(self, to_start_IE);
    }

    public static PausableCoroutine StartCachePausableCoroutine(this MonoBehaviour self, string ie_name,
      IEnumerator to_start_IE)
    {
      return MonoBehaviourUtil.StartCachePausableCoroutine(self, ie_name, to_start_IE);
    }

    public static PausableCoroutine StopAndStartCachePausableCoroutine(this MonoBehaviour self, string ie_name,
      IEnumerator to_start_IE)
    {
      return MonoBehaviourUtil.StopAndStartCachePausableCoroutine(self, ie_name, to_start_IE);
    }

    public static PausableCoroutine StopAndStartPausableCoroutine(this MonoBehaviour self,
      ref PausableCoroutine save_co, IEnumerator to_start_IE)
    {
      return MonoBehaviourUtil.StopAndStartPausableCoroutine(self, ref save_co, to_start_IE);
    }

    public static void StopCachePausableCoroutineDict(this MonoBehaviour self)
    {
      MonoBehaviourUtil.StopCachePausableCoroutineDict(self);
    }

    public static void StopCachePausableCoroutine(this MonoBehaviour self, string name)
    {
      MonoBehaviourUtil.StopCachePausableCoroutine(self, name);
    }

    public static void RemoveCachePausableCoroutine(this MonoBehaviour self, string name)
    {
      MonoBehaviourUtil.RemoveCachePausableCoroutine(self, name);
    }

    #endregion

    #region monoBehaviourDicts

    /// <summary>
    /// 在mono中需要有这个属性 protected CacheMono cacheMono=new CacheMono(this);
    /// 获取或者添加cacheMono.dict[dictName]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="dict_name"></param>
    /// <param name="whenNotContainKey">当monoBehaviourDicts的Key中不包含dictName时的调用的创建方法</param>
    /// <returns></returns>
    public static T GetOrAddCacheDict<T>(this MonoBehaviour self, string dict_name, Func<T> whenNotContainKey)
    {
      return MonoBehaviourUtil.GetOrAddCacheDict(self, dict_name, whenNotContainKey);
    }

    /// <summary>
    /// 在mono中需要有这个属性  protected CacheMono cacheMono=new CacheMono(this);
    /// 获取或者添加cacheMono.dict[dictName]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="dict_name"></param>
    /// <returns></returns>
    public static T GetOrAddCacheDict<T>(this MonoBehaviour self, string dict_name) where T : new()
    {
      return MonoBehaviourUtil.GetOrAddCacheDict<T>(self, dict_name);
    }

    /// <summary>
    /// 获取或添加 Dictionary<string, IEnumerator>cacheMono.dict["IEnumeratorDict"]
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Dictionary<string, IEnumerator> GetCacheIEnumeratorDict(this MonoBehaviour self)
    {
      return MonoBehaviourUtil.GetCacheIEnumeratorDict(self);
    }

    public static Dictionary<string, PausableCoroutine> GetCachePausableCoroutineDict(this MonoBehaviour self)
    {
      return MonoBehaviourUtil.GetCachePausableCoroutineDict(self);
    }

    #endregion







  }
}