using UnityEngine;

namespace CsCat
{
  public class UIShowLogoPanel : UIBackgroundPanel
  {
    public override bool is_resident
    {
      get { return true; }
    }
    

    public void Init(GameObject gameObject)
    {
      base.Init();
      graphicComponent.SetGameObject(gameObject, true);
    }

    protected override void __Reset()
    {
      base.__Reset();
      graphicComponent.SetIsShow(false);
    }
  }
}