using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class UIBloodManager : UIObject
  {
    List<GameObject> uiBlood_gameObject_pool = new List<GameObject>();

    public override void Init()
    {
      base.Init();
      var gameObject = GameObject.Find(UIConst.UICanvas_Path + "/UIBloodManager");
      graphicComponent.SetGameObject(gameObject, true);
    }

    public UIBlood AddUIBlood(Transform parent_transform, float max_value, int? slider_count, float? to_value,
      List<Color> slider_color_list = null)
    {
      var uiBlood =
        this.AddChild<UIBlood>(null, parent_transform, max_value, slider_count, to_value, slider_color_list);
      return uiBlood;
    }

    public GameObject SpawnUIBloodGameObject()
    {
      if (uiBlood_gameObject_pool.Count > 0)
        return uiBlood_gameObject_pool.RemoveLast();
      return null;
    }

    public void DespawnUIBloodGameObject(GameObject uiBlood_gameObject)
    {
      if (uiBlood_gameObject == null)
        return;
      uiBlood_gameObject_pool.Add(uiBlood_gameObject);
      uiBlood_gameObject.transform.SetParent(graphicComponent.transform);
    }

    protected override void _Reset()
    {
      base._Reset();
      graphicComponent.SetIsShow(false);
    }
  }
}