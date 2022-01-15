using System.Collections;

namespace CsCat
{
	public class CombatStageTest : CombatStageBase
	{
		public override bool isShowFade => true;

		public override string stageName => "CombatStageTest";

		public override void LoadPanels()
		{
			base.LoadPanels();
			panelList.Add(Client.instance.uiManager.CreateChildPanel("UICombatTestPanel", default(UICombatTestPanel)));
		}

		public override void Show()
		{
			base.Show();
			Hashtable argDict = new Hashtable();
			argDict["combat_class_path"] = typeof(CombatTest).ToString();
			argDict["gameLevel_class_path"] = typeof(GameLevelTest).ToString();
			StartCombat(argDict);
			this.HideFade();
		}
	}

}

