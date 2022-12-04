using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
	{
		protected bool _isCanNotUpdate = false;
		public virtual bool IsCanUpdate()
		{
			return !_isCanNotUpdate&&isEnabled && !this.isPaused && !IsDestroyed();
		}

		protected virtual bool IsNeedUpdateChildren()
		{
			return true;
		}

		public virtual void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (!this.IsCanUpdate())
				return;

			if (IsNeedUpdateChildren())
			{
				for (var i = 0; i < _childPoolItemIndexList.Count; i++)
				{
					var childPoolItemIndex = _childPoolItemIndexList[i];
					var child = _GetChild(childPoolItemIndex);
					child?.Update(deltaTime, unscaledDeltaTime);
				}
			}
			_Update(deltaTime, unscaledDeltaTime);
		}

		public virtual void FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (!this.IsCanUpdate()) return;
			if (IsNeedUpdateChildren())
			{
				for (var i = 0; i < _childPoolItemIndexList.Count; i++)
				{
					var childPoolItemIndex = _childPoolItemIndexList[i];
					var child = _GetChild(childPoolItemIndex);
					child?.FixedUpdate(deltaTime, unscaledDeltaTime);
				}
			}
			_FixedUpdate(deltaTime, unscaledDeltaTime);
		}


		public virtual void LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (!this.IsCanUpdate()) return;
			if (IsNeedUpdateChildren())
			{
				for (var i = 0; i < _childPoolItemIndexList.Count; i++)
				{
					var childPoolItemIndex = _childPoolItemIndexList[i];
					var child = _GetChild(childPoolItemIndex);
					child?.LateUpdate(deltaTime, unscaledDeltaTime);
				}
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
			_isCanNotUpdate = false;
		}

		void _OnDespawn_Update()
		{
			_isCanNotUpdate = false;
		}
	}
}