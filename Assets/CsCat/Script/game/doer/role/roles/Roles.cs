using System.Collections;

namespace CsCat
{
	public class Roles
	{
		private Doer parent_doer;
		private string sub_doer_key;

		public Roles(Doer parent_doer, string sub_doer_key)
		{
			this.parent_doer = parent_doer;
			this.sub_doer_key = sub_doer_key;
		}

		////////////////////DoXXX/////////////////////////////////
		//卸载
		public void DoRelease()
		{
			SubDoerUtil1.DoReleaseSubDoer<Role>(this.parent_doer, this.sub_doer_key);
		}

		//保存
		public void DoSave(Hashtable dict, Hashtable dict_tmp, string save_key = null)
		{
			save_key = save_key ?? "roles";
			var roles = this.GetRoles();
			var dict_roles = new ArrayList();
			var dict_roles_tmp = new Hashtable();
			foreach (var role in roles)
			{
				var dict_role = new Hashtable();
				var dict_role_tmp = new Hashtable();
				var rid = role.GetRid();
				role.PrepareSave(dict_role, dict_role_tmp);
				dict_role["rid"] = rid;
				dict_roles.Add(dict_role);
				if (!dict_role_tmp.IsNullOrEmpty())
					dict_roles_tmp[rid] = dict_role_tmp;
			}

			if (!dict_roles.IsNullOrEmpty())
				dict[save_key] = dict_roles;
			if (!dict_roles_tmp.IsNullOrEmpty())
				dict_tmp[save_key] = dict_roles;
		}

		//还原
		public void DoRestore(Hashtable dict, Hashtable dict_tmp, string restore_key = null)
		{
			restore_key = restore_key ?? "roles";
			this.ClearRoles();
			var dict_roles = dict.Remove3<ArrayList>(restore_key);
			var dict_roles_tmp = dict_tmp?.Remove3<Hashtable>(restore_key);
			if (!dict_roles.IsNullOrEmpty())
			{
				var roles = this.GetRoles_ToEdit();
				foreach (var _dict_role in dict_roles)
				{
					var dict_role = _dict_role as Hashtable;
					var rid = dict_role.Remove3<string>("rid");
					Role role = Client.instance.roleFactory.NewDoer(rid) as Role;
					role.SetEnv(this.parent_doer);
					Hashtable dict_role_tmp = null;
					if (dict_roles_tmp != null && dict_roles_tmp.ContainsKey(rid))
						dict_role_tmp = dict_roles_tmp[rid] as Hashtable;
					role.FinishRestore(dict_role, dict_role_tmp);
					roles.Add(role);
				}
			}
		}
		//////////////////////////OnXXX//////////////////////////////////////////////////


		////////////////////////////////////////////////////////////////////////////
		//获得指定的角色
		public Role[] GetRoles(string id = null)
		{
			return SubDoerUtil1.GetSubDoers<Role>(this.parent_doer, this.sub_doer_key, id, null);
		}

		public ArrayList GetRoles_ToEdit() //可以直接插入删除
		{
			return SubDoerUtil1.GetSubDoers_ToEdit(this.parent_doer, this.sub_doer_key);
		}

		public int GetRolesCount()
		{
			return SubDoerUtil1.GetSubDoersCount<Role>(this.parent_doer, this.sub_doer_key);
		}

		//获得指定的角色
		public Role GetRole(string id_or_rid)
		{
			return SubDoerUtil1.GetSubDoer<Role>(this.parent_doer, this.sub_doer_key, id_or_rid);
		}

		//清除所有角色
		public void ClearRoles()
		{
			SubDoerUtil1.ClearSubDoers<Role>(this.parent_doer, this.sub_doer_key,
			  (role) => { ((User)this.parent_doer).RemoveRole(role); });
		}
	}
}