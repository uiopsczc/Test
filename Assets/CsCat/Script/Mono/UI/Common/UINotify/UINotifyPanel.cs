using DG.Tweening;
using UnityEngine.UI;

namespace CsCat
{
	public class UINotifyPanel : UIPanel
	{
		public override EUILayerName layerName => EUILayerName.NotifyUILayer;


		private Text descText;
		private Image bgImage;


		private string desc;
		private bool isMovingUp;
		private bool isRised;
		private float position;
		private bool isCreated;

		private float moveUpDelayDuration = 1f;
		private float closeDelayDuration = 2.8f;

		public void Init(string desc)
		{
			base.Init();
			this.desc = desc;
			graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UINotifyPanel.prefab");
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			descText = graphicComponent.transform.FindComponentInChildren<Text>("desc");
			bgImage = graphicComponent.transform.FindComponentInChildren<Image>("bg");

			this.AddTimer(MoveUp, this.moveUpDelayDuration);
			this.AddTimer((args) =>
			{
				this.Close();
				return false;
			}, this.closeDelayDuration);
			this.isCreated = true;
			if (this.isRised)
				Rise();
		}

		public override void Refresh()
		{
			base.Refresh();
			descText.text = desc;
		}

		public bool MoveUp(params object[] args)
		{
			graphicComponent.transform.DOBlendableMoveYBy(5, 1);
			descText.DOFade(0, 1);
			bgImage.DOFade(0, 1);
			isMovingUp = true;
			return false;
		}

		public void Rise()
		{
			if (!isCreated)
			{
				isRised = true;
				this.position = this.position + 0.5f;
				return;
			}

			if (this.isMovingUp)
				return;

			graphicComponent.transform.DOBlendableMoveYBy(this.position + 0.5f, 0.2f);
			this.position = 0;

		}

		protected override void _Destroy()
		{
			base._Destroy();
			isMovingUp = false;
			isRised = false;
			isCreated = false;
		}
	}
}