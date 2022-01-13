using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public static class TransformFinderConst
	{
		public static List<TransformFinderInfo> transformFinderInfoList = new List<TransformFinderInfo>()
		{
			new TransformFinderInfo(typeof(TransformFinder0)),
			new TransformFinderInfo(typeof(TransformFinder1)),
		};

		private static Dictionary<Type, TransformFinderInfo> _transformFinderInfoDict;

		public static Dictionary<Type, TransformFinderInfo> transformFinderInfoDict
		{
			get
			{
				if (_transformFinderInfoDict == null)
				{
					_transformFinderInfoDict = new Dictionary<Type, TransformFinderInfo>();
					for (var i = 0; i < transformFinderInfoList.Count; i++)
					{
						var transformFinderInfo = transformFinderInfoList[i];
						_transformFinderInfoDict[transformFinderInfo.transformFinderType] = transformFinderInfo;
					}
				}

				return _transformFinderInfoDict;
			}
		}
	}
}