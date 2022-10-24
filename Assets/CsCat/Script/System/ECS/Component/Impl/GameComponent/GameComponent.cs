using System;

namespace CsCat
{
	public partial class GameComponent : Component
	{
		protected override void _Reset()
		{
			base._Reset();
			StopAllCoroutines();
			StopAllPausableCoroutines();
			RemoveAllDOTweens();
			RemoveAllTimers();

			RemoveAllListeners();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			StopAllCoroutines();
			StopAllPausableCoroutines();
			RemoveAllDOTweens();
			RemoveAllTimers();

			RemoveAllListeners();
		}
	}
}