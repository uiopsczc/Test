using System;

namespace CsCat
{
	public partial class Unit
	{
		public AIBaseComp aiComp;

		public void RunAI(string aiClassPath)
		{
			Type aiClass = !aiClassPath.IsNullOrWhiteSpace() ? TypeUtil.GetType(aiClassPath) : typeof(AIBaseComp);
			if (this.aiComp != null)
				this.RemoveChild(this.aiComp.key);
			this.aiComp = this.AddChild(null, aiClass, this) as AIBaseComp;
		}
	}
}