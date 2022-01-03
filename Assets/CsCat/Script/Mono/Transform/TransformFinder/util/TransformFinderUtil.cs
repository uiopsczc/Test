using System.Collections.Generic;

namespace CsCat
{
	public static class TransformFinderUtil
	{
		public static List<TransformFinderBase> CreateInstanceList()
		{
			List<TransformFinderBase> list = new List<TransformFinderBase>();
			foreach (var transformFinderInfo in TransformFinderConst.transformFinderInfo_list)
				list.Add(transformFinderInfo.CreateInstance());
			return list;
		}
	}
}




