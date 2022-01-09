using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UILoadingPanel : UIPanel
	{
		public override bool isResident => true;

		public override EUILayerName layerName => EUILayerName.LoadingUILayer;

		private float pct;
		private Slider silder;
		private Text descText;

		public void Init(GameObject gameObject)
		{
			base.Init();
			graphicComponent.SetGameObject(gameObject, true);
			graphicComponent.SetIsShow(false);
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			silder = frameTransform.FindComponentInChildren<Slider>("Slider");
			descText = graphicComponent.transform.FindComponentInChildren<Text>("desc");
		}

		public void SetPct(float pct)
		{
			graphicComponent.SetIsShow(true);
			silder.value = pct;
		}

		public void SetDesc(string desc)
		{
			graphicComponent.SetIsShow(true);
			this.descText.text = desc;
		}

		protected override void _Reset()
		{
			base._Reset();
			this.descText.text = "";
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