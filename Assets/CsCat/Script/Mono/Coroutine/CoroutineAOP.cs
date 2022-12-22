using System;
using UnityEngine;
using System.Collections;


namespace CsCat
{
	public class CoroutineAOP : MonoBehaviour
	{
		#region field

		float _duration;
		float _delay;
		bool _isDestroyWhileEnd;

		#endregion

		#region property

		private MonoBehaviourCache _monoBehaviourCache;

		public MonoBehaviourCache monoBehaviourCache =>
			_monoBehaviourCache ?? (_monoBehaviourCache = new MonoBehaviourCache(this));

		#endregion

		#region delegate

		Action _startCallback;
		Action _updateCallback;
		Action _endCallback;

		#endregion

		#region public method

		/// <summary>
		/// 
		/// </summary>
		/// <param name="duration">时间</param>
		/// <param name="delay">延迟</param>
		/// <param name="startCallback">开始时执行</param>
		/// <param name="updateCallback">每帧执行</param>
		/// <param name="endCallback">结束时执行</param>
		/// <param name="isDestroyWhileEnd">结束时是否移除对象</param>
		public void StartIE(float duration, float delay = 0, Action startCallback = null,
			Action updateCallback = null, Action endCallback = null, bool isDestroyWhileEnd = true)
		{
			this._startCallback = startCallback;
			this._updateCallback = updateCallback;
			this._endCallback = endCallback;
			this._duration = duration;
			this._delay = delay;
			this._isDestroyWhileEnd = isDestroyWhileEnd;
			this.StopAndStartCacheIEnumerator("ProcessIE", _ProcessIE());
		}

		/// <summary>
		/// 终止协程
		/// </summary>
		public void StopIE()
		{
			this.StopCacheIEnumerator("ProcessIE");
		}

		#endregion

		#region private method

		IEnumerator _ProcessIE()
		{
			yield return _WaitForDelay();
			_startCallback?.Invoke();
			yield return _ExecuteUpdate();
			_endCallback?.Invoke();
			if (_isDestroyWhileEnd)
				this.Destroy();

			yield return 0;
		}

		IEnumerator _WaitForDelay()
		{
			float delayRemainDuration = _delay;
			while (delayRemainDuration > 0)
			{
				if (Pause.instance.isPaused)
				{
					yield return null;
					continue;
				}

				yield return null;
				delayRemainDuration -= Time.deltaTime;
			}
		}

		IEnumerator _ExecuteUpdate()
		{
			float exeRemainDuration = _duration;
			while (exeRemainDuration > 0)
			{
				if (Pause.instance.isPaused)
				{
					yield return null;
					continue;
				}

				yield return null;
				_updateCallback?.Invoke();
				exeRemainDuration -= Time.deltaTime;
			}
		}

		#endregion
	}
}