namespace CsCat
{
    public partial class DoerAttrSetter
    {
        public bool SetObjectValue_User(Doer doer, string key, object objectValue, bool isAdd)
        {
            bool isBreak = false;
            if (doer is User user)
            {
                if (objectValue is string value)
                {
                    if (this.SetObjectValue_User_AddAttrEquip(user, key, value, isAdd))
                        return true;

                    if (this.SetObjectValue_User_Missions(user, key, value, isAdd))
                        return true;

                    if (this.SetObjectValue_User_Items(user, key, value, isAdd))
                        return true;
                }
            }

            return isBreak;
        }
    }
}