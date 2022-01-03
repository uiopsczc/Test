using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public static class TransformFinderConst
	{
		public static List<TransformFinderInfo> transformFinderInfo_list = new List<TransformFinderInfo>()
	{
	  new TransformFinderInfo(typeof(TransformFinder0)),
	  new TransformFinderInfo(typeof(TransformFinder1)),
	};

		private static Dictionary<Type, TransformFinderInfo> _transformFinderInfo_dict;

		public static Dictionary<Type, TransformFinderInfo> transformFinderInfo_dict
		{
			get
			{
				if (_transformFinderInfo_dict == null)
				{
					_transformFinderInfo_dict = new Dictionary<Type, TransformFinderInfo>();
					foreach (var transformFinderInfo in transformFinderInfo_list)
						_transformFinderInfo_dict[transformFinderInfo.transformFinder_type] = transformFinderInfo;
				}
				return _transformFinderInfo_dict;
			}
		}

	}
}




