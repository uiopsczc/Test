using System.Collections;

namespace CsCat
{
	public partial class User : Thing
	{
		private ItemBag oItemBag;
		private Roles oRoles;
		private Missions oMissions;
		public Role mainRole;

		public override void Init()
		{
			base.Init();
			this.oRoles = new Roles(this, "o_roles");
			this.oItemBag = new ItemBag(this, "o_item_bag");
			this.oMissions = new Missions(this, "o_missions");
		}

		//////////////////////DoXXX/////////////////////////////////////
		//卸载
		public override void DoRelease()
		{
			this.oRoles.DoRelease();
			this.oItemBag.DoRelease();
			this.oMissions.DoRelease();
			base.DoRelease();
		}

		// 保存
		public override void DoSave(Hashtable dict, Hashtable dictTmp)
		{
			base.DoSave(dict, dictTmp);
			//存储角色
			this.oRoles.DoSave(dict, dictTmp);
			//存储背包
			this.oItemBag.DoSave(dict, dictTmp);
			//存储任务
			this.oMissions.DoSave(dict, dictTmp);
			if (this.mainRole != null)
				dict["main_role_rid"] = this.mainRole.GetRid();
		}

		//还原
		public override void DoRestore(Hashtable dict, Hashtable dictTmp)
		{
			//还原角色
			this.oRoles.DoRestore(dict, dictTmp);
			//还原背包
			this.oItemBag.DoRestore(dict, dictTmp);
			//还原任务
			this.oMissions.DoRestore(dict, dictTmp);

			string mainRoleRid = dict.Remove3<string>("main_role_rid");
			this.mainRole = this.GetRole(mainRoleRid);

			base.DoRestore(dict, dictTmp);
		}

		//////////////////////OnXXX/////////////////////////////////////



		//////////////////////////////////////////Util///////////////////////////////////









	}
}