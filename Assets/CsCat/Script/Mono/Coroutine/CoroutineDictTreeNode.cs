using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class CoroutineDictTreeNode : TreeNode
	{
		private CoroutineDict _coroutineDict;

		protected void _Init(CoroutineDict coroutineDict)
		{
			base._Init();
			this._coroutineDict = coroutineDict;
		}

		public string StartCoroutine(IEnumerator iEnumerator, string key = null)
		{
			return _coroutineDict.StartCoroutine(iEnumerator, key);
		}

		/// <summary>
		/// 此处的key与StartCoroutine的key保持一致
		/// </summary>
		/// <param name="key"></param>
		public void StopCoroutine(string key)
		{
			_coroutineDict.StopCoroutine(key);
		}

		public void StopAllCoroutines()
		{
			_coroutineDict.StopAllCoroutines();
		}

		protected override void _Reset()
		{
			StopAllCoroutines();
			base._Reset();
		}

		protected override void _Destroy()
		{
			StopAllCoroutines();
			base._Destroy();
		}
	}
}