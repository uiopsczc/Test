using System.Collections;

namespace CsCat
{
	public static class EmbedsTest
	{
		public static void Test()
		{
			User user = Client.instance.user;
			user.AddItems("5", 5);
			user.AddItems("6", 6);
			user.PutOnEquip("6", user.main_role);
			user.EmbedOn(user.main_role.GetEquipOfTypes("装备", "武器"), "5");
			//    user.EmbedOff(user.main_role.GetEquipOfTypes("装备", "武器"), "5");

			var dict = new Hashtable();
			var dict_tmp = new Hashtable();
			user.DoSave(dict, dict_tmp);
			LogCat.log(dict, dict_tmp);
		}


	}
}