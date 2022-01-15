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
			var dictTmp = new Hashtable();
			user.DoSave(dict, dictTmp);
			LogCat.log(dict, dictTmp);
		}

	}
}