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

		private Text _TxtC_Desc;
		private RectTransform _RectTransform_Desc;
		private RectTransform _RectTransform_Mask;

		private float moveToCenterDuration = 1f;
		private float stayCenterDuration = 1f;
		private float moveToEndDuration = 1f;

		protected void Init(GameObject gameObject)
		{
			base._Init();
			_SetGameObject(gameObject, true);
			this.AddChild<DOTweenDictTreeNode>(null, new DOTweenDict());
		}

		protected override void _PostInit()
		{
			base._PostInit();
			this.SetIsShow(false);
		}

		protected override void _InitGameObjectChildren()
		{
			base._InitGameObjectChildren();
			_TxtC_Desc = this._frameTransform.Find("Nego_Mask/TxtC_Desc").GetComponent<Text>();
			_RectTransform_Desc = _TxtC_Desc.GetComponent<RectTransform>();
			_RectTransform_Mask = this._frameTransform.Find("Nego_Mask").GetComponent< RectTransform>();
		}

		public void Show(string desc)
		{
			SetIsShow(true);
			_TxtC_Desc.text = desc;
			LayoutRebuilder.ForceRebuildLayoutImmediate(_RectTransform_Desc); //计算desc_rtf的长度
			_RectTransform_Desc.SetAnchoredPositionX(_RectTransform_Mask.sizeDelta.x / 2 + _RectTransform_Desc.sizeDelta.x / 2);

			var sequence = this.GetChild<DOTweenDictTreeNode>().AddDOTweenSequence(null);
			sequence.Append(_RectTransform_Desc.DOMoveX(0, moveToCenterDuration));
			sequence.Append(_RectTransform_Desc.DOWait(stayCenterDuration));
			sequence.Append(_RectTransform_Desc.DOAnchorPosX(
			  -_RectTransform_Mask.sizeDelta.x / 2 - _RectTransform_Desc.sizeDelta.x, moveToEndDuration));
			sequence.OnComplete(DoReset);
		}

		protected override void _Reset()
		{
			base._Reset();
			Client.instance.uiManager.uiNotifyManager.LanternNotify();
		}
	}
}