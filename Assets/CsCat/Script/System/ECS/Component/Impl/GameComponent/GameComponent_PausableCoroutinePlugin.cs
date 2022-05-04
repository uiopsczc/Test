using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
	public partial class GameComponent
	{
		protected PausableCoroutineDict PausableCoroutineDict => cache.GetOrAddDefault(() =>
			new PausableCoroutineDict(GetGameEntity().GetComponent<PausableCoroutineDictComponent>()
				.pausableCoroutineDict._monoBehaviour));


		public string StartPausableCoroutine(IEnumerator ie, string key = null)
		{
			return PausableCoroutineDict.StartCoroutine(ie, key);
		}

		/// <summary>
		/// 此处的key与StartCoroutine的key保持一致
		/// </summary>
		/// <param name="key"></param>
		public void StopPausableCoroutine(string key)
		{
			PausableCoroutineDict.StopCoroutine(key);
		}

		public void StopAllPausableCoroutines()
		{
			if (cache.ContainsKey<PausableCoroutineDict>())
				PausableCoroutineDict.StopAllCoroutines();
		}

		public void SetIsPaused_PausableCoroutines(bool isPaused)
		{
			PausableCoroutineDict.SetIsPaused(isPaused);
		}
	}
}