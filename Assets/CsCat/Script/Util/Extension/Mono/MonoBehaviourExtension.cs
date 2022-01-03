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
		/// <param name="ieName"></param>
		/// <param name="toStartIE"></param>
		public static void StopAndStartCacheIEnumerator(this MonoBehaviour self, string ieName,
			IEnumerator toStartIE)
		{
			MonoBehaviourUtil.StopAndStartCacheIEnumerator(self, ieName, toStartIE);
		}

		/// <summary>
		/// 如果ieSave不为null或者没执行完，则停掉ieSave，然后将ieToStart赋值给ieSave，然后开始ieToStart
		/// </summary>
		/// <param name="self"></param>
		/// <param name="saveIE"></param>
		/// <param name="toStartIE"></param>
		/// <returns></returns>
		public static IEnumerator StopAndStartIEnumerator(this MonoBehaviour self, ref IEnumerator saveIE,
			IEnumerator toStartIE)
		{
			return MonoBehaviourUtil.StopAndStartIEnumerator(self, ref saveIE, toStartIE);
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

		public static PausableCoroutine StartPausableCoroutine(this MonoBehaviour self, IEnumerator toStartIE)
		{
			return MonoBehaviourUtil.StartPausableCoroutine(self, toStartIE);
		}

		public static PausableCoroutine StartCachePausableCoroutine(this MonoBehaviour self, string ieName,
			IEnumerator toStartIE)
		{
			return MonoBehaviourUtil.StartCachePausableCoroutine(self, ieName, toStartIE);
		}

		public static PausableCoroutine StopAndStartCachePausableCoroutine(this MonoBehaviour self, string ie_name,
			IEnumerator toStartIE)
		{
			return MonoBehaviourUtil.StopAndStartCachePausableCoroutine(self, ie_name, toStartIE);
		}

		public static PausableCoroutine StopAndStartPausableCoroutine(this MonoBehaviour self,
			ref PausableCoroutine saveCo, IEnumerator toStartIE)
		{
			return MonoBehaviourUtil.StopAndStartPausableCoroutine(self, ref saveCo, toStartIE);
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
		/// <param name="dictName"></param>
		/// <param name="whenNotContainKey">当monoBehaviourDicts的Key中不包含dictName时的调用的创建方法</param>
		/// <returns></returns>
		public static T GetOrAddCacheDict<T>(this MonoBehaviour self, string dictName, Func<T> whenNotContainKey)
		{
			return MonoBehaviourUtil.GetOrAddCacheDict(self, dictName, whenNotContainKey);
		}

		/// <summary>
		/// 在mono中需要有这个属性  protected CacheMono cacheMono=new CacheMono(this);
		/// 获取或者添加cacheMono.dict[dictName]
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <param name="dictName"></param>
		/// <returns></returns>
		public static T GetOrAddCacheDict<T>(this MonoBehaviour self, string dictName) where T : new()
		{
			return MonoBehaviourUtil.GetOrAddCacheDict<T>(self, dictName);
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