using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UIFadePanel : UIPanel
	{
		public override bool isResident => true;

		public override EUILayerName layerName => EUILayerName.FadeUILayer;

		private Image _ImgC_Fade;

		protected void _Init(GameObject gameObject)
		{
			base._Init();
			this.AddChild<DOTweenDictTreeNode>(null, new DOTweenDict());
			_SetGameObject(gameObject, true);
		}

		protected override void _PostInit()
		{
			base._PostInit();
			this.SetIsShow(false);
		}

		protected override void _InitGameObjectChildren()
		{
			base._InitGameObjectChildren();
			_ImgC_Fade = this._frameTransform.Find("ImgC_Fade").GetComponent<Image>();
		}

		public void FadeInOut(float duration, Action callback)
		{
			SetIsShow(true);
			Sequence sequence = this.GetChild<DOTweenDictTreeNode>().AddDOTweenSequence(null);
			_ImgC_Fade.SetColorA(0);
			sequence.Append(_ImgC_Fade.DOFade(1, duration * 0.25f)); //透明度从0-1
			sequence.Append(this.GetTransform().DOWait(0.45f)); //透明度在1的时候保持X * 0.4秒
			sequence.Append(_ImgC_Fade.DOFade(1, duration * 0.3f)); //透明度从1 - 0
			sequence.OnComplete(() => { callback?.Invoke(); });
		}

		public void FadeTo(float toAlpha, float duration, Action callback = null)
		{
			SetIsShow(true);
			_ImgC_Fade.DOFade(toAlpha, duration).OnComplete(() => { callback?.Invoke(); });
		}

		public void FadeTo(float fromAlpha, float toAlpha, float duration, Action callback = null)
		{
			_ImgC_Fade.SetColorA(fromAlpha);
			FadeTo(toAlpha, duration, callback);
		}

		protected override void _Reset()
		{
			_ImgC_Fade.SetColorA(1);
			base._Reset();
		}

		public void HideFade()
		{
			DoReset();
		}
	}
}