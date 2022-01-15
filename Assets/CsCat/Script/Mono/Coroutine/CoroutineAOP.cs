using System;
using UnityEngine;
using System.Collections;


namespace CsCat
{
	public class CoroutineAOP : MonoBehaviour
	{
		#region field

		float duration;
		float delay;
		bool isDestroyWhileEnd;

		#endregion

		#region property

		private MonoBehaviourCache _monoBehaviourCache;

		public MonoBehaviourCache monoBehaviourCache =>
			_monoBehaviourCache ?? (_monoBehaviourCache = new MonoBehaviourCache(this));

		#endregion

		#region delegate

		Action startCallback;
		Action updateCallback;
		Action endCallback;

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
			this.startCallback = startCallback;
			this.updateCallback = updateCallback;
			this.endCallback = endCallback;
			this.duration = duration;
			this.delay = delay;
			this.isDestroyWhileEnd = isDestroyWhileEnd;
			this.StopAndStartCacheIEnumerator("ProcessIE", ProcessIE());
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

		IEnumerator ProcessIE()
		{
			yield return WaitForDelay();
			startCallback?.Invoke();
			yield return ExecuteUpdate();
			endCallback?.Invoke();
			if (isDestroyWhileEnd)
				this.Destroy();

			yield return 0;
		}

		IEnumerator WaitForDelay()
		{
			float delayRemainDuration = delay;
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

		IEnumerator ExecuteUpdate()
		{
			float exeRemainDuration = duration;
			while (exeRemainDuration > 0)
			{
				if (Pause.instance.isPaused)
				{
					yield return null;
					continue;
				}

				yield return null;
				updateCallback?.Invoke();
				exeRemainDuration -= Time.deltaTime;
			}
		}

		#endregion
	}
}