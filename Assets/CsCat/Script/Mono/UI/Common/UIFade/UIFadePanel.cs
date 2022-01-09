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

		private Image fadeImage;

		public void Init(GameObject gameObject)
		{
			base.Init();
			graphicComponent.SetGameObject(gameObject, true);
			graphicComponent.SetIsShow(false);
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			fadeImage = this.frameTransform.Find("fade").GetComponent<Image>();
		}

		public void FadeInOut(float duration, Action callback)
		{
			graphicComponent.SetIsShow(true);
			Sequence sequence = DOTween.Sequence();
			fadeImage.SetColorA(0);
			sequence.Append(fadeImage.DOFade(1, duration * 0.25f)); //透明度从0-1
			sequence.Append(graphicComponent.transform.DOWait(0.45f)); //透明度在1的时候保持X * 0.4秒
			sequence.Append(fadeImage.DOFade(1, duration * 0.3f)); //透明度从1 - 0
			sequence.OnComplete(() => { callback?.Invoke(); });
		}

		public void FadeTo(float toAlpha, float duration, Action callback = null)
		{
			graphicComponent.SetIsShow(true);
			fadeImage.DOFade(toAlpha, duration).OnComplete(() => { callback?.Invoke(); });
		}

		public void FadeTo(float fromAlpha, float toAlpha, float duration, Action callback = null)
		{
			fadeImage.SetColorA(fromAlpha);
			FadeTo(toAlpha, duration, callback);
		}

		protected override void _Reset()
		{
			base._Reset();
			graphicComponent.SetIsShow(false);
			fadeImage.SetColorA(1);
		}

		public void HideFade()
		{
			Reset();
		}
	}
}