using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class PausableCoroutinePluginComponent : AbstractComponent
	{
		public PausableCoroutinePlugin pausableCoroutinePlugin;

		public void Init(PausableCoroutinePlugin pausableCoroutinePlugin)
		{
			base.Init();
			this.pausableCoroutinePlugin = pausableCoroutinePlugin;
		}

		public string StartCoroutine(IEnumerator ie, string key = null)
		{
			return pausableCoroutinePlugin.StartCoroutine(ie, key);
		}

		/// <summary>
		/// 此处的key与StartCoroutine的key保持一致
		/// </summary>
		/// <param name="key"></param>
		public void StopCoroutine(string key)
		{
			pausableCoroutinePlugin.StopCoroutine(key);
		}

		public void StopAllCoroutines()
		{
			pausableCoroutinePlugin.StopAllCoroutines();
		}

		protected override void _SetIsPaused(bool isPaused)
		{
			base._SetIsPaused(isPaused);
			pausableCoroutinePlugin.SetIsPaused(isPaused);
		}

		protected override void _Reset()
		{
			base._Reset();
			StopAllCoroutines();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			StopAllCoroutines();
		}
	}
}