using System.Collections;

namespace CsCat
{
	public class Roles
	{
		private Doer parentDoer;
		private string subDoerKey;

		public Roles(Doer parentDoer, string subDoerKey)
		{
			this.parentDoer = parentDoer;
			this.subDoerKey = subDoerKey;
		}

		////////////////////DoXXX/////////////////////////////////
		//卸载
		public void DoRelease()
		{
			SubDoerUtil1.DoReleaseSubDoer<Role>(this.parentDoer, this.subDoerKey);
		}

		//保存
		public void DoSave(Hashtable dict, Hashtable dictTmp, string saveKey = null)
		{
			saveKey = saveKey ?? "roles";
			var roles = this.GetRoles();
			var dictRoles = new ArrayList();
			var dictRolesTmp = new Hashtable();
			for (var i = 0; i < roles.Length; i++)
			{
				var role = roles[i];
				var dictRole = new Hashtable();
				var dictRoleTmp = new Hashtable();
				var rid = role.GetRid();
				role.PrepareSave(dictRole, dictRoleTmp);
				dictRole["rid"] = rid;
				dictRoles.Add(dictRole);
				if (!dictRoleTmp.IsNullOrEmpty())
					dictRolesTmp[rid] = dictRoleTmp;
			}

			if (!dictRoles.IsNullOrEmpty())
				dict[saveKey] = dictRoles;
			if (!dictRolesTmp.IsNullOrEmpty())
				dictTmp[saveKey] = dictRoles;
		}

		//还原
		public void DoRestore(Hashtable dict, Hashtable dictTmp, string restoreKey = null)
		{
			restoreKey = restoreKey ?? "roles";
			this.ClearRoles();
			var dictRoles = dict.Remove3<ArrayList>(restoreKey);
			var dictRolesTmp = dictTmp?.Remove3<Hashtable>(restoreKey);
			if (!dictRoles.IsNullOrEmpty())
			{
				var roles = this.GetRoles_ToEdit();
				for (var i = 0; i < dictRoles.Count; i++)
				{
					var curDictRole = dictRoles[i];
					var dictRole = curDictRole as Hashtable;
					var rid = dictRole.Remove3<string>("rid");
					Role role = Client.instance.roleFactory.NewDoer(rid) as Role;
					role.SetEnv(this.parentDoer);
					Hashtable dictRoleTmp = null;
					if (dictRolesTmp != null && dictRolesTmp.ContainsKey(rid))
						dictRoleTmp = dictRolesTmp[rid] as Hashtable;
					role.FinishRestore(dictRole, dictRoleTmp);
					roles.Add(role);
				}
			}
		}
		//////////////////////////OnXXX//////////////////////////////////////////////////


		////////////////////////////////////////////////////////////////////////////
		//获得指定的角色
		public Role[] GetRoles(string id = null)
		{
			return SubDoerUtil1.GetSubDoers<Role>(this.parentDoer, this.subDoerKey, id, null);
		}

		public ArrayList GetRoles_ToEdit() //可以直接插入删除
		{
			return SubDoerUtil1.GetSubDoers_ToEdit(this.parentDoer, this.subDoerKey);
		}

		public int GetRolesCount()
		{
			return SubDoerUtil1.GetSubDoersCount<Role>(this.parentDoer, this.subDoerKey);
		}

		//获得指定的角色
		public Role GetRole(string idOrRid)
		{
			return SubDoerUtil1.GetSubDoer<Role>(this.parentDoer, this.subDoerKey, idOrRid);
		}

		//清除所有角色
		public void ClearRoles()
		{
			SubDoerUtil1.ClearSubDoers<Role>(this.parentDoer, this.subDoerKey,
			  (role) => { ((User)this.parentDoer).RemoveRole(role); });
		}
	}
}