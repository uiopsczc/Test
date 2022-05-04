using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class PausableCoroutineDictComponent : AbstractComponent
	{
		public PausableCoroutineDict pausableCoroutineDict;

		public void Init(PausableCoroutineDict pausableCoroutineDict)
		{
			base.Init();
			this.pausableCoroutineDict = pausableCoroutineDict;
		}

		public string StartCoroutine(IEnumerator iEnumerator, string key = null)
		{
			return pausableCoroutineDict.StartCoroutine(iEnumerator, key);
		}

		/// <summary>
		/// 此处的key与StartCoroutine的key保持一致
		/// </summary>
		/// <param name="key"></param>
		public void StopCoroutine(string key)
		{
			pausableCoroutineDict.StopCoroutine(key);
		}

		public void StopAllCoroutines()
		{
			pausableCoroutineDict.StopAllCoroutines();
		}

		protected override void _SetIsPaused(bool isPaused)
		{
			base._SetIsPaused(isPaused);
			pausableCoroutineDict.SetIsPaused(isPaused);
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