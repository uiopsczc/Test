namespace CsCat
{
  public class Role : Critter
  {

    public RoleFactory GetRoleFactory()
    {
      return this.factory as RoleFactory;
    }

    public CfgRoleData GetCfgRoleData()
    {
      return GetRoleFactory().GetCfgRoleData(this.GetId());
    }

    //////////////////////OnXXX/////////////////////////////////////
    public virtual bool OnCheckAddRole(User user)
    {
      return true;
    }

    public virtual bool OnAddRole(User user)
    {
      return true;
    }

    public virtual bool OnCheckRemoveRole(User user)
    {
      return true;
    }

    public virtual bool OnRemoveRole(User user)
    {
      return true;
    }
  }
}