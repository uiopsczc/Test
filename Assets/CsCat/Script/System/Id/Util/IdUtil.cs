namespace CsCat
{
	public class IdUtil
	{
		// 拼凑rid
		private static string JoinRid(string id, string seq)
		{
			return id + IdConst.Rid_Infix + seq;
		}

		// 判断是否rid
		public static bool IsRid(string str)
		{
			return str.IndexOf(IdConst.Rid_Infix) != -1;
		}

		// 判断是否id
		public static bool IsId(string str)
		{
			return str.IndexOf(IdConst.Rid_Infix) == -1;
		}

		// 通过rid得到id
		public static string RidToId(string rid)
		{
			int index = rid.IndexOf(IdConst.Rid_Infix);
			if (index != -1)
				return rid.Substring(0, index);
			return rid;
		}

		public static bool IsIdOrRidEquals(string idOrRid, string id, string rid)
		{
			if (IsId(idOrRid))
				return idOrRid.Equals(id);

			return idOrRid.Equals(rid);
		}

		//生成序号
		private static string NewSeq()
		{
			return TimeUtil.GetNowTimestamp().ToString();
		}

		// 生成一个新的rid
		public static string NewRid(string id)
		{
			return JoinRid(id, NewSeq());
		}

		// 获得运行时标识后缀序号
		public static string RidToSeq(string rid)
		{
			int index = rid.IndexOf(IdConst.Rid_Infix);
			return index != -1 ? rid.Substring(index + 1) : StringConst.String_Empty;
		}
	}
}