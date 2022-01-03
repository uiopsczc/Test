using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class ActSequence : Act
	{
		public List<Act> act_list = new List<Act>();
		public int cur_act_index;

		public Act cur_act => act_list[cur_act_index];

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
			var act = act_list.First();
			act.Start();
			on_start_callback?.Invoke(this);
			status = Status.Started;
		}

		public override void Update()
		{
			if (status != Status.Started) return;
			cur_act.Update();
			on_update_callback?.Invoke(this);
		}

		public void Next()
		{
			cur_act_index++;
			if (act_list.IsNullOrEmpty() || cur_act_index == act_list.Count)
				Exit();
			else
				cur_act.Start();
		}

		public override void Break()
		{
			is_break = true;
			if (!act_list.IsNullOrEmpty())
				cur_act.Break();
			Exit();
		}

		public void Clear()
		{
			act_list.Clear();
			Reset();
		}

		protected override void Reset()
		{
			cur_act_index = 0;
			base.Reset();
		}


		#region actList edit

		public Act Append(Act act)
		{
			act_list.Add(act);
			return act;
		}

		public Act InsertAt(int index, Act act)
		{
			act_list.Insert(index, act);
			return act;
		}

		public Act InsertAt(int index, List<Act> act_list)
		{
			this.act_list.InsertRange(index, act_list);
			return this.act_list[index + act_list.Count - 1];
		}

		public Act Append(List<Act> act_list)
		{
			this.act_list.AddRange(act_list);
			return this.act_list[this.act_list.Count - 1];
		}

		public Act AppendActStart(Action<Act> start_callback, bool is_exit_at_once = false)
		{
			var act = new Act(this);
			Append(act);
			act.OnStart(
			  thisAct =>
			  {
				  start_callback(thisAct);
				  if (is_exit_at_once)
					  thisAct.Exit();
			  }
			);
			return act;
		}

		public Act InsertAtActStart(int index, Action<Act> start, bool is_exit_at_once = false)
		{
			var act = new Act(this);
			InsertAt(index, act);
			act.OnStart(
			  this_act =>
			  {
				  start(this_act);
				  if (is_exit_at_once)
					  this_act.Exit();
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
			  this_act => { owner.StartCoroutine(_IEnumerator(iEnumerator, this_act.Exit)); }
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
			  this_act => { owner.StartCoroutine(_Coroutine(coroutine, this_act.Exit)); }
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
		/// <param name="frame_count"></param>
		/// <returns></returns>
		public Act WaitForFrames(int frame_count)
		{
			return Coroutine(IEWaitForFrames(frame_count));
		}

		private IEnumerator IEWaitForFrames(int frame_count)
		{
			for (var i = 0; i < frame_count; i++)
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
			return Coroutine(IEWaitUtil(func, timeout));
		}

		private IEnumerator IEWaitUtil(Func<bool> func, float timeout = -1)
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

		public override void OnDespawn()
		{
			base.OnDespawn();
			act_list.Clear();
			cur_act_index = 0;
		}
	}
}