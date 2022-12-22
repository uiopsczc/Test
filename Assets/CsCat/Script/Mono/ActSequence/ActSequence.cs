using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class ActSequence : Act
	{
		public List<Act> actList = new List<Act>();
		public int curActIndex;

		public Act curAct => actList[curActIndex];

		public ActSequence()
		{
		}

		public ActSequence(ActSequence sequence) : this(null, sequence, null)
		{
		}

		public ActSequence(MonoBehaviour owner) : this(owner, null)
		{
		}

		public ActSequence(MonoBehaviour owner, ActSequence parent) : this(owner, parent, null)
		{
		}

		public ActSequence(MonoBehaviour owner, ActSequence parent, string id) : base(parent, id)
		{
			_owner = owner;
		}

		public void Init(MonoBehaviour owner, ActSequence parent, string id)
		{
			_owner = owner;
			base.Init(parent, id);
		}


		public override void Start()
		{
			status = Status.Starting;
			var act = actList.First();
			act.Start();
			onStartCallback?.Invoke(this);
			status = Status.Started;
		}

		public override void Update()
		{
			if (status != Status.Started) return;
			curAct.Update();
			onUpdateCallback?.Invoke(this);
		}

		public void Next()
		{
			curActIndex++;
			if (actList.IsNullOrEmpty() || curActIndex == actList.Count)
				Exit();
			else
				curAct.Start();
		}

		public override void Break()
		{
			isBreak = true;
			if (!actList.IsNullOrEmpty())
				curAct.Break();
			Exit();
		}

		public void Clear()
		{
			actList.Clear();
			_Reset();
		}

		protected override void _Reset()
		{
			curActIndex = 0;
			base._Reset();
		}


		#region actList edit

		public Act Append(Act act)
		{
			actList.Add(act);
			return act;
		}

		public Act InsertAt(int index, Act act)
		{
			actList.Insert(index, act);
			return act;
		}

		public Act InsertAt(int index, List<Act> actList)
		{
			this.actList.InsertRange(index, actList);
			return this.actList[index + actList.Count - 1];
		}

		public Act Append(List<Act> actList)
		{
			this.actList.AddRange(actList);
			return this.actList[this.actList.Count - 1];
		}

		public Act AppendActStart(Action<Act> startCallback, bool isExitAtOnce = false)
		{
			var act = new Act(this);
			Append(act);
			act.OnStart(
				thisAct =>
				{
					startCallback(thisAct);
					if (isExitAtOnce)
						thisAct.Exit();
				}
			);
			return act;
		}

		public Act InsertAtActStart(int index, Action<Act> start, bool isExitAtOnce = false)
		{
			var act = new Act(this);
			InsertAt(index, act);
			act.OnStart(
				thisAct =>
				{
					start(thisAct);
					if (isExitAtOnce)
						thisAct.Exit();
				}
			);
			return act;
		}

		#endregion


		#region Util

		public Act Coroutine(IEnumerator iEnumerator)
		{
			var act = new Act(this);
			Append(act);
			act.OnStart(
				thisAct => { owner.StartCoroutine(_IEnumerator(iEnumerator, thisAct.Exit)); }
			);
			return act;
		}

		private IEnumerator _IEnumerator(IEnumerator iEnumerator, Action exit)
		{
			yield return iEnumerator;
			exit();
		}

		/// <summary>
		///   等待一个协程
		/// </summary>
		/// <param name="enumtor"></param>
		/// <returns></returns>
		public Act Coroutine(Coroutine coroutine)
		{
			var act = new Act(this);
			Append(act);
			act.OnStart(
				thisAct => { owner.StartCoroutine(_Coroutine(coroutine, thisAct.Exit)); }
			);
			return act;
		}

		private IEnumerator _Coroutine(Coroutine coroutine, Action exit)
		{
			yield return coroutine;
			exit();
		}

		/// <summary>
		///   等待秒数
		/// </summary>
		/// <param name="second"></param>
		public Act WaitForSeconds(float second)
		{
			return Coroutine(IEWaitForSeconds(second));
		}

		private IEnumerator IEWaitForSeconds(float second)
		{
			yield return new WaitForSeconds(second);
		}

		/// <summary>
		///   等待一定帧数
		/// </summary>
		/// <param name="frameCount"></param>
		/// <returns></returns>
		public Act WaitForFrames(int frameCount)
		{
			return Coroutine(IEWaitForFrames(frameCount));
		}

		private IEnumerator IEWaitForFrames(int frameCount)
		{
			for (var i = 0; i < frameCount; i++)
				yield return null;
		}

		/// <summary>
		///   等到本帧结束
		/// </summary>
		/// <returns></returns>
		public Act WaitForEndOfFrame()
		{
			return Coroutine(IEWaitForEndOfFrame());
		}

		private IEnumerator IEWaitForEndOfFrame()
		{
			yield return new WaitForEndOfFrame();
		}


		public Act WaitUntil(Func<bool> func, float timeout = -1)
		{
			return Coroutine(_IEWaitUtil(func, timeout));
		}

		private IEnumerator _IEWaitUtil(Func<bool> func, float timeout = -1)
		{
			var time = 0f;
			while (!func())
			{
				time += Time.deltaTime;
				if (timeout > 0 && time > timeout)
				{
					LogCat.LogError("[Act:When]A WHEN Timeout!!!");
					break;
				}

				yield return null;
			}
		}

		#endregion

		public override void Despawn()
		{
			base.Despawn();
			actList.Clear();
			curActIndex = 0;
		}
	}
}