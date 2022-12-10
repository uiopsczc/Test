using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class UIBloodManager : UIObject
	{
		protected override void _Init()
		{
			base._Init();
			var gameObject = GameObject.Find(UIConst.UIBloodManager_Path);
			DoSetGameObject(gameObject);
		}

		public UIBlood AddUIBlood(Transform parentTransform, float maxValue, int? sliderCount, float? toValue,
		  List<Color> sliderColorList = null)
		{
			var uiBlood =
			  this.AddChild<UIBlood>(null, parentTransform, maxValue, sliderCount, toValue, sliderColorList);
			return uiBlood;
		}
		protected override void _DestroyGameObject()
		{
		}
	}
}