using System.Collections;

namespace CsCat
{
  public class CombatStageBase : StageBase
  {
    public override bool is_show_fade
    {
      get { return true; }
    }

    public override bool is_show_loading
    {
      get { return true; }
    }

    public override string stage_name
    {
      get { return "CombatStageBase"; }
    }

    public CombatBase combat;

    public void StartCombat(Hashtable arg_dict = null)
    {
      if (this.combat != null)
        this.RemoveChild(this.combat.key);
      var combat_class =
        TypeUtil.GetType(arg_dict.GetOrGetDefault2<string>("combat_class_path", () => typeof(CombatBase).ToString()));
      this.combat = this.AddChild(null, combat_class, arg_dict) as CombatBase;
      Client.instance.combat = this.combat;
      this.combat.Start();
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      Client.instance.combat = null;
    }
  }
}


