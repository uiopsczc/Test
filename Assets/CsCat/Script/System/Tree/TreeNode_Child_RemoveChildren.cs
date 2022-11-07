using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
	{

		public void RemoveChildren(Type[] childTypes)
		{
			for (var i = 0; i < childTypes.Length; i++)
			{
				var componentType = childTypes[i];
				this.RemoveChild(componentType);
			}
		}
	}
}