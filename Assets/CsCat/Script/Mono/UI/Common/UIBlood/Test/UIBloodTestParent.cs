using System;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
	public partial class UIBloodTestParent : UIObject
	{
		public UIBlood uiBlood;

		protected void _Init(string name, int sliderCount)
		{
			base._Init();
			var rectTransform = GameObject.Find("UITestPanel").NewChildWithRectTransform(name);
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.sizeDelta = Vector2.zero;
			_SetGameObject(rectTransform.gameObject, false);
			this.uiBlood = Client.instance.uiManager.AddUIBlood(this.GetTransform(), 150, sliderCount, null);
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