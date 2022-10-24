using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class CoroutineDictComponent : Component
	{
		public CoroutineDict coroutineDict;

		public void Init(CoroutineDict coroutineDict)
		{
			base.Init();
			this.coroutineDict = coroutineDict;
		}

		public string StartCoroutine(IEnumerator iEnumerator, string key = null)
		{
			return coroutineDict.StartCoroutine(iEnumerator, key);
		}

		/// <summary>
		/// 此处的key与StartCoroutine的key保持一致
		/// </summary>
		/// <param name="key"></param>
		public void StopCoroutine(string key)
		{
			coroutineDict.StopCoroutine(key);
		}

		public void StopAllCoroutines()
		{
			coroutineDict.StopAllCoroutines();
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