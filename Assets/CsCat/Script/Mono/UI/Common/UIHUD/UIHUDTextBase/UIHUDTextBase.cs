using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UIHUDTextBase : UIObject
	{

		float textAlpha;
		public Text textComp;
		public Animation textAnimation;

		public void Init(Transform parent_transform)
		{
			base.Init();
			this.graphicComponent.SetParentTransform(parent_transform);
			this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UIHUDText.prefab");
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			this.textComp = graphicComponent.gameObject.GetComponent<Text>();
			this.textAlpha = textComp.color.a;
			this.textAnimation = graphicComponent.gameObject.GetComponent<Animation>();
		}

		protected override void _Reset()
		{
			base._Reset();
			this.textComp.SetColorA(this.textAlpha);
		}
	}
}