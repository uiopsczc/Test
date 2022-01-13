using System.Collections.Generic;

namespace CsCat
{
	public static class TransformFinderUtil
	{
		public static List<TransformFinderBase> CreateInstanceList()
		{
			List<TransformFinderBase> list = new List<TransformFinderBase>();
			for (var i = 0; i < TransformFinderConst.transformFinderInfoList.Count; i++)
			{
				var transformFinderInfo = TransformFinderConst.transformFinderInfoList[i];
				list.Add(transformFinderInfo.CreateInstance());
			}

			return list;
		}
	}
}