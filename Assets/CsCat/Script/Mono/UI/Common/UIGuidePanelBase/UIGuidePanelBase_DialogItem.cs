using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIGuidePanelBase
	{
		public class DialogItem : UIObject
		{
			private Image _ImgC_Head;
			private Text _TxtC_Desc;

			protected void _Init(GameObject gameObject)
			{
				base._Init();
				DoSetGameObject(gameObject);
			}

			protected override void _InitGameObjectChildren()
			{
				base._InitGameObjectChildren();
				_ImgC_Head = this.GetTransform().Find("ImgC_Head").GetComponent<Image>();
				_TxtC_Desc = this.GetTransform().Find("Nego_Content/TxtC_Desc").GetComponent<Text>();
			}

			public void Show(string desc, string imagePath = null)
			{
				_TxtC_Desc.text = desc;
				if (imagePath != null)
					_SetImageAsync(_ImgC_Head, imagePath);
			}

			protected override void _DestroyGameObject()
			{
			}
		}
	}
}