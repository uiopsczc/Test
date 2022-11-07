using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{

		public void RemoveComponents(Type[] componentTypes)
		{
			for (var i = 0; i < componentTypes.Length; i++)
			{
				var componentType = componentTypes[i];
				this.RemoveComponent(componentType);
			}
		}
	}
}