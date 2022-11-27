using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UILoadingPanel : UIPanel
	{
		public override bool isResident => true;

		public override EUILayerName layerName => EUILayerName.LoadingUILayer;

		private float _pct;
		private Slider _Slider_Progress;
		private Text _TxtC_Desc;

		protected void _Init(GameObject gameObject)
		{
			base._Init();
			SetGameObject(gameObject, true);
		}

		protected override void _PostInit()
		{
			base._PostInit();
			this.SetIsShow(false);
		}

		protected override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			_Slider_Progress = _frameTransform.Find("Slider_Progress").GetComponent<Slider>();
			_TxtC_Desc = _frameTransform.Find("TxtC_Desc").GetComponent<Text>();
		}

		public void SetPct(float pct)
		{
			SetIsShow(true);
			_Slider_Progress.value = pct;
		}

		public void SetDesc(string desc)
		{
			SetIsShow(true);
			_TxtC_Desc.text = desc;
		}

		protected override void _Reset()
		{
			base._Reset();
			_TxtC_Desc.text = "";
			_Slider_Progress.value = 0;
			SetIsShow(false);
		}

		public void HideLoading()
		{
			Reset();
			SetIsShow(false);
		}
	}
}