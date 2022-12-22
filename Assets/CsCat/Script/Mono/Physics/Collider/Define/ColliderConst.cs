using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public static class ColliderConst
	{
		public static Dictionary<ColliderType, ColliderTypeInfo> ColliderInfoDict =
			new Dictionary<ColliderType, ColliderTypeInfo>()
			{
				{ColliderType.Attack, new ColliderTypeInfo(ColliderType.Attack, Color.red)},
				{ColliderType.Hit, new ColliderTypeInfo(ColliderType.Hit, Color.green)}
			};
	}
}