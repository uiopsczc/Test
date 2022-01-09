using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UIItemBase : UIObject
	{

		private Transform contentTransform;
		private Image contentBgImage;
		private Image contentQualityImage;
		private Image contentIconImage;
		private Text contentCountText;
		private Transform nameTransform;
		private Image nameBgImage;
		private Text nameText;

		private string itemId;
		private int itemCount;
		private CfgItemData cfgItemData;
		private CfgQualityData cfgQualityData;

		public void Init(Transform parent_transform)
		{
			base.Init();
			this.graphicComponent.SetParentTransform(parent_transform);
			this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UIItemBase.prefab");
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			this.contentTransform = graphicComponent.transform.Find("content");
			this.contentBgImage = contentTransform.FindComponentInChildren<Image>("bg");
			this.contentQualityImage = contentTransform.FindComponentInChildren<Image>("quality");
			this.contentIconImage = contentTransform.FindComponentInChildren<Image>("icon");
			this.contentCountText = contentTransform.FindComponentInChildren<Text>("count");
			this.nameTransform = graphicComponent.transform.Find("name");
			this.nameBgImage = this.nameTransform.FindComponentInChildren<Image>("bg");
			this.nameText = this.nameTransform.FindComponentInChildren<Text>("text");
		}

		public void Show(string itemId, int itemCount, bool isShowName = true)
		{
			this.itemId = itemId;
			this.itemCount = itemCount;

			this.cfgItemData = CfgItem.Instance.get_by_id(itemId);
			this.cfgQualityData = cfgItemData.quality_id == null
			  ? null
			  : CfgQuality.Instance.get_by_id(cfgItemData.quality_id);

			if (!cfgItemData.bg_path.IsNullOrWhiteSpace())
				this.SetImageAsync(this.contentBgImage, cfgItemData.bg_path, null, false);
			if (this.cfgQualityData != null && !this.cfgQualityData.icon_path.IsNullOrWhiteSpace())
				this.SetImageAsync(this.contentQualityImage, cfgQualityData.icon_path, null, false);
			else
				this.contentBgImage.gameObject.SetActive(false);
			//    LogCat.LogWarning(this.content_icon_image);
			//    LogCat.LogWarning(itemData.icon_path);
			this.SetImageAsync(this.contentIconImage, cfgItemData.icon_path, null, false);
			this.contentCountText.text = (itemCount == 0 || itemCount == 1) ? "" : string.Format("x{0}", itemCount);
			this.nameText.text = cfgItemData.name;

		}
	}
}