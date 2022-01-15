using System.Collections;

namespace CsCat
{
	public partial class Critter : Thing
	{
		private Equips oEquips;

		public override void Init()
		{
			base.Init();
			this.oEquips = new Equips(this, "o_equips");
		}

		//////////////////////DoXXX/////////////////////////////////////
		//卸载
		public override void DoRelease()
		{
			this.oEquips.DoRelease();
			base.DoRelease();
		}

		// 保存
		public override void DoSave(Hashtable dict, Hashtable dictTmp)
		{
			base.DoSave(dict, dictTmp);
			//存储装备
			this.oEquips.DoSave(dict, dictTmp);
		}

		//还原
		public override void DoRestore(Hashtable dict, Hashtable dictTmp)
		{
			//还原装备
			this.oEquips.DoRestore(dict, dictTmp);
			base.DoRestore(dict, dictTmp);
		}

		//////////////////////OnXXX/////////////////////////////////////




	}
}