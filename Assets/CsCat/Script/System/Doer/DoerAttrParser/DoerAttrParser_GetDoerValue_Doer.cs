using UnityEngine;

namespace CsCat
{
	public partial class DoerAttrParser
	{
		public bool GetDoerValue_Doer(Doer doer, string key, string type_string, out string result)
		{
			bool isBreak = false;
			result = null;
			if (doer is Doer)
			{
				if (key.StartsWith(StringConst.String_env_dot) || key.StartsWith(StringConst.String_envt_dot))
				{
					Doer env = doer.GetEnv();
					if (env != null)
					{
						key = key.Substring(StringConst.String_env.Length);
						DoerAttrParser doerAttrParser = new DoerAttrParser(env);
						result = doerAttrParser.ParseString(type_string + StringConst.String_u + key);
						return true;
					}

					result = ConvertValue(StringConst.String_Empty, type_string);
					return true;
				}

				if (key.StartsWith(StringConst.String_pos2))
				{
					key = key.Substring(StringConst.String_pos2.Length);
					Vector2 pos2 = doer.GetPos2();
					if (pos2 != Vector2Const.Default)
					{
						if (key.Equals(StringConst.String_dot_x))
						{
							result = ConvertValue(pos2.x, type_string);
							return true;
						}

						if (key.Equals(StringConst.String_dot_y))
						{
							result = ConvertValue(pos2.y, type_string);
							return true;
						}

						result = ConvertValue(pos2.ToString(), type_string);
						return true;
					}

					result = ConvertValue(StringConst.String_Empty, type_string);
					return true;
				}

				if (key.StartsWith(StringConst.String_pos3))
				{
					key = key.Substring(StringConst.String_pos3.Length);
					Vector3 pos3 = doer.GetPos3();
					if (pos3 != Vector3Const.Default)
					{
						if (key.Equals(StringConst.String_dot_x))
						{
							result = ConvertValue(pos3.x, type_string);
							return true;
						}

						if (key.Equals(StringConst.String_dot_y))
						{
							result = ConvertValue(pos3.y, type_string);
							return true;
						}

						if (key.Equals(StringConst.String_dot_z))
						{
							result = ConvertValue(pos3.z, type_string);
							return true;
						}

						result = ConvertValue(pos3.ToString(), type_string);
						return true;
					}

					result = ConvertValue(StringConst.String_Empty, type_string);
					return true;
				}
			}

			return isBreak;
		}
	}
}