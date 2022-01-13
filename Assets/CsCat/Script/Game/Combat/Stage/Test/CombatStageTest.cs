using System.Collections;

namespace CsCat
{
	public class CombatStageTest : CombatStageBase
	{
		public override bool isShowFade
		{
			get { return true; }
		}

		public override string stageName
		{
			get { return "CombatStageTest"; }
		}

		public override void LoadPanels()
		{
			base.LoadPanels();
			panelList.Add(Client.instance.uiManager.CreateChildPanel("UICombatTestPanel", default(UICombatTestPanel)));
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

