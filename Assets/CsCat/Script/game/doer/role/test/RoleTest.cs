using System.Collections;

namespace CsCat
{
	public static class RoleTest
	{
		public static void Test()
		{
			User user = Client.instance.user;
			user.AddRole("1");

			var dict = new Hashtable();
			var dict_tmp = new Hashtable();
			user.DoSave(dict, dict_tmp);
			LogCat.log(dict, dict_tmp);
		}

	}
}