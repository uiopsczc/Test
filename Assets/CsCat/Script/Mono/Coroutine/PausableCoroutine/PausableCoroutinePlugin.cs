using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class PausableCoroutinePlugin
	{
		public MonoBehaviour mono;
		private IdPool idPool = new IdPool();
		private Dictionary<string, PausableCoroutine> dict = new Dictionary<string, PausableCoroutine>();

		public PausableCoroutinePlugin(MonoBehaviour mono)
		{
			this.mono = mono;
		}

		public string StartCoroutine(IEnumerator ie, string key = null)
		{
			CleanFinishedCoroutines();
			key = key ?? idPool.Get().ToString();
			var coroutine = mono.StopAndStartCachePausableCoroutine(key.ToGuid(this), ie);
			this.dict[key] = coroutine;
			return key;
		}

		/// <summary>
		/// 此处的key与StartCoroutine的key保持一致
		/// </summary>
		/// <param name="key"></param>
		public void StopCoroutine(string key)
		{
			CleanFinishedCoroutines();
			if (!this.dict.ContainsKey(key))
				return;
			this.dict.Remove(key);
			idPool.Despawn(key);
			mono.StopCachePausableCoroutine(key.ToGuid(this));
		}

		public void StopAllCoroutines()
		{
			foreach (var keyValue in dict)
			{
				var key = keyValue.Key;
				mono.StopCachePausableCoroutine(key.ToGuid(this));
			}

			dict.Clear();
			idPool.DespawnAll();
		}

		public void SetIsPaused(bool isPaused)
		{
			CleanFinishedCoroutines();
			foreach (var keyValue in dict)
			{
				var key = keyValue.Key;
				this.dict[key].SetIsPaused(isPaused);
			}
		}

		void CleanFinishedCoroutines()
		{
			dict.RemoveByFunc<string, PausableCoroutine>((key, coroutine) =>
			{
				if (coroutine.isFinished)
				{
					idPool.Despawn(key);
					return true;
				}

				return false;
			});
		}
	}
}