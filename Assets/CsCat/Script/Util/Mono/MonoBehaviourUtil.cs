

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public static class MonoBehaviourUtil
  {

    #region IEnumerator

    /// <summary>
    /// 如果ieSave不为null或者没执行完，则停掉ieSave，然后将ieToStart赋值给ieSave，然后开始ieToStart
    /// </summary>
    /// <param name="monoBehaviour"></param>
    /// <param name="save_ie"></param>
    /// <param name="to_start_ie"></param>
    /// <returns></returns>
    public static IEnumerator StopAndStartIEnumerator(MonoBehaviour monoBehaviour, ref IEnumerator save_ie,
      IEnumerator to_start_ie)
    {
      if (save_ie != null)
        monoBehaviour.StopCoroutine(save_ie);
      save_ie = to_start_ie;
      monoBehaviour.StartCoroutine(save_ie);
      return save_ie;
    }

    /// <summary>
    ///  将GetIEnumeratorDict()[ieName]赋值给ieSave，
    ///  然后逻辑跟  StopAndStartCacheIEnumerator(this MonoBehaviour mono, ref IEnumerator ieSave, IEnumerator ieToStart) 一样
    ///  最后需要将StopAndStartCoroutine(this MonoBehaviour mono, ref IEnumerator ieSave, IEnumerator ieToStart)的值赋值给IEnumeratorDict[ieName]，否则IEnumeratorDict[ieName]的值不会变化【被新的值替换】
    ///  例子：  this.StopAndStartCacheIEnumerator("CountNum2", CountNum(100));   CountNum(100)为IEnumerator函数
    /// </summary>
    /// <param name="monoBehaviour"></param>
    /// <param name="ie_name"></param>
    /// <param name="to_start_ie"></param>
    public static void StopAndStartCacheIEnumerator(MonoBehaviour monoBehaviour, string ie_name,
      IEnumerator to_start_ie)
    {
      Dictionary<string, IEnumerator> IEnumeratorDict = monoBehaviour.GetCacheIEnumeratorDict();
      IEnumerator ieSave = IEnumeratorDict.GetOrAddDefault<IEnumerator>(ie_name);
      IEnumeratorDict[ie_name] = monoBehaviour.StopAndStartIEnumerator(ref ieSave, to_start_ie);
    }

    /// <summary>
    /// 停止所有在GetIEnumeratorDict中的IEnumerator
    /// </summary>
    /// <param name="monoBehaviour"></param>
    public static void StopCacheIEnumeratorDict(MonoBehaviour monoBehaviour)
    {
      Dictionary<string, IEnumerator> IEnumeratorDict = monoBehaviour.GetCacheIEnumeratorDict();
      foreach (var ie in IEnumeratorDict.Values)
      {
        if (ie != null)
          monoBehaviour.StopCoroutine(ie);
      }

      IEnumeratorDict.Clear();
    }

    public static void StopCacheIEnumerator(MonoBehaviour monoBehaviour, string name)
    {
      Dictionary<string, IEnumerator> IEnumeratorDict = monoBehaviour.GetCacheIEnumeratorDict();
      if (!IEnumeratorDict.ContainsKey(name))
        return;
      if (IEnumeratorDict[name] == null)
        return;
      monoBehaviour.StopCoroutine(IEnumeratorDict[name]);
      IEnumeratorDict.Remove(name);
    }

    public static void RemoveCacheIEnumerator(MonoBehaviour monoBehaviour, string name)
    {
      Dictionary<string, IEnumerator> IEnumeratorDict = monoBehaviour.GetCacheIEnumeratorDict();
      if (IEnumeratorDict.ContainsKey(name))
        IEnumeratorDict.Remove(name);
    }

    #endregion

    #region PausableCoroutine

    public static PausableCoroutine StartPausableCoroutine(MonoBehaviour monoBehaviour, IEnumerator to_start_ie)
    {
      PausableCoroutine result = PausableCoroutineManager.instance.StartCoroutine(to_start_ie, monoBehaviour);
      return result;
    }

    public static PausableCoroutine StopAndStartPausableCoroutine(MonoBehaviour monoBehaviour,
      ref PausableCoroutine save_co, IEnumerator to_start_ie)
    {
      if (save_co != null)
        PausableCoroutineManager.instance.StopCoroutine(save_co.routine, monoBehaviour);
      PausableCoroutine result = PausableCoroutineManager.instance.StartCoroutine(to_start_ie, monoBehaviour);
      save_co = result;
      return result;
    }

    public static PausableCoroutine StartCachePausableCoroutine(MonoBehaviour monoBehaviour, string ie_name,
      IEnumerator to_start_ie)
    {
      Dictionary<string, PausableCoroutine> PausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
      PausableCoroutineDict[ie_name] = monoBehaviour.StartPausableCoroutine(to_start_ie);
      return PausableCoroutineDict[ie_name];
    }

    public static PausableCoroutine StopAndStartCachePausableCoroutine(MonoBehaviour monoBehaviour, string ie_name,
      IEnumerator to_start_ie)
    {
      Dictionary<string, PausableCoroutine> PausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
      PausableCoroutine save_co = PausableCoroutineDict.GetOrAddDefault<PausableCoroutine>(ie_name);
      PausableCoroutineDict[ie_name] = monoBehaviour.StopAndStartPausableCoroutine(ref save_co, to_start_ie);
      return PausableCoroutineDict[ie_name];
    }

    public static void StopCachePausableCoroutineDict(MonoBehaviour monoBehaviour)
    {
      Dictionary<string, PausableCoroutine> PausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
      foreach (var pausableCoroutine in PausableCoroutineDict.Values)
      {
        if (pausableCoroutine != null)
          PausableCoroutineManager.instance.StopCoroutine(pausableCoroutine.routine, monoBehaviour);
      }

      PausableCoroutineDict.Clear();
    }

    public static void StopCachePausableCoroutine(MonoBehaviour monoBehaviour, string name)
    {
      Dictionary<string, PausableCoroutine> PausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
      if (!PausableCoroutineDict.ContainsKey(name))
        return;
      if (PausableCoroutineDict[name] == null)
        return;
      PausableCoroutineManager.instance.StopCoroutine(PausableCoroutineDict[name].routine, name);
      PausableCoroutineDict.Remove(name);
    }

    public static void RemoveCachePausableCoroutine(MonoBehaviour monoBehaviour, string name)
    {
      Dictionary<string, PausableCoroutine> PausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
      if (PausableCoroutineDict.ContainsKey(name))
        PausableCoroutineDict.Remove(name);
    }

    #endregion

    #region CacheMono

    public static MonoBehaviourCache GetMonoBehaviourCache(this MonoBehaviour monoBehaviour)
    {
      return monoBehaviour.GetPropertyValue<MonoBehaviourCache>(MonoBehaviourCacheConst.MonoBehaviourCache);
    }

    /// <summary>
    /// 在mono中需要有这个属性  protected CacheMono cacheMono=new CacheMono(this);
    /// 获取或者添加cacheMono.dict[dictName]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="monoBehaviour"></param>
    /// <param name="dict_name"></param>
    /// <param name="whenNotContainKey">当monoBehaviourDicts的Key中不包含dictName时的调用的创建方法</param>
    /// <returns></returns>
    public static T GetOrAddCacheDict<T>(MonoBehaviour monoBehaviour, string dict_name, Func<T> whenNotContainKey)
    {
      MonoBehaviourCache monoBehaviourCache = monoBehaviour.GetMonoBehaviourCache();
      //Dictionary<string, object> monoBehaviourDicts = monoBehaviour.GetFieldValue<Dictionary<string, object>>(CacheMonoBehaviourConst.CacheMono);
      //Dictionary<string, object> monoBehaviourDicts = GlobalCache.Instance.Get<Dictionary<string, object>>(monoBehaviour, MonoBehaviourConst.MONOBEHAVIOUR_DICTS);
      return monoBehaviourCache.dict.GetOrAddDefault(dict_name, () => whenNotContainKey());
    }

    /// <summary>
    /// 在mono中需要有这个属性  protected CacheMono cacheMono=new CacheMono(this);
    /// 获取或者添加 cacheMono.dict[dictName]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="monoBehaviour"></param>
    /// <param name="dict_name"></param>
    /// <returns></returns>
    public static T GetOrAddCacheDict<T>(MonoBehaviour monoBehaviour, string dict_name) where T : new()
    {
      return monoBehaviour.GetOrAddCacheDict<T>(dict_name, () => new T());
    }

    /// <summary>
    /// 获取或添加 Dictionary<string, IEnumerator> cacheMono.dict["IEnumeratorDict"]
    /// </summary>
    /// <param name="monoBehaviour"></param>
    /// <returns></returns>
    public static Dictionary<string, IEnumerator> GetCacheIEnumeratorDict(MonoBehaviour monoBehaviour)
    {
      return monoBehaviour.GetOrAddCacheDict<Dictionary<string, IEnumerator>>(MonoBehaviourCacheConst.IEnumeratorDict);
    }

    public static Dictionary<string, PausableCoroutine> GetCachePausableCoroutineDict(MonoBehaviour monoBehaviour)
    {
      return monoBehaviour.GetOrAddCacheDict<Dictionary<string, PausableCoroutine>>(MonoBehaviourCacheConst
        .PausableCoroutineDict);
    }

    #endregion







  }
}