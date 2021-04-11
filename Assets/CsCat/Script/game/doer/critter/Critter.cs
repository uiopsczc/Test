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
    public override void DoSave(Hashtable dict, Hashtable dict_tmp)
    {
      base.DoSave(dict, dict_tmp);
      //存储装备
      this.o_equips.DoSave(dict, dict_tmp);
    }

    //还原
    public override void DoRestore(Hashtable dict, Hashtable dict_tmp)
    {
      //还原装备
      this.o_equips.DoRestore(dict, dict_tmp);
      base.DoRestore(dict, dict_tmp);
    }

    //////////////////////OnXXX/////////////////////////////////////




  }
}