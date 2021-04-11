namespace CsCat
{
  public class HuiFuJi : Item
  {
    ///////////////////////////////////////OnXXX//////////////////////////////

    public override bool OnUseItem(Critter critter)
    {
      critter.Set("mp", 3);
      return true;
    }
  }
}