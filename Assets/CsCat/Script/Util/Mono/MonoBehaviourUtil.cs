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
        /// <param name="saveEnumerator"></param>
        /// <param name="toStartEnumerator"></param>
        /// <returns></returns>
        public static IEnumerator StopAndStartIEnumerator(MonoBehaviour monoBehaviour, ref IEnumerator saveEnumerator,
            IEnumerator toStartEnumerator)
        {
            if (saveEnumerator != null)
                monoBehaviour.StopCoroutine(saveEnumerator);
            saveEnumerator = toStartEnumerator;
            monoBehaviour.StartCoroutine(saveEnumerator);
            return saveEnumerator;
        }

        /// <summary>
        ///  将GetIEnumeratorDict()[ieName]赋值给ieSave，
        ///  然后逻辑跟  StopAndStartCacheIEnumerator(this MonoBehaviour mono, ref IEnumerator ieSave, IEnumerator ieToStart) 一样
        ///  最后需要将StopAndStartCoroutine(this MonoBehaviour mono, ref IEnumerator ieSave, IEnumerator ieToStart)的值赋值给IEnumeratorDict[ieName]，否则IEnumeratorDict[ieName]的值不会变化【被新的值替换】
        ///  例子：  this.StopAndStartCacheIEnumerator("CountNum2", CountNum(100));   CountNum(100)为IEnumerator函数
        /// </summary>
        /// <param name="monoBehaviour"></param>
        /// <param name="enumeratorName"></param>
        /// <param name="toStartEnumerator"></param>
        public static void StopAndStartCacheIEnumerator(MonoBehaviour monoBehaviour, string enumeratorName,
            IEnumerator toStartEnumerator)
        {
            Dictionary<string, IEnumerator> enumeratorDict = monoBehaviour.GetCacheIEnumeratorDict();
            IEnumerator saveEnumerator = enumeratorDict.GetOrAddDefault<IEnumerator>(enumeratorName);
            enumeratorDict[enumeratorName] =
                monoBehaviour.StopAndStartIEnumerator(ref saveEnumerator, toStartEnumerator);
        }

        /// <summary>
        /// 停止所有在GetIEnumeratorDict中的IEnumerator
        /// </summary>
        /// <param name="monoBehaviour"></param>
        public static void StopCacheIEnumeratorDict(MonoBehaviour monoBehaviour)
        {
            Dictionary<string, IEnumerator> enumeratorDict = monoBehaviour.GetCacheIEnumeratorDict();
            foreach (var enumerator in enumeratorDict.Values)
            {
                if (enumerator == null) continue;
                monoBehaviour.StopCoroutine(enumerator);
            }

            enumeratorDict.Clear();
        }

        public static void StopCacheIEnumerator(MonoBehaviour monoBehaviour, string name)
        {
            Dictionary<string, IEnumerator> enumeratorDict = monoBehaviour.GetCacheIEnumeratorDict();
            if (!enumeratorDict.ContainsKey(name))
                return;
            if (enumeratorDict[name] == null)
                return;
            monoBehaviour.StopCoroutine(enumeratorDict[name]);
            enumeratorDict.Remove(name);
        }

        public static void RemoveCacheIEnumerator(MonoBehaviour monoBehaviour, string name)
        {
            Dictionary<string, IEnumerator> enumeratorDict = monoBehaviour.GetCacheIEnumeratorDict();
            if (enumeratorDict.ContainsKey(name))
                enumeratorDict.Remove(name);
        }

        #endregion

        #region PausableCoroutine

        public static PausableCoroutine StartPausableCoroutine(MonoBehaviour monoBehaviour,
            IEnumerator toStartEnumerator)
        {
            PausableCoroutine result =
                PausableCoroutineManager.instance.StartCoroutine(toStartEnumerator, monoBehaviour);
            return result;
        }

        public static PausableCoroutine StopAndStartPausableCoroutine(MonoBehaviour monoBehaviour,
            ref PausableCoroutine saveEnumerator, IEnumerator toStartEnumerator)
        {
            if (saveEnumerator != null)
                PausableCoroutineManager.instance.StopCoroutine(saveEnumerator.routine, monoBehaviour);
            PausableCoroutine result =
                PausableCoroutineManager.instance.StartCoroutine(toStartEnumerator, monoBehaviour);
            saveEnumerator = result;
            return result;
        }

        public static PausableCoroutine StartCachePausableCoroutine(MonoBehaviour monoBehaviour, string enumeratorName,
            IEnumerator toStartEnumerator)
        {
            Dictionary<string, PausableCoroutine> pausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
            pausableCoroutineDict[enumeratorName] = monoBehaviour.StartPausableCoroutine(toStartEnumerator);
            return pausableCoroutineDict[enumeratorName];
        }

        public static PausableCoroutine StopAndStartCachePausableCoroutine(MonoBehaviour monoBehaviour,
            string enumeratorName,
            IEnumerator toStartEnumerator)
        {
            Dictionary<string, PausableCoroutine> pausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
            PausableCoroutine saveEnumerator = pausableCoroutineDict.GetOrAddDefault<PausableCoroutine>(enumeratorName);
            pausableCoroutineDict[enumeratorName] =
                monoBehaviour.StopAndStartPausableCoroutine(ref saveEnumerator, toStartEnumerator);
            return pausableCoroutineDict[enumeratorName];
        }

        public static void StopCachePausableCoroutineDict(MonoBehaviour monoBehaviour)
        {
            Dictionary<string, PausableCoroutine> pausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
            foreach (var pausableCoroutine in pausableCoroutineDict.Values)
                if (pausableCoroutine != null)
                    PausableCoroutineManager.instance.StopCoroutine(pausableCoroutine.routine, monoBehaviour);

            pausableCoroutineDict.Clear();
        }

        public static void StopCachePausableCoroutine(MonoBehaviour monoBehaviour, string name)
        {
            Dictionary<string, PausableCoroutine> pausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
            if (!pausableCoroutineDict.ContainsKey(name))
                return;
            if (pausableCoroutineDict[name] == null)
                return;
            PausableCoroutineManager.instance.StopCoroutine(pausableCoroutineDict[name].routine, name);
            pausableCoroutineDict.Remove(name);
        }

        public static void RemoveCachePausableCoroutine(MonoBehaviour monoBehaviour, string name)
        {
            Dictionary<string, PausableCoroutine> pausableCoroutineDict = monoBehaviour.GetCachePausableCoroutineDict();
            if (pausableCoroutineDict.ContainsKey(name))
                pausableCoroutineDict.Remove(name);
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
        /// <param name="dictName"></param>
        /// <param name="whenNotContainKeyFunc">当monoBehaviourDicts的Key中不包含dictName时的调用的创建方法</param>
        /// <returns></returns>
        public static T GetOrAddCacheDict<T>(MonoBehaviour monoBehaviour, string dictName,
            Func<T> whenNotContainKeyFunc)
        {
            MonoBehaviourCache monoBehaviourCache = monoBehaviour.GetMonoBehaviourCache();
            return monoBehaviourCache.dict.GetOrAddDefault(dictName, () => whenNotContainKeyFunc());
        }

        /// <summary>
        /// 在mono中需要有这个属性  protected CacheMono cacheMono=new CacheMono(this);
        /// 获取或者添加 cacheMono.dict[dictName]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="monoBehaviour"></param>
        /// <param name="dictName"></param>
        /// <returns></returns>
        public static T GetOrAddCacheDict<T>(MonoBehaviour monoBehaviour, string dictName) where T : new()
        {
            return monoBehaviour.GetOrAddCacheDict(dictName, () => new T());
        }

        /// <summary>
        /// 获取或添加 Dictionary<string, IEnumerator> cacheMono.dict["IEnumeratorDict"]
        /// </summary>
        /// <param name="monoBehaviour"></param>
        /// <returns></returns>
        public static Dictionary<string, IEnumerator> GetCacheIEnumeratorDict(MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.GetOrAddCacheDict<Dictionary<string, IEnumerator>>(MonoBehaviourCacheConst
                .IEnumeratorDict);
        }

        public static Dictionary<string, PausableCoroutine> GetCachePausableCoroutineDict(MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.GetOrAddCacheDict<Dictionary<string, PausableCoroutine>>(MonoBehaviourCacheConst
                .PausableCoroutineDict);
        }

        #endregion
    }
}