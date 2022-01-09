using System.Collections;

namespace CsCat
{
	public partial class Critter : Thing
	{
		private Equips o_equips;

		public override void Init()
		{
			base.Init();
			this.o_equips = new Equips(this, "o_equips");
		}

		//////////////////////DoXXX/////////////////////////////////////
		//卸载
		public override void DoRelease()
		{
			this.o_equips.DoRelease();
			base.DoRelease();
		}

		// 保存
		public override void DoSave(Hashtable dict, Hashtable dictTmp)
		{
			base.DoSave(dict, dictTmp);
			//存储装备
			this.o_equips.DoSave(dict, dictTmp);
		}

		//还原
		public override void DoRestore(Hashtable dict, Hashtable dictTmp)
		{
			//还原装备
			this.o_equips.DoRestore(dict, dictTmp);
			base.DoRestore(dict, dictTmp);
		}

		//////////////////////OnXXX/////////////////////////////////////




	}
}