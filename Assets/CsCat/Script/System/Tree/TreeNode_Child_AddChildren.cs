using System;

namespace CsCat
{
	public partial class TreeNode
	{
		public void AddChildrenWithoutInit(Type[] childTypes)
		{
			for (var i = 0; i < childTypes.Length; i++)
			{
				var childType = childTypes[i];
				this.AddChildWithoutInit(childType);
			}
		}

		public void AddChildren(Type[] childTypes)
		{
			for (var i = 0; i < childTypes.Length; i++)
			{
				var childType = childTypes[i];
				this.AddChild(childType, null);
			}
		}
	}
}