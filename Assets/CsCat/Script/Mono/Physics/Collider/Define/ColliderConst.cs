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
				{ColliderType.attack, new ColliderTypeInfo(ColliderType.attack, Color.red)},
				{ColliderType.hit, new ColliderTypeInfo(ColliderType.hit, Color.green)}
			};
	}
}