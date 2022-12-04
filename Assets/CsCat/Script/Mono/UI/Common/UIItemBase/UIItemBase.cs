using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UIItemBase : UIObject
	{

		private Image _ImgC_Bg;
		private Image _ImgC_Quality;
		private Image _ImgC_Icon;
		private Text _TxtC_Count;
		private Text _TxtC_Name;

		private string itemId;
		private int itemCount;
		private CfgItemData cfgItemData;
		private CfgQualityData cfgQualityData;

		protected void _Init(Transform parentTransform)
		{
			base._Init();
			this.SetParentTransform(parentTransform);
			this.SetPrefabPath("Assets/PatchResources/UI/UIItemBase/Prefab/UIItemBase.prefab");
		}

		protected override void _InitGameObjectChildren()
		{
			base._InitGameObjectChildren();
			this._ImgC_Bg = _contentTransform.Find("ImgC_Bg").GetComponent<Image>();
			this._ImgC_Quality = _contentTransform.Find("ImgC_Quality").GetComponent<Image>();
			this._ImgC_Icon = _contentTransform.Find("ImgC_Icon").GetComponent<Image>();
			this._TxtC_Count = _contentTransform.Find("TxtC_Count").GetComponent<Text>();
			this._TxtC_Name = _contentTransform.Find("TxtC_Name").GetComponent<Text>();
		}

		public void Show(string itemId, int itemCount, bool isShowName = true)
		{
			this.itemId = itemId;
			this.itemCount = itemCount;

			this.cfgItemData = CfgItem.Instance.GetById(itemId);
			this.cfgQualityData = cfgItemData.qualityId == null
			  ? null
			  : CfgQuality.Instance.GetById(cfgItemData.qualityId);

			if (!cfgItemData.bgPath.IsNullOrWhiteSpace())
				this._SetImageAsync(this._ImgC_Bg, cfgItemData.bgPath, null, false);
			if (this.cfgQualityData != null && !this.cfgQualityData.iconPath.IsNullOrWhiteSpace())
				this._SetImageAsync(this._ImgC_Quality, cfgQualityData.iconPath, null, false);
			else
				this._ImgC_Bg.gameObject.SetActive(false);
			//    LogCat.LogWarning(this.content_icon_image);
			//    LogCat.LogWarning(itemData.icon_path);
			this._SetImageAsync(this._ImgC_Icon, cfgItemData.iconPath, null, false);
			this._TxtC_Count.text = (itemCount == 0 || itemCount == 1) ? "" : string.Format("x{0}", itemCount);
			this._TxtC_Name.text = cfgItemData.name;

		}
	}
}