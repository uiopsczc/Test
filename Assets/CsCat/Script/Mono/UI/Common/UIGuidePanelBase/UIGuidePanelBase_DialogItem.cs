using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIGuidePanelBase
	{
		public class DialogItem : UIObject
		{
			private Image touXiangImg;
			private Text descText;

			public void Init(GameObject gameObject)
			{
				base.Init();
				graphicComponent.SetGameObject(gameObject, true);
			}

			public override void InitGameObjectChildren()
			{
				base.InitGameObjectChildren();
				touXiangImg = graphicComponent.transform.FindComponentInChildren<Image>("tou_xiang");
				descText = graphicComponent.transform.FindComponentInChildren<Text>("content/desc");
			}

			public void Show(string desc, string imagePath = null)
			{
				descText.text = desc;
				if (imagePath != null)
					SetImageAsync(touXiangImg, imagePath);
			}
		}
	}
}