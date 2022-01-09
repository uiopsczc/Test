using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIGuidePanelBase
	{
		public class DescItem : UIObject
		{
			private Text descText;
			private Image descImage;

			public void Init(GameObject gameObject)
			{
				base.Init();
				graphicComponent.SetGameObject(gameObject, true);
			}

			public override void InitGameObjectChildren()
			{
				base.InitGameObjectChildren();
				descText = graphicComponent.transform.FindComponentInChildren<Text>("desc");
				descImage = graphicComponent.gameObject.GetComponent<Image>();
			}

			public void Show(string desc, bool isBgVisible = true)
			{
				descText.text = desc;
				if (!isBgVisible)
					descImage.SetAlpha(0);
			}
		}
	}
}