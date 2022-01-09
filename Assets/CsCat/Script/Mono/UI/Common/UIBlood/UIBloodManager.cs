using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class UIBloodManager : UIObject
	{
		List<GameObject> uiBloodGameObjectPool = new List<GameObject>();

		public override void Init()
		{
			base.Init();
			var gameObject = GameObject.Find(UIConst.UICanvas_Path + "/UIBloodManager");
			graphicComponent.SetGameObject(gameObject, true);
		}

		public UIBlood AddUIBlood(Transform parentTransform, float maxValue, int? sliderCount, float? toValue,
		  List<Color> sliderColorList = null)
		{
			var uiBlood =
			  this.AddChild<UIBlood>(null, parentTransform, maxValue, sliderCount, toValue, sliderColorList);
			return uiBlood;
		}

		public GameObject SpawnUIBloodGameObject()
		{
			if (uiBloodGameObjectPool.Count > 0)
				return uiBloodGameObjectPool.RemoveLast();
			return null;
		}

		public void DespawnUIBloodGameObject(GameObject uiBloodGameObject)
		{
			if (uiBloodGameObject == null)
				return;
			uiBloodGameObjectPool.Add(uiBloodGameObject);
			uiBloodGameObject.transform.SetParent(graphicComponent.transform);
		}

		protected override void _Reset()
		{
			base._Reset();
			graphicComponent.SetIsShow(false);
		}
	}
}