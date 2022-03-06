using System.Collections.Generic;

namespace CsCat
{
	public class AutoGenLineCfgInfo
	{
		public const string kUniqueKey = "uniqueKey";
		public const string kIsDeleteIfNotExist = "isDeleteIfNotExist";

		private  string _startsWith;
		private  bool _isDeleteIfNotExist;
		private  string _uniqueKey;

		public AutoGenLineCfgInfo()
		{
		}

		public AutoGenLineCfgInfo(string startsWith, string uniqueKey, bool isDeleteIfNotExist = false)
		{
			this._startsWith = startsWith;
			this._uniqueKey = uniqueKey;
			this._isDeleteIfNotExist = isDeleteIfNotExist;
		}

		public void Parse(string cfgPartStartsWith, string cfgDictString)
		{
			this._startsWith = cfgPartStartsWith;
			Dictionary<string, string> cfgDict = cfgDictString.ToDictionary<string, string>();
			if (cfgDict.ContainsKey("isDeleteIfNotExist"))
				this._isDeleteIfNotExist = cfgDict["isDeleteIfNotExist"].To<bool>();
			if (cfgDict.TryGetValue(kUniqueKey, out var uniqueKey))
				this._uniqueKey = uniqueKey;
			if (cfgDict.TryGetValue(kIsDeleteIfNotExist, out var isDeleteIfNotExist))
				this._isDeleteIfNotExist = isDeleteIfNotExist.To<bool>();
		}

		public string GetStartsWith()
		{
			return this._startsWith;
		}

		public string GetUniqueKey()
		{
			return this._uniqueKey;
		}

		public bool IsDeleteIfNotExist()
		{
			return this._isDeleteIfNotExist;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is AutoGenLineCfgInfo))
				return false;
			var other = (AutoGenLineCfgInfo)obj;
			return ObjectUtil.Equals(this._startsWith, other._startsWith) &&
			       ObjectUtil.Equals(this._uniqueKey, other._uniqueKey) &&
			       this.IsDeleteIfNotExist() == other.IsDeleteIfNotExist();
		}

		public override string ToString()
		{
			if (_startsWith == null)
				return null;

			Dictionary<string, string> cfgDict = new Dictionary<string, string> {[kUniqueKey] = _uniqueKey};
			if (IsDeleteIfNotExist())
				cfgDict[kIsDeleteIfNotExist] = IsDeleteIfNotExist().ToString();
			return _startsWith + cfgDict.ToString2().ReplaceAll("\r").ReplaceAll("\n");
		}
	}
}