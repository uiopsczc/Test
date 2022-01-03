using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class CoroutinePluginComponent : AbstractComponent
	{
		public CoroutinePlugin coroutinePlugin;
		public void Init(CoroutinePlugin coroutinePlugin)
		{
			base.Init();
			this.coroutinePlugin = coroutinePlugin;
		}

		public string StartCoroutine(IEnumerator ie, string key = null)
		{
			return coroutinePlugin.StartCoroutine(ie, key);
		}

		/// <summary>
		/// 此处的key与StartCoroutine的key保持一致
		/// </summary>
		/// <param name="key"></param>
		public void StopCoroutine(string key)
		{
			coroutinePlugin.StopCoroutine(key);

		}

		public void StopAllCoroutines()
		{
			coroutinePlugin.StopAllCoroutines();
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