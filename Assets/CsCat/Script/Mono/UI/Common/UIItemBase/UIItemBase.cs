using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UIItemBase : UIObject
	{

		private Transform content_transform;
		private Image content_bg_image;
		private Image content_quality_image;
		private Image content_icon_image;
		private Text content_count_text;
		private Transform name_transform;
		private Image name_bg_image;
		private Text name_text;

		private string item_id;
		private int item_count;
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
			this.content_transform = graphicComponent.transform.Find("content");
			this.content_bg_image = content_transform.FindComponentInChildren<Image>("bg");
			this.content_quality_image = content_transform.FindComponentInChildren<Image>("quality");
			this.content_icon_image = content_transform.FindComponentInChildren<Image>("icon");
			this.content_count_text = content_transform.FindComponentInChildren<Text>("count");
			this.name_transform = graphicComponent.transform.Find("name");
			this.name_bg_image = this.name_transform.FindComponentInChildren<Image>("bg");
			this.name_text = this.name_transform.FindComponentInChildren<Text>("text");
		}

		public void Show(string item_id, int item_count, bool is_show_name = true)
		{
			this.item_id = item_id;
			this.item_count = item_count;

			this.cfgItemData = CfgItem.Instance.get_by_id(item_id);
			this.cfgQualityData = cfgItemData.quality_id == null
			  ? null
			  : CfgQuality.Instance.get_by_id(cfgItemData.quality_id);

			if (!cfgItemData.bg_path.IsNullOrWhiteSpace())
				this.SetImageAsync(this.content_bg_image, cfgItemData.bg_path, null, false);
			if (this.cfgQualityData != null && !this.cfgQualityData.icon_path.IsNullOrWhiteSpace())
				this.SetImageAsync(this.content_quality_image, cfgQualityData.icon_path, null, false);
			else
				this.content_bg_image.gameObject.SetActive(false);
			//    LogCat.LogWarning(this.content_icon_image);
			//    LogCat.LogWarning(itemData.icon_path);
			this.SetImageAsync(this.content_icon_image, cfgItemData.icon_path, null, false);
			this.content_count_text.text = (item_count == 0 || item_count == 1) ? "" : string.Format("x{0}", item_count);
			this.name_text.text = cfgItemData.name;

		}
	}
}