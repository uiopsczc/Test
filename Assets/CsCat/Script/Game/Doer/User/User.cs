using System.Collections;

namespace CsCat
{
	public partial class User : Thing
	{
		private ItemBag o_item_bag;
		private Roles o_roles;
		private Missions o_missions;
		public Role mainRole;

		public override void Init()
		{
			base.Init();
			this.o_roles = new Roles(this, "o_roles");
			this.o_item_bag = new ItemBag(this, "o_item_bag");
			this.o_missions = new Missions(this, "o_missions");
		}

		//////////////////////DoXXX/////////////////////////////////////
		//卸载
		public override void DoRelease()
		{
			this.o_roles.DoRelease();
			this.o_item_bag.DoRelease();
			this.o_missions.DoRelease();
			base.DoRelease();
		}

		// 保存
		public override void DoSave(Hashtable dict, Hashtable dictTmp)
		{
			base.DoSave(dict, dictTmp);
			//存储角色
			this.o_roles.DoSave(dict, dictTmp);
			//存储背包
			this.o_item_bag.DoSave(dict, dictTmp);
			//存储任务
			this.o_missions.DoSave(dict, dictTmp);
			if (this.mainRole != null)
				dict["main_role_rid"] = this.mainRole.GetRid();
		}

		//还原
		public override void DoRestore(Hashtable dict, Hashtable dictTmp)
		{
			//还原角色
			this.o_roles.DoRestore(dict, dictTmp);
			//还原背包
			this.o_item_bag.DoRestore(dict, dictTmp);
			//还原任务
			this.o_missions.DoRestore(dict, dictTmp);

			string main_role_rid = dict.Remove3<string>("main_role_rid");
			this.mainRole = this.GetRole(main_role_rid);

			base.DoRestore(dict, dictTmp);
		}

		//////////////////////OnXXX/////////////////////////////////////



		//////////////////////////////////////////Util///////////////////////////////////









	}
}