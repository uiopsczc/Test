using UnityEngine.UI;

namespace CsCat
{
  public class UICombatTestPanel : UIPanel
  {
    public override EUILayerName layerName
    {
      get { return EUILayerName.PopUpUILayer; }
    }

    public override void Init()
    {
      base.Init();
      this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UICombatTestPanel.prefab");
    }

    public override void InitGameObjectChildren()
    {
      base.InitGameObjectChildren();
      Button gm_btn = this.frame_transform.FindComponentInChildren<Button>("gm_btn");
      Button test_btn = this.frame_transform.FindComponentInChildren<Button>("test_btn");
      this.RegisterOnClick(test_btn, Test);
    }

    void Test()
    {
      EffectTest.Test();
    }
  }
}