using System;
using UnityEngine;

namespace CsCat
{
	public partial class Act : IDespawn
	{
		protected MonoBehaviour _owner;
		public string id;
		public bool isBreak;
		public ActSequence parent;
		public Status status;

		protected MonoBehaviour owner
		{
			get
			{
				if (_owner != null)
					return _owner;
				var current = parent;
				while (current.owner == null)
					current = current.parent;
				return current.owner;
			}
		}

		public Action<Action> onExitCallback; //参数是用于跳到下一个act    (参数next)
		public Action onPreExitCallback;
		public Action<Act> onStartCallback;
		public Action onPreStartCallback;
		public Action<Act> onUpdateCallback;


		public Act()
		{
		}

		public Act(ActSequence parent)
		{
			Init(parent);
		}

		public Act(ActSequence parent, string id) : this(parent)
		{
			Init(parent, id);
		}

		public void Init(ActSequence parent, string id = null)
		{
			this.parent = parent;
			this.id = id;
			status = Status.Ready;
		}


		public Act OnPreStart(Action onPreStartCallback, bool isAppend = true)
		{
			if (isAppend)
				this.onPreStartCallback += onPreStartCallback;
			else
				this.onPreStartCallback.InsertFirst(onPreStartCallback);
			return this;
		}

		public Act OnStart(Action<Act> onStartCallback, bool isAppend = true)
		{
			if (isAppend)
				this.onStartCallback += onStartCallback;
			else
				this.onStartCallback.InsertFirst(onStartCallback);
			return this;
		}

		public Act OnUpdate(Action<Act> onUpdateCallback, bool isAppend = true)
		{
			if (isAppend)
				this.onUpdateCallback += onUpdateCallback;
			else
				this.onUpdateCallback.InsertFirst(onUpdateCallback);
			return this;
		}

		public Act OnPreExit(Action onPreExitCallback, bool isAppend = true)
		{
			if (isAppend)
				this.onPreExitCallback += onPreExitCallback;
			else
				this.onPreExitCallback.InsertFirst(onPreExitCallback);
			return this;
		}

		public Act OnExit(Action<Action> onExitCallback, bool isAppend = true)
		{
			if (isAppend)
				this.onExitCallback += onExitCallback;
			else
				this.onExitCallback.InsertFirst(onExitCallback);
			return this;
		}


		public virtual void Start()
		{
			status = Status.Starting;
			onPreStartCallback?.Invoke();
			onStartCallback?.Invoke(this);
		}

		public virtual void Update()
		{
			if (status == Status.Started) onUpdateCallback?.Invoke(this);
		}

		public virtual void Exit()
		{
			status = Status.Exiting;
			onPreExitCallback?.Invoke();

			Action parentNextAction = () => { };
			if (parent != null)
				parentNextAction = parent.Next;

			if (isBreak)
				parentNextAction = () => { };


			if (onExitCallback != null)
				onExitCallback(parentNextAction);
			else
				parentNextAction();
			status = Status.Exited;

			_Reset();
		}

		protected virtual void _Reset()
		{
			status = Status.Ready;
			isBreak = false;
		}

		public virtual void Break()
		{
			isBreak = true;
			Exit();
		}


		public virtual void Despawn()
		{
			_owner = null;
			id = null;
			isBreak = false;
			parent = null;
			status = Status.Ready;

			onExitCallback = null;
			onPreExitCallback = null;
			onStartCallback = null;
			onPreStartCallback = null;
			onUpdateCallback = null;
		}
	}
}