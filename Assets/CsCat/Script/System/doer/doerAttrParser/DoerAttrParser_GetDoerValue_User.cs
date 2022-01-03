namespace CsCat
{
	public partial class DoerAttrParser
	{
		public bool GetDoerValue_User(Doer doer, string key, string typeString, out string result)
		{
			bool isBreak = false;
			result = null;
			if (doer is User user)
			{
				if (GetDoerValue_User_Missions(user, key, typeString, out result))
					return true;
				if (GetDoerValue_User_Items(user, key, typeString, out result))
					return true;
			}

			return isBreak;
		}
	}
}