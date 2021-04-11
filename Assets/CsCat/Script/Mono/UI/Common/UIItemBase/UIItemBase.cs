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
    private ItemDefinition itemDefinition;
    private QualityDefinition qualityDefinition;

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

      this.itemDefinition = DefinitionManager.instance.itemDefinition.GetData(item_id);
//    LogCat.logError(Client.instance.definitionManager.Quality.GetData(item_data.quality_id));
      this.qualityDefinition = itemDefinition.quality_id == null
        ? null
        : DefinitionManager.instance.qualityDefinition.GetData(itemDefinition.quality_id);

      if (!itemDefinition.bg_path.IsNullOrWhiteSpace())
        this.SetImageAsync(this.content_bg_image, itemDefinition.bg_path, null, false);
      if (this.qualityDefinition != null && !this.qualityDefinition.icon_path.IsNullOrWhiteSpace())
        this.SetImageAsync(this.content_quality_image, qualityDefinition.icon_path, null, false);
      else
        this.content_bg_image.gameObject.SetActive(false);
//    LogCat.LogWarning(this.content_icon_image);
//    LogCat.LogWarning(itemData.icon_path);
      this.SetImageAsync(this.content_icon_image, itemDefinition.icon_path, null, false);
      this.content_count_text.text = (item_count == 0 || item_count == 1) ? "" : string.Format("x{0}", item_count);
      this.name_text.text = itemDefinition.name;

    }
  }
}