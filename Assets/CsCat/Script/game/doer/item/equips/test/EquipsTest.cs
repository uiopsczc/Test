using System.Collections;

namespace CsCat
{
	public static class EquipsTest
	{
		public static void Test()
		{
			User user = Client.instance.user;
			user.AddItems("5", 5);
			user.AddItems("6", 6);
			user.PutOnEquip("5", user.main_role);
			user.PutOnEquip("6", user.main_role);

			var dict = new Hashtable();
			var dict_tmp = new Hashtable();
			user.DoSave(dict, dict_tmp);
			LogCat.log(dict, dict_tmp);
		}

	}
}