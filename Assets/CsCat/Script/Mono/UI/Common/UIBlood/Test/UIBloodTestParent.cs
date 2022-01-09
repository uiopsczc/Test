using System;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
	public partial class UIBloodTestParent : GameEntity
	{
		public UIBlood uiBlood;

		public void Init(string name, int sliderCount)
		{
			base.Init();
			var rectTransform = GameObject.Find("UITestPanel").NewChildWithRectTransform(name);
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.sizeDelta = Vector2.zero;
			graphicComponent.SetGameObject(rectTransform.gameObject, false);
			this.uiBlood = Client.instance.uiManager.AddUIBlood(graphicComponent.transform, 150, sliderCount, null, null);
		}

		public Tween SlideTo(float toValue, Action<float, Tween> callback = null)
		{
			return this.uiBlood.SlideTo(toValue, callback);
		}


		protected override void _Reset()
		{
			base._Reset();
			Client.instance.uiManager.RemoveUIBlood(this.uiBlood);
			this.uiBlood = null;
		}
	}
}