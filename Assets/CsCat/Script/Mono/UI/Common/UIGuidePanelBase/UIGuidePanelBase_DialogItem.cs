using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  public partial class UIGuidePanelBase
  {
    public class DialogItem : UIObject
    {
      private Image tou_xiang_img;
      private Text desc_text;

      public void Init(GameObject gameObject)
      {
        base.Init();
        graphicComponent.SetGameObject(gameObject, true);
      }

      public override void InitGameObjectChildren()
      {
        base.InitGameObjectChildren();
        tou_xiang_img = graphicComponent.transform.FindComponentInChildren<Image>("tou_xiang");
        desc_text = graphicComponent.transform.FindComponentInChildren<Text>("content/desc");
      }

      public void Show(string desc, string image_path = null)
      {
        desc_text.text = desc;
        if (image_path != null)
          SetImageAsync(tou_xiang_img, image_path);
      }
    }
  }
}