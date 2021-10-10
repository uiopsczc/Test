using System.Collections;

namespace CsCat
{
  public partial class User : Thing
  {
    private ItemBag o_item_bag;
    private Roles o_roles;
    private Missions o_missions;
    public Role main_role;

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
    public override void DoSave(Hashtable dict, Hashtable dict_tmp)
    {
      base.DoSave(dict, dict_tmp);
      //存储角色
      this.o_roles.DoSave(dict, dict_tmp);
      //存储背包
      this.o_item_bag.DoSave(dict, dict_tmp);
      //存储任务
      this.o_missions.DoSave(dict, dict_tmp);
      if (this.main_role != null)
        dict["main_role_rid"] = this.main_role.GetRid();
    }

    //还原
    public override void DoRestore(Hashtable dict, Hashtable dict_tmp)
    {
      //还原角色
      this.o_roles.DoRestore(dict, dict_tmp);
      //还原背包
      this.o_item_bag.DoRestore(dict, dict_tmp);
      //还原任务
      this.o_missions.DoRestore(dict, dict_tmp);

      string main_role_rid = dict.Remove3<string>("main_role_rid");
      this.main_role = this.GetRole(main_role_rid);

      base.DoRestore(dict, dict_tmp);
    }

    //////////////////////OnXXX/////////////////////////////////////



    //////////////////////////////////////////Util///////////////////////////////////









  }
}