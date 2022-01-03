using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UILoadingPanel : UIPanel
	{
		public override bool is_resident
		{
			get { return true; }
		}

		public override EUILayerName layerName
		{
			get { return EUILayerName.LoadingUILayer; }
		}

		private float pct;
		private Slider silder;
		private Text desc_text;

		public void Init(GameObject gameObject)
		{
			base.Init();
			graphicComponent.SetGameObject(gameObject, true);
			graphicComponent.SetIsShow(false);
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			silder = frame_transform.FindComponentInChildren<Slider>("Slider");
			desc_text = graphicComponent.transform.FindComponentInChildren<Text>("desc");
		}

		public void SetPct(float pct)
		{
			graphicComponent.SetIsShow(true);
			silder.value = pct;
		}

		public void SetDesc(string desc)
		{
			graphicComponent.SetIsShow(true);
			this.desc_text.text = desc;
		}

		protected override void _Reset()
		{
			base._Reset();
			this.desc_text.text = "";
			silder.value = 0;
			graphicComponent.SetIsShow(false);
		}

		public void HideLoading()
		{
			Reset();
			graphicComponent.SetIsShow(false);
		}


	}
}