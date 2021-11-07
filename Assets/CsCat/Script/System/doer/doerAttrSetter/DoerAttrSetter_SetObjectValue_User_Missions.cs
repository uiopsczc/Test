namespace CsCat
{
    public partial class DoerAttrSetter
    {
        public bool SetObjectValue_User_Missions(User user, string key, object objectValue, bool isAdd)
        {
            bool isBreak = false;
            if (key.StartsWith(StringConst.String_missions_dot)) //任务对象
            {
                key = key.Substring(StringConst.String_missions_dot.Length);
                int pos = key.IndexOf(CharConst.Char_Dot);
                if (pos != -1)
                {
                    string missionId = key.Substring(0, pos);
                    key = key.Substring(pos);
                    if (missionId.EndsWith(StringConst.String_t)) //改变tmpValue
                    {
                        missionId = missionId.Substring(0, missionId.Length - 1);
                        key = StringConst.String_t + key;
                    }

                    var mission = user.GetMission(missionId);
                    if (mission != null)
                    {
                        DoerAttrSetter doerAttrSetter = new DoerAttrSetter(desc);
                        doerAttrSetter.SetU(mission);
                        doerAttrSetter.SetObject(StringConst.String_u + key, objectValue, isAdd);
                    }
                }

                return true;
            }

            return isBreak;
        }
    }
}