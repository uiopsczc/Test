using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIGuidePanelBase
	{
		public class DescItem : UIObject
		{
			private Text _TxtC_Desc;
			private Image _ImgC_Bg;

			protected void _Init(GameObject gameObject)
			{
				base._Init();
				_SetGameObject(gameObject, true);
			}

			protected override void _InitGameObjectChildren()
			{
				base._InitGameObjectChildren();
				_TxtC_Desc = this.GetTransform().Find("TxtC_Desc").GetComponent<Text>();
				_ImgC_Bg = this.GetTransform().Find("ImgC_Bg").GetComponent<Image>();
			}

			public void Show(string desc, bool isBgVisible = true)
			{
				_TxtC_Desc.text = desc;
				if (!isBgVisible)
					_ImgC_Bg.SetAlpha(0);
			}
		}
	}
}