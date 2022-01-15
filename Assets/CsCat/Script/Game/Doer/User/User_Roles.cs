namespace CsCat
{
	public partial class User
	{
		////////////////////////////角色////////////////////////
		//获得指定的角色
		public Role[] GetRoles(string id = null)
		{
			return this.oRoles.GetRoles(id);
		}

		public int GetRolesCount()
		{
			return this.oRoles.GetRolesCount();
		}

		//获得指定的角色
		public Role GetRole(string idOrRid)
		{
			return this.oRoles.GetRole(idOrRid);
		}

		//清除所有角色
		public void ClearRoles()
		{
			this.oRoles.ClearRoles();
		}

		public bool CheckAddRole(Role role)
		{
			return this.OnCheckAddRole(role) && role.OnCheckAddRole(this);
		}

		public bool AddRole(Role role)
		{
			var env = role.GetEnv();
			if (env != null)
			{
				LogCat.error(string.Format("{0} still in {1}", role, env));
				return false;
			}

			var list = this.oRoles.GetRoles_ToEdit();
			if (list.Contains(role))
			{
				LogCat.error(string.Format("{0} already has role:{1}", this, role));
				return false;
			}

			if (!(this.OnAddRole(role) && role.OnAddRole(this)))
				return false;

			role.SetEnv(this);
			list.Add(role);
			return true;
		}

		public bool CheckRemoveRole(Role role)
		{
			return this.OnCheckRemoveRole(role) && role.OnCheckRemoveRole(this);
		}

		public bool RemoveRole(Role role)
		{
			var list = this.oRoles.GetRoles_ToEdit();
			if (!list.Contains(role))
			{
				LogCat.error(string.Format("{0} not contains role:{1}", this, role));
				return false;
			}

			if (!(this.OnRemoveRole(role) && role.OnRemoveRole(this)))
				return false;

			list.Remove(role);
			role.SetEnv(null);
			return true;
		}

		//////////////////////OnXXX////////////////////////////////////
		public virtual bool OnCheckAddRole(Role role)
		{
			return true;
		}

		public virtual bool OnAddRole(Role role)
		{
			return true;
		}

		public virtual bool OnCheckRemoveRole(Role role)
		{
			return true;
		}

		public virtual bool OnRemoveRole(Role role)
		{
			return true;
		}

		////////////////////////////////Util////////////////////////////
		public Role AddRole(string idOrRid)
		{
			Role role = Client.instance.roleFactory.NewDoer(idOrRid) as Role;
			if (!this.CheckAddRole(role))
				return null;
			if (!this.AddRole(role))
			{
				role.Destruct();
				return null;
			}

			return role;
		}

		public Role RemoveRole(string idOrRid, bool isNotNeedDestruct = false)
		{
			Role role = GetRole(idOrRid);
			if (role == null)
			{
				LogCat.error(string.Format("{0} do not contain role:{1}", this, role));
				return null;
			}

			if (!this.CheckRemoveRole(role))
				return null;
			if (!this.RemoveRole(role))
				return null;
			if (!isNotNeedDestruct)
				role.Destruct();
			return role;
		}
	}
}