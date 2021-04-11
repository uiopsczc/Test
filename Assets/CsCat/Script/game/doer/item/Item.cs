using System.Collections;

namespace CsCat
{
  public partial class Item : Thing
  {
    private Embeds o_embeds;

    public override void Init()
    {
      base.Init();
      SetCount(1);
      o_embeds = new Embeds(this, "o_embeds");
    }

    public ItemFactory GetItemFactory()
    {
      return this.factory as ItemFactory;
    }

    public ItemDefinition GetItemDefinition()
    {
      return GetItemFactory().GetItemDefinition(this.GetId());
    }

    ///////////////////////////////////////DoXXX//////////////////////////////
    //卸载
    public override void DoRelease()
    {
      // 销毁镶物
      this.o_embeds.DoRelease();
      base.DoRelease();
    }

    //保存
    public override void DoSave(Hashtable dict, Hashtable dict_tmp)
    {
      base.DoSave(dict, dict_tmp);
      // 存储镶物
      this.o_embeds.DoSave(dict, dict_tmp);
    }

    //还原
    public override void DoRestore(Hashtable dict, Hashtable dict_tmp)
    {
      // 还原镶物
      this.o_embeds.DoRestore(dict, dict_tmp);
      base.DoRestore(dict, dict_tmp);
    }


    ///////////////////////////////////////OnXXX//////////////////////////////

    public virtual bool OnCheckUseItem(Critter critter)
    {
      return true;
    }

    public virtual bool OnUseItem(Critter critter)
    {
      return true;
    }



    /////////////////////////////Util/////////////////////////////////////////
    public void SetGroup(string group)
    {
      SetTmp("group", group);
    }

    public string GetGroup()
    {
      return GetTmp("group", "");
    }

    public bool IsWeapon()
    {
      return this.IsType1(ItemConst.Item_Type1_Weapon);
    }

    public bool IsArmor()
    {
      return this.IsType1(ItemConst.Item_Type1_Armor);
    }

    public bool IsEmb()
    {
      return this.IsType1(ItemConst.Item_Type1_Embed);
    }

    public bool IsEquip()
    {
      return this.IsWeapon() || this.IsArmor();
    }

    public bool IsType1(string type_1)
    {
      return type_1.Equals(this.GetType1());
    }

    public bool IsType2(string type_2)
    {
      return type_2.Equals(this.GetType2());
    }

    public bool IsPutOn()
    {
      return this.GetTmp("o_is_put_on", false);
    }

    public void SetIsPutOn(bool is_put_on)
    {
      this.SetTmp("o_is_put_on", is_put_on);
    }

  }
}