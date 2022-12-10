using UnityEngine;

namespace CsCat
{
	public class UIWaitingPanel : UIPanel
	{
		public override bool isResident => true;

		public override EUILayerName layerName => EUILayerName.WaitingUILayer;

		private int _waitingCount = 0;
		private Animation _waitingAinimation;
		private Transform _Nego_Waiting;

		protected void _Init(GameObject gameObject)
		{
			base._Init();
			DoSetGameObject(gameObject);
		}

		protected override void _PostInit()
		{
			base._PostInit();
			this.SetIsShow(false);
		}

		protected override void _InitGameObjectChildren()
		{
			base._InitGameObjectChildren();
			_Nego_Waiting = _frameTransform.Find("Nego_Waiting");
			_waitingAinimation = _Nego_Waiting.GetComponent<Animation>();
		}

		public void StartWaiting()
		{
			this._waitingCount += 1;
			SetIsShow(true);
		}

		public void EndWaiting()
		{
			this._waitingCount -= 1;
			if (this._waitingCount <= 0)
			{
				this._waitingCount = 0;
				SetIsShow(false);
			}
		}


		protected override void _Reset()
		{
			this._waitingCount = 0;
			base._Reset();
		}

		public void HideWaiting()
		{
			DoReset();
		}

		protected override void _DestroyGameObject()
		{
		}
	}
}