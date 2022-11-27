using UnityEngine;

namespace CsCat
{
	public class UIWaitingPanel : UIPanel
	{
		public override bool isResident => true;

		public override EUILayerName layerName => EUILayerName.WaitingUILayer;

		private int _waitingCount = 0;
		private Animation _waitingAinimation;
		private GameObject _waitingGameObject;

		protected void _Init(GameObject gameObject)
		{
			base._Init();
			SetGameObject(gameObject, true);
		}

		protected override void _PostInit()
		{
			base._PostInit();
			this.SetIsShow(false);
		}

		protected override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			_waitingGameObject = _frameTransform.Find("waiting").gameObject;
			_waitingAinimation = _waitingGameObject.GetComponent<Animation>();
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
			base._Reset();
			this._waitingCount = 0;
		}

		public void HideWaiting()
		{
			Reset();
			SetIsShow(false);
		}
	}
}