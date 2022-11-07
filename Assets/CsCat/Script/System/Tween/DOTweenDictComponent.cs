using System.Collections.Generic;
using DG.Tweening;

namespace CsCat
{
	public class DOTweenDictComponent : Component
	{
		private DOTweenDict _dotweenDict;

		protected void _Init(DOTweenDict dotweenDict)
		{
			base._Init();
			this._dotweenDict = dotweenDict;
		}

		public Sequence AddDOTweenSequence(string key)
		{
			return _dotweenDict.AddDOTweenSequence(key);
		}

		public Tween AddDOTween(string key, Tween tween)
		{
			return _dotweenDict.AddDOTween(key, tween);
		}

		public void RemoveDOTween(string key)
		{
			_dotweenDict.RemoveDOTween(key);
		}

		public void RemoveDOTween(Tween tween)
		{
			_dotweenDict.RemoveDOTween(tween);
		}


		protected override void _SetIsPaused(bool isPaused)
		{
			base._SetIsPaused(isPaused);
			_dotweenDict.SetIsPaused(isPaused);
		}

		public void RemoveDOTweens()
		{
			this._dotweenDict.RemoveDOTweens();
		}

		protected override void _Reset()
		{
			base._Reset();
			RemoveDOTweens();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			RemoveDOTweens();
		}
	}
}