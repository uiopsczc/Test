using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TypeUtil
	{
		private static readonly Dictionary<string, Type> _cacheDict = new Dictionary<string, Type>();
		public static Dictionary<string, Type> GetCacheDict() => _cacheDict;
	}
}