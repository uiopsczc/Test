namespace CsCat
{
	public partial class DoerAttrParser
	{
		public bool GetDoerValue_User_Missions(User user, string key, string typeString, out string result)
		{
			bool isBreak = false;
			result = null;
			if (key.StartsWith(StringConst.String_missions_dot)) //任务对象
			{
				key = key.Substring(StringConst.String_missions_dot.Length);
				string missionId;
				int pos = key.IndexOf(CharConst.Char_Dot);
				if (pos != -1)
				{
					missionId = key.Substring(0, pos);
					key = key.Substring(pos);
					if (missionId.EndsWith(StringConst.String_t)) //修改tmpValue
					{
						missionId = missionId.Substring(0, missionId.Length - 1);
						key = StringConst.String_t + key;
					}
				}
				else
				{
					missionId = key;
					key = StringConst.String_Empty;
				}

				Mission mission = user.GetMission(missionId);
				if (mission != null) //身上有这个任务
				{
					if (key.Length > 0)
					{
						DoerAttrParser doerAttrParser = new DoerAttrParser(mission);
						result = doerAttrParser.ParseString(typeString + StringConst.String_u + key);
						return true;
					}

					result = ConvertValue(StringConst.String_1, typeString);
					return true;
				}

				if (key.StartsWith(StringConst.String_dot_status))
				{
					if (user.GetFinishedMissionIds().Contains(missionId))
					{
						result = ConvertValue(4, typeString); // 已完成
						return true;
					}

					result = ConvertValue(0, typeString); // 未接到
					return true;
				}

				return true;
			}

			return isBreak;
		}
	}
}