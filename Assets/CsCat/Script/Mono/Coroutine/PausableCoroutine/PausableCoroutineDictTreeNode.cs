using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class PausableCoroutineDictTreeNode : TreeNode
	{
		public PausableCoroutineDict _pausableCoroutineDict;

		protected void _Init(PausableCoroutineDict pausableCoroutineDict)
		{
			base._Init();
			this._pausableCoroutineDict = pausableCoroutineDict;
		}

		public string StartCoroutine(IEnumerator iEnumerator, string key = null)
		{
			return _pausableCoroutineDict.StartCoroutine(iEnumerator, key);
		}

		/// <summary>
		/// 此处的key与StartCoroutine的key保持一致
		/// </summary>
		/// <param name="key"></param>
		public void StopCoroutine(string key)
		{
			_pausableCoroutineDict.StopCoroutine(key);
		}

		public void StopAllCoroutines()
		{
			_pausableCoroutineDict.StopAllCoroutines();
		}

		protected override void _SetIsPaused(bool isPaused)
		{
			base._SetIsPaused(isPaused);
			_pausableCoroutineDict.SetIsPaused(isPaused);
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