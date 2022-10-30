using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{

		public void RemoveComponentGroup(ComponentGroupType componentGroupType)
		{
			var componentTypeList = componentGroupType.GetComponentTypeList();
			for (var i = 0; i < componentTypeList.Count; i++)
			{
				var componentType = componentTypeList[i];
				this.RemoveComponent(componentType);
			}
		}
	}
}