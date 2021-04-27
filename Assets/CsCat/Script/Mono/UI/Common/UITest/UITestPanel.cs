using UnityEngine.UI;

namespace CsCat
{
  public class UITestPanel : UIBackgroundPanel
  {
    public override void Init()
    {
      base.Init();
      this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UITestPanel.prefab");
    }

    public override void InitGameObjectChildren()
    {
      base.InitGameObjectChildren();
      Button gm_btn = this.frame_transform.FindComponentInChildren<Button>("gm_btn");
      Button test_btn = this.frame_transform.FindComponentInChildren<Button>("test_btn");
      Button combat_test_btn = this.frame_transform.FindComponentInChildren<Button>("combat_test_btn");
      this.RegisterOnClick(gm_btn,
        () => { Client.instance.uiManager.CreateChildPanel("UIGMPanel", default(UIGMTestPanel)); });
      this.RegisterOnClick(combat_test_btn, () =>
      {
        Client.instance.Goto<CombatStageTest>(0.5f,
          () => { Client.instance.uiManager.uiLoadingPanel.Reset(); });
      });
      this.RegisterOnClick(test_btn, Test);

      UIItemBaseTest.Test(this);
      UIGuidePanelTest.Test();

    }

    void Test()
    {
    }
  }
}