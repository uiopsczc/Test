using System.Collections.Generic;

namespace CsCat
{
	public class HFSMComponent<T> : Component where T : HFSM
	{
		public T hfsm;

		protected void _Init(T hfsm)
		{
			base._Init();
			this.hfsm = hfsm;
		}

		protected override void _SetIsPaused(bool isPaused)
		{
			base._SetIsPaused(isPaused);
			this.hfsm.SetIsPaused(true, true);
		}

		protected override void _SetIsEnabled(bool isEnabled)
		{
			base._SetIsEnabled(isEnabled);
			this.hfsm.SetIsEnabled(true, true);
		}

		protected override void _Reset()
		{
			this.hfsm.Reset(true);
			base._Reset();
		}

		protected override void _Destroy()
		{
			this.hfsm.Destroy();
			base._Destroy();
		}

		public override void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base.Update(deltaTime, unscaledDeltaTime);
			this.hfsm.CheckDestroyed();
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			this.hfsm.Update(deltaTime, unscaledDeltaTime);
		}

		protected override void _FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._FixedUpdate(deltaTime, unscaledDeltaTime);
			this.hfsm.FixedUpdate(deltaTime, unscaledDeltaTime);
		}

		protected override void _LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._LateUpdate(deltaTime, unscaledDeltaTime);
			this.hfsm.LateUpdate(deltaTime, unscaledDeltaTime);
		}
	}
}