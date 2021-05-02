using UnityEngine.UI;

namespace CsCat
{
  public class UICombatTestPanel : UIPanel
  {
    private Button gm_btn;
    private Button test_btn;
    public override EUILayerName layerName => EUILayerName.BackgroundUILayer;

    public override void Init()
    {
      base.Init();
      this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UICombatTestPanel.prefab");
    }

    public override void InitGameObjectChildren()
    {
      base.InitGameObjectChildren();
      gm_btn = this.frame_transform.FindComponentInChildren<Button>("gm_btn");
      test_btn = this.frame_transform.FindComponentInChildren<Button>("test_btn");
      
    }

    protected override void AddUntiyEvnts()
    {
      base.AddUntiyEvnts();
      this.RegisterOnClick(test_btn, Test);
    }

    void Test()
    {
      EffectTest.Test();
    }
  }
}