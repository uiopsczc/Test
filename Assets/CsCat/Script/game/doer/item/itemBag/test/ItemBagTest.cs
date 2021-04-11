using System.Collections;

namespace CsCat
{
  public static class ItemBagTest
  {
    public static void Test()
    {
      User user = Client.instance.user;
      user.AddItems("4", 4);
      user.UseItem("4", user.main_role);
//    Client.instance.user.DealItems(new Dictionary<string, string>() { { "4", "4" }, { "5", "5\"hhhhhp:6\"" } });

      var dict = new Hashtable();
      var dict_tmp = new Hashtable();
      user.DoSave(dict, dict_tmp);
      LogCat.log(dict, dict_tmp);
    }

  }
}