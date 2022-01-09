namespace CsCat
{
	public partial class DoerAttrSetter
	{
		public bool SetObjectValue_Doer(Doer doer, string key, object objectValue, bool isAdd)
		{
			bool isBreak = false;
			if (doer is Doer)
			{
				if (key.StartsWith(StringConst.String_env_dot) || key.StartsWith(StringConst.String_envt_dot)) //获得属性所在的环境
				{
					Doer env = doer.GetEnv();
					if (env != null)
					{
						key = key.Substring(StringConst.String_env.Length);
						DoerAttrSetter attrAttrSetter = new DoerAttrSetter(desc);
						attrAttrSetter.SetU(env);
						attrAttrSetter.SetObject(StringConst.String_u + key, objectValue, isAdd);
					}

					return true;
				}

				if (key.StartsWith(StringConst.String_ownerDot) ||
					key.StartsWith(StringConst.String_ownertDot)) //获得属性所在的环境
				{
					Doer owner = doer.GetOwner();
					if (owner != null)
					{
						key = key.Substring(StringConst.String_owner.Length);
						DoerAttrSetter attrAttrSetter = new DoerAttrSetter(desc);
						attrAttrSetter.SetU(owner);
						attrAttrSetter.SetObject(StringConst.String_u + key, objectValue, isAdd);
					}

					return true;
				}
			}

			return isBreak;
		}
	}
}