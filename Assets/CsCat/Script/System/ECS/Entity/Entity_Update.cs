using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		public bool isCanNotUpdate = false;
		public virtual bool IsCanUpdate()
		{
			return !isCanNotUpdate&&isEnabled && !this.isPaused && !IsDestroyed();
		}

		public virtual void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (!this.IsCanUpdate())
				return;

			for (var i = 0; i < _componentPoolItemIndexList.Count; i++)
			{
				var componentPoolItemIndex = _componentPoolItemIndexList[i];
				var component = _GetComponent(componentPoolItemIndex);
				component?.Update(deltaTime, unscaledDeltaTime);
			}
			_Update(deltaTime, unscaledDeltaTime);
		}

		public virtual void FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (!this.IsCanUpdate()) return;
			for (var i = 0; i < _componentPoolItemIndexList.Count; i++)
			{
				var componentPoolItemIndex = _componentPoolItemIndexList[i];
				var component = _GetComponent(componentPoolItemIndex);
				component?.FixedUpdate(deltaTime, unscaledDeltaTime);
			}

			_FixedUpdate(deltaTime, unscaledDeltaTime);
		}


		public virtual void LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (!this.IsCanUpdate()) return;

			for (var i = 0; i < _componentPoolItemIndexList.Count; i++)
			{
				var componentPoolItemIndex = _componentPoolItemIndexList[i];
				var component = _GetComponent(componentPoolItemIndex);
				component?.LateUpdate(deltaTime, unscaledDeltaTime);
			}
			_LateUpdate(deltaTime, unscaledDeltaTime);
		}


		protected virtual bool _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			return true;
		}

		protected virtual bool _FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			return true;
		}

		protected virtual bool _LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			return true;
		}

		void _OnReset_Update()
		{
			isCanNotUpdate = false;
		}

		void _OnDespawn_Update()
		{
			isCanNotUpdate = false;
		}
	}
}