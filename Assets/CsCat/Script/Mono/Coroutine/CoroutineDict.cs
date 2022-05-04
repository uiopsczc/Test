using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class CoroutineDict
	{
		private readonly MonoBehaviour _monoBehaviour;
		private readonly PoolObjectDict<ulong> _idPoolObjectDict = new PoolObjectDict<ulong>(new IdPool());
		private readonly Dictionary<string, IEnumerator> _dict = new Dictionary<string, IEnumerator>();
		

		public CoroutineDict(MonoBehaviour monoBehaviour)
		{
			this._monoBehaviour = monoBehaviour;
		}

		public MonoBehaviour GetMonoBehaviour()
		{
			return this._monoBehaviour;
		}

		public string StartCoroutine(IEnumerator iEnumerator, string key = null)
		{
			key = key ?? _idPoolObjectDict.Get().ToString();
			this._dict[key] = iEnumerator;
			_monoBehaviour.StopAndStartCacheIEnumerator(key.ToGuid(this), iEnumerator);
			return key;
		}

		/// <summary>
		/// 此处的key与StartCoroutine的key保持一致
		/// </summary>
		/// <param name="key"></param>
		public void StopCoroutine(string key)
		{
			if (!this._dict.ContainsKey(key))
				return;
			this._dict.Remove(key);
			if(ulong.TryParse(key, out var id))
				_idPoolObjectDict.Remove(id);
			_monoBehaviour.StopCacheIEnumerator(key.ToGuid(this));
		}

		public void StopAllCoroutines()
		{
			foreach (var keyValue in _dict)
			{
				var key = keyValue.Key;
				_monoBehaviour.StopCacheIEnumerator(key.ToGuid(this));
			}

			_dict.Clear();
			_idPoolObjectDict.Clear();
		}
	}
}