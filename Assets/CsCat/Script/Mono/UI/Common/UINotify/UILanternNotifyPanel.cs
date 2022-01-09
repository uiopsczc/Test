using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	// 走马灯效果
	public class UILanternNotifyPanel : UIPanel
	{
		public override bool isResident => true;

		public override EUILayerName layerName => EUILayerName.NotifyUILayer;

		private Text descText;
		private RectTransform descRectTransform;
		private RectTransform maskRectTransform;

		private Sequence sequence;

		private float moveToCenterDuration = 1f;
		private float stayCenterDuration = 1f;
		private float moveToEndDuration = 1f;

		public void Init(GameObject gameObject)
		{
			base.Init();
			graphicComponent.SetGameObject(gameObject, true);
			graphicComponent.SetIsShow(false);
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			descText = this.frameTransform.FindComponentInChildren<Text>("mask/desc");
			descRectTransform = descText.GetComponent<RectTransform>();
			maskRectTransform = this.frameTransform.FindComponentInChildren<RectTransform>("mask");
		}

		public void Show(string desc)
		{
			graphicComponent.SetIsShow(true);
			descText.text = desc;
			LayoutRebuilder.ForceRebuildLayoutImmediate(descRectTransform); //计算desc_rtf的长度
			descRectTransform.SetAnchoredPositionX(maskRectTransform.sizeDelta.x / 2 + descRectTransform.sizeDelta.x / 2);
			sequence = DOTween.Sequence();
			sequence.Append(descRectTransform.DOMoveX(0, moveToCenterDuration));
			sequence.Append(descRectTransform.DOWait(stayCenterDuration));
			sequence.Append(descRectTransform.DOAnchorPosX(
			  -maskRectTransform.sizeDelta.x / 2 - descRectTransform.sizeDelta.x, moveToEndDuration));
			sequence.OnComplete(() => { Reset(); });
		}

		protected override void _Reset()
		{
			base._Reset();
			graphicComponent.SetIsShow(false);
			Client.instance.uiManager.uiNotifyManager.__LanternNotify();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			sequence?.Kill();
		}
	}
}