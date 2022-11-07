using System.Collections;

namespace CsCat
{
	public class CoroutineDictComponent : Component
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