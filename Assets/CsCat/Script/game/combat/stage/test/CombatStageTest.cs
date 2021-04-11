using System.Collections;

namespace CsCat
{
  public class CombatStageTest : CombatStageBase
  {
    public override bool is_show_fade
    {
      get { return true; }
    }

    public override string stage_name
    {
      get { return "CombatStageTest"; }
    }

    public override void LoadPanels()
    {
      base.LoadPanels();
      panel_list.Add(Client.instance.uiManager.CreateChildPanel("UICombatTestPanel", default(UICombatTestPanel)));
    }

    public override void Show()
    {
      base.Show();
      Hashtable arg_dict = new Hashtable();
      arg_dict["combat_class_path"] = typeof(CombatTest).ToString();
      arg_dict["gameLevel_class_path"] = typeof(GameLevelTest).ToString();
      StartCombat(arg_dict);
      this.HideFade();
    }
  }

}

