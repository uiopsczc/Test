using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
	public partial class GameComponent
	{
		protected PausableCoroutinePlugin pausableCoroutinePlugin => cache.GetOrAddDefault(() =>
			new PausableCoroutinePlugin(GetGameEntity().GetComponent<PausableCoroutinePluginComponent>()
				.pausableCoroutinePlugin.mono));


		public string StartPausableCoroutine(IEnumerator ie, string key = null)
		{
			return pausableCoroutinePlugin.StartCoroutine(ie, key);
		}

		/// <summary>
		/// 此处的key与StartCoroutine的key保持一致
		/// </summary>
		/// <param name="key"></param>
		public void StopPausableCoroutine(string key)
		{
			pausableCoroutinePlugin.StopCoroutine(key);
		}

		public void StopAllPausableCoroutines()
		{
			if (cache.ContainsKey<PausableCoroutinePlugin>())
				pausableCoroutinePlugin.StopAllCoroutines();
		}

		public void SetIsPaused_PausableCoroutines(bool isPaused)
		{
			pausableCoroutinePlugin.SetIsPaused(isPaused);
		}
	}
}