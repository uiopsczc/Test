using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIGuidePanelBase
	{
		public class DescItem : UIObject
		{
			private Text desc_text;
			private Image desc_image;

			public void Init(GameObject gameObject)
			{
				base.Init();
				graphicComponent.SetGameObject(gameObject, true);
			}

			public override void InitGameObjectChildren()
			{
				base.InitGameObjectChildren();
				desc_text = graphicComponent.transform.FindComponentInChildren<Text>("desc");
				desc_image = graphicComponent.gameObject.GetComponent<Image>();
			}

			public void Show(string desc, bool is_bg_visible = true)
			{
				desc_text.text = desc;
				if (!is_bg_visible)
					desc_image.SetAlpha(0);
			}
		}
	}
}