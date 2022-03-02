using System.Collections;

namespace CsCat
{
	public class CombatStageBase : StageBase
	{
		public override bool isShowFade => true;

		public override bool isShowLoading => true;

		public override string stageName => "CombatStageBase";

		public CombatBase combat;

		public void StartCombat(Hashtable argDict = null)
		{
			if (this.combat != null)
				this.RemoveChild(this.combat.key);
			var combatClass =
			  TypeUtil.GetType(argDict.GetOrGetDefault2("combat_class_path", () => typeof(CombatBase).ToString()));
			this.combat = this.AddChild(null, combatClass, argDict) as CombatBase;
			Client.instance.combat = this.combat;
			this.combat.Start();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			Client.instance.combat = null;
		}
	}
}


