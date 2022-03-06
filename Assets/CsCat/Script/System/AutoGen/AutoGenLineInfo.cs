namespace CsCat
{
	public class AutoGenLineInfo
	{
		public string mainPart;
		public AutoGenLineCfgInfo cfgInfo;

		public AutoGenLineInfo(string mainPart, string cfgPart, string cfgPartStartsWith)
		{
			this.mainPart = mainPart;
			this.cfgInfo = cfgPart.ParseAutoGenLineCfgInfo(cfgPartStartsWith);
		}

		public AutoGenLineInfo(string mainPart, AutoGenLineCfgInfo cfgInfo)
		{
			this.mainPart = mainPart;
			this.cfgInfo = cfgInfo;
		}

		public string ToLine()
		{
			if (this.cfgInfo == null)
				return mainPart;
			return mainPart + cfgInfo.ToString();
		}

		public bool IsDeleteIfNotExist()
		{
			if (cfgInfo == null)
				return false;
			return cfgInfo.IsDeleteIfNotExist();
		}

		public bool IsMatch(AutoGenLineInfo autoGenLineInfo)
		{
			if (autoGenLineInfo.cfgInfo == null && this.cfgInfo == null)
				return ObjectUtil.Equals(autoGenLineInfo.mainPart, this.mainPart);
			if (autoGenLineInfo.cfgInfo != null && this.cfgInfo != null)
				return ObjectUtil.Equals(autoGenLineInfo.cfgInfo, this.cfgInfo);
			return false;
		}


	}
}