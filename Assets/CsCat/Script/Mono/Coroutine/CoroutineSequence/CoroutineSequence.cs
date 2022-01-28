using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	/// <summary>
	///   链式操作，结合协程和DOTween
	///   适合做动画、结合协程、回调一堆的情况
	/// </summary>
	/// <example>
	///   CoroutineSequence.Start(doSomething)
	///   .WaitForSeconds(1f)
	///   .Coroutine(CGame.Instance.StartCoroutine(enumtor))
	///   .Then(logSomething)
	///   .Then((next)=>{
	///   DOTween.DO(tween).OnComplete(next);
	///   });
	///   .When(()=> booleanVarTrue)
	///   .Then(()=>{
	///   // Over!
	///   });
	/// </example>
	public class CoroutineSequence
	{
		private Queue<Action<Action>> callbackQueue;
		private bool isCanNext;
		private readonly MonoBehaviour owner;

		private CoroutineSequence(MonoBehaviour owner)
		{
			this.owner = owner;
			isCanNext = true;
		}

		public bool isFinished { get; private set; }

		public Coroutine WaitFinish()
		{
			return owner.StartCoroutine(IEWaitFinish());
		}

		private IEnumerator IEWaitFinish()
		{
			while (!isFinished)
				yield return null;
		}


		public static CoroutineSequence Start(MonoBehaviour owner)
		{
			var async = new CoroutineSequence(owner);
			return async;
		}

		public static CoroutineSequence Start(MonoBehaviour owner, Action callback)
		{
			var async = Start(owner);
			return async.Then(callback);
		}

		public static CoroutineSequence Start(MonoBehaviour owner, Action<Action, Action> then_callback)
		{
			var async = Start(owner);
			return async.Then(then_callback);
		}

		public CoroutineSequence Then(Action callback)
		{
			WaitNext(next =>
			{
				callback();
				next();
			});
			return this;
		}

		public CoroutineSequence Then(Action<Action> callback)
		{
			WaitNext(next => callback(next));
			return this;
		}

		public CoroutineSequence Then(Action<Action, Action> thenCallback)
		{
			WaitNext(next => { thenCallback(next, () => { LogCat.LogError("TODO: kill!"); }); });
			return this;
		}

		private void WaitNext(Action<Action> callback)
		{
			if (!isCanNext)
			{
				if (callbackQueue == null)
					callbackQueue = new Queue<Action<Action>>();
				callbackQueue.Enqueue(callback);
			}
			else
			{
				isCanNext = false;
				callback(Next);
			}
		}

		private void Next()
		{
			isCanNext = true;
			if (callbackQueue != null && callbackQueue.Count > 0)
				WaitNext(callbackQueue.Dequeue());
			else
				isFinished = true;
		}

		#region Util

		/// <summary>
		///   开启并等待一个协程
		/// </summary>
		/// <param name="enumtor"></param>
		/// <returns></returns>
		public CoroutineSequence Coroutine(IEnumerator enumtor)
		{
			WaitNext(next => { owner.StartCoroutine(_IEnumerator(enumtor, next)); });
			return this;
		}

		private IEnumerator _IEnumerator(IEnumerator enumtor, Action next)
		{
			yield return owner.StartCoroutine(enumtor);
			next();
		}

		/// <summary>
		///   等待一个已经被其它MonoBehaviour开启的协程
		/// </summary>
		/// <param name="co"></param>
		/// <returns></returns>
		public CoroutineSequence Coroutine(Coroutine co)
		{
			WaitNext(next => { owner.StartCoroutine(_Coroutine(co, next)); });
			return this;
		}

		private IEnumerator _Coroutine(Coroutine co, Action next)
		{
			yield return co;
			next();
		}

		/// <summary>
		///   等待一定帧数
		/// </summary>
		/// <param name="frameCount"></param>
		/// <returns></returns>
		public CoroutineSequence WaitForFrames(int frameCount)
		{
			return Coroutine(IEWaitForFrames(frameCount));
		}

		private IEnumerator IEWaitForFrames(int frameCount)
		{
			for (var i = 0; i < frameCount; i++)
				yield return null;
		}

		/// <summary>
		///   等待秒数
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public CoroutineSequence WaitForSeconds(float second)
		{
			return Coroutine(IEWaitForSeconds(second));
		}

		private IEnumerator IEWaitForSeconds(float second)
		{
			yield return new WaitForSeconds(second);
		}

		/// <summary>
		///   等到本帧结束
		/// </summary>
		/// <returns></returns>
		public CoroutineSequence WaitForEndOfFrame()
		{
			return Coroutine(IEWaitForEndOfFrame());
		}

		private IEnumerator IEWaitForEndOfFrame()
		{
			yield return new WaitForEndOfFrame();
		}

		public CoroutineSequence Until(Func<bool> func, float timeout = -1)
		{
			return Coroutine(IEUtil(func, timeout));
		}

		private IEnumerator IEUtil(Func<bool> func, float timeout = -1)
		{
			var time = 0f;
			while (!func())
			{
				time += Time.deltaTime;
				if (time > 0 && time > timeout)
				{
					LogCat.LogError("[Async:When]A WHEN Timeout!!!");
					break;
				}

				yield return null;
			}
		}

		#endregion
	}
}