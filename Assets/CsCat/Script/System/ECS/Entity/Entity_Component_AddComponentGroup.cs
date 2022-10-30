using System;
using System.CodeDom;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		public void AddComponentsWithoutInit(Type[] componentTypes)
		{
			for (var i = 0; i < componentTypes.Length; i++)
			{
				var componentType = componentTypes[i];
				this.AddComponentWithoutInit(componentType);
			}
		}

		public void AddComponents(Type[] componentTypes)
		{
			for (var i = 0; i < componentTypes.Length; i++)
			{
				var componentType = componentTypes[i];
				this.AddComponent(componentType);
			}
		}
	}
}