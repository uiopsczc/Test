using UnityEngine;

namespace CsCat
{
	public class UIWaitingPanel : UIPanel
	{
		public override bool isResident => true;

		public override EUILayerName layerName => EUILayerName.WaitingUILayer;

		private int waitingCount = 0;
		private Animation waitingAinimation;
		private GameObject waitingGameObject;

		public void Init(GameObject gameObject)
		{
			base.Init();
			graphicComponent.SetGameObject(gameObject, true);
			graphicComponent.SetIsShow(false);
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			waitingGameObject = frameTransform.Find("waiting").gameObject;
			waitingAinimation = waitingGameObject.GetComponent<Animation>();
		}

		public void StartWaiting()
		{
			this.waitingCount += 1;
			graphicComponent.SetIsShow(true);
		}

		public void EndWaiting()
		{
			this.waitingCount -= 1;
			if (this.waitingCount <= 0)
			{
				this.waitingCount = 0;
				graphicComponent.SetIsShow(false);
			}
		}


		protected override void _Reset()
		{
			base._Reset();
			this.waitingCount = 0;
		}

		public void HideWaiting()
		{
			Reset();
			graphicComponent.SetIsShow(false);
		}




	}
}