namespace CsCat
{
  public partial class Item
  {
    ///////////////////////////////////////装备//////////////////////////////
    ///////////////////////////////////////OnXXX//////////////////////////////
    public virtual bool OnCheckPutOnEquip(Critter critter)
    {
      return true;
    }

    public virtual bool OnPutOnEquip(Critter critter)
    {
      return true;
    }


    public virtual bool OnCheckTakeOffEquip(Critter critter)
    {
      return true;
    }

    public virtual bool OnTakeOffEquip(Critter critter)
    {
      return true;
    }
  }
}