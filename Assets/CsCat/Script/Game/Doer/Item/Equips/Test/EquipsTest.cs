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
			user.PutOnEquip("5", user.mainRole);
			user.PutOnEquip("6", user.mainRole);

			var dict = new Hashtable();
			var dict_tmp = new Hashtable();
			user.DoSave(dict, dict_tmp);
			LogCat.log(dict, dict_tmp);
		}

	}
}