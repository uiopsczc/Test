using System.Collections;

namespace CsCat
{
	public static class DoerAttrSetterTest
	{
		public static void Test()
		{
			DoerAttrSetter doerAttrSetter = new DoerAttrSetter(null, new DoerAttrParser(Client.instance.user));
			var result = "";
			//    doerAttrSetter.Set("u.hp","{eval(4+5)}",false);


			var dict = new Hashtable();
			var dict_tmp = new Hashtable();
			Client.instance.user.DoSave(dict, dict_tmp);
			LogCat.log(dict, dict_tmp);
			//    LogCat.log(result);
		}
	}
}