using System.Collections;

namespace CsCat
{
	public static class MissionTest
	{
		public static void Test1()
		{
			User user = Client.instance.user;
			user.AcceptMission("1", user);

			var dict = new Hashtable();
			var dictTmp = new Hashtable();
			user.DoSave(dict, dictTmp);
			LogCat.log(dict, dictTmp);
		}

		public static void Test2()
		{
			User user = Client.instance.user;
			user.FinishMission("1", user);

			var dict = new Hashtable();
			var dictTmp = new Hashtable();
			user.DoSave(dict, dictTmp);
			LogCat.log(dict, dictTmp);
		}

		public static void Test3()
		{
			User user = Client.instance.user;
			user.GiveUpMission("1", user);

			var dict = new Hashtable();
			var dictTmp = new Hashtable();
			user.DoSave(dict, dictTmp);
			LogCat.log(dict, dictTmp);
		}

	}
}