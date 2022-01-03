using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UIHUDTextBase : UIObject
	{

		float text_alpha;
		public Text text_comp;
		public Animation text_animation;

		public void Init(Transform parent_transform)
		{
			base.Init();
			this.graphicComponent.SetParentTransform(parent_transform);
			this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UIHUDText.prefab");
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			this.text_comp = graphicComponent.gameObject.GetComponent<Text>();
			this.text_alpha = text_comp.color.a;
			this.text_animation = graphicComponent.gameObject.GetComponent<Animation>();
		}

		protected override void _Reset()
		{
			base._Reset();
			this.text_comp.SetColorA(this.text_alpha);
		}
	}
}