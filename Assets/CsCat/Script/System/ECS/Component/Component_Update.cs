using System;

namespace CsCat
{
	public partial class Component
	{
		
		public bool isCanNotUpdate = false;

		
		public virtual bool IsCanUpdate()
		{
			return !isCanNotUpdate && isEnabled && !isPaused && !IsDestroyed();
		}

		public virtual void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (this.IsCanUpdate())
				_Update(deltaTime, unscaledDeltaTime);
		}

		public virtual void FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (this.IsCanUpdate())
				_FixedUpdate(deltaTime, unscaledDeltaTime);
		}


		public virtual void LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (this.IsCanUpdate())
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