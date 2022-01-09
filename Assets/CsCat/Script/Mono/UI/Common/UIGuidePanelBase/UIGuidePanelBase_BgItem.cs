using System;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIGuidePanelBase
	{
		public class BgItem : UIObject
		{
			public Image image;
			public Button button;

			public void Init(GameObject gameObject)
			{
				base.Init();
				graphicComponent.SetGameObject(gameObject, true);
			}

			public override void InitGameObjectChildren()
			{
				base.InitGameObjectChildren();
				image = graphicComponent.gameObject.GetComponent<Image>();
				button = graphicComponent.gameObject.GetComponent<Button>();
			}

			public void Show(bool isClickable = true, Action<UIPanel> clickCallback = null, bool isVisible = true)
			{
				if (!isVisible)
					image.SetAlpha(0.007f);
				if (isClickable)
				{
					if (clickCallback == null)
						this.parentUIPanel.RegisterOnClick(button, () => { parentUIPanel.Close(); });
					else
						this.parentUIPanel.RegisterOnClick(button, () => { clickCallback(parentUIPanel); });
				}

			}
		}
	}
}