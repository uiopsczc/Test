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
		bool is_destroy_while_end;

		#endregion

		#region property

		private MonoBehaviourCache _monoBehaviourCache;

		public MonoBehaviourCache monoBehaviourCache =>
		  _monoBehaviourCache ?? (_monoBehaviourCache = new MonoBehaviourCache(this));

		#endregion

		#region delegate

		Action start_callback;
		Action update_callback;
		Action end_callback;

		#endregion

		#region public method

		/// <summary>
		/// 
		/// </summary>
		/// <param name="duration">时间</param>
		/// <param name="delay">延迟</param>
		/// <param name="start_callback">开始时执行</param>
		/// <param name="update_callback">每帧执行</param>
		/// <param name="end_callback">结束时执行</param>
		/// <param name="is_destroy_while_end">结束时是否移除对象</param>
		public void StartIE(float duration, float delay = 0, Action start_callback = null,
		  Action update_callback = null, Action end_callback = null, bool is_destroy_while_end = true)
		{
			this.start_callback = start_callback;
			this.update_callback = update_callback;
			this.end_callback = end_callback;
			this.duration = duration;
			this.delay = delay;
			this.is_destroy_while_end = is_destroy_while_end;
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
			start_callback?.Invoke();
			yield return ExecuteUpdate();
			end_callback?.Invoke();
			if (is_destroy_while_end)
				this.Destroy();

			yield return 0;
		}

		IEnumerator WaitForDelay()
		{
			float delay_remain_duration = delay;
			while (delay_remain_duration > 0)
			{
				if (Pause.instance.isPaused)
				{
					yield return null;
					continue;
				}

				yield return null;
				delay_remain_duration -= Time.deltaTime;

			}
		}

		IEnumerator ExecuteUpdate()
		{
			float exe_remain_duration = duration;
			while (exe_remain_duration > 0)
			{
				if (Pause.instance.isPaused)
				{
					yield return null;
					continue;
				}

				yield return null;
				update_callback?.Invoke();
				exe_remain_duration -= Time.deltaTime;
			}
		}

		#endregion

	}
}