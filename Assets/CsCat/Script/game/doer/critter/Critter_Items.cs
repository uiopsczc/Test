namespace CsCat
{
  public partial class Critter
  {
    //////////////////////////////////////////Item///////////////////////////////////
    public bool CheckUseItem(Item item)
    {
      return this.OnCheckUseItem(item) && item.OnCheckUseItem(this);
    }

    public bool UseItem(Item item)
    {
      var env = item.GetEnv();
      if (env != null)
      {
        LogCat.error(string.Format("UseItem error:{0} still in {1}", item, env));
        return false;
      }

      return this.OnUseItem(item) && item.OnUseItem(this);
    }

    //////////////////////OnXXX/////////////////////////////////////
    public virtual bool OnCheckUseItem(Item item)
    {
      return true;
    }

    public virtual bool OnUseItem(Item item)
    {
      return true;
    }

    ////////////////////////////////////////////////////////////////////
    public void SetUser(User user)
    {
      this.SetTmp("o_user", user);
    }

    public User GetUser()
    {
      return this.GetTmp<User>("o_user");
    }
  }
}