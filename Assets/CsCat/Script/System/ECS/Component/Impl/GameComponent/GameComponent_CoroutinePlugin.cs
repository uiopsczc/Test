using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
	public partial class GameComponent
	{
		protected CoroutineDict CoroutineDict => cache.GetOrAddDefault(() =>
			new CoroutineDict(GetGameEntity().GetComponent<CoroutineDictComponent>().coroutineDict.GetMonoBehaviour()));


		public string StartCoroutine(IEnumerator ie, string key = null)
		{
			return CoroutineDict.StartCoroutine(ie, key);
		}

		/// <summary>
		/// 此处的key与StartCoroutine的key保持一致
		/// </summary>
		/// <param name="key"></param>
		public void StopCoroutine(string key)
		{
			CoroutineDict.StopCoroutine(key);
		}

		public void StopAllCoroutines()
		{
			if (cache.ContainsKey<CoroutineDict>())
				CoroutineDict.StopAllCoroutines();
		}
	}
}