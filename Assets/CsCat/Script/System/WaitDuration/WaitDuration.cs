using System;
using System.Collections;
using UnityEngine;

namespace CsCat
{
	//为Pause设计
	public class WaitDuration
	{
		#region delegate

		/// <summary>
		///   更新
		/// </summary>
		public Action<WaitDuration> onUpdate;

		#endregion

		#region field

		/// <summary>
		///   用于计算时间差的开始时间，可以改变其值
		/// </summary>
		public float startTimeToCalc;


		/// <summary>
		///   时长
		/// </summary>
		private readonly float _duration;

		/// <summary>
		///   不可以改变的值，构造函数中指定
		/// </summary>
		private float _startTime;

		#endregion

		#region public method

		public WaitDuration(float duration, Action<WaitDuration> onUpdate)
		{
			this._duration = duration;
			this.onUpdate = onUpdate;
		}


		public IEnumerator Start()
		{
			_startTime = Time.realtimeSinceStartup;
			startTimeToCalc = _startTime;
			while (Time.realtimeSinceStartup - startTimeToCalc < _duration)
			{
				onUpdate?.Invoke(this);
				yield return null;
			}
		}

		#endregion
	}
}