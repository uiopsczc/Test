using System.Collections.Generic;

namespace CsCat
{
	public class UpdateComponent : Component
	{
		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			foreach (var component in this.GetEntity().ForeachComponent())
			{
				if(component is UpdateComponent)
					continue;
				component.Update(deltaTime, unscaledDeltaTime);
			}
		}

		protected override void _FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._FixedUpdate(deltaTime, unscaledDeltaTime);
			foreach (var component in this.GetEntity().ForeachComponent())
			{
				if (component is UpdateComponent)
					continue;
				component.FixedUpdate(deltaTime, unscaledDeltaTime);
			}
		}

		protected override void _LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._LateUpdate(deltaTime, unscaledDeltaTime);
			foreach (var component in this.GetEntity().ForeachComponent())
			{
				if (component is UpdateComponent)
					continue;
				component.LateUpdate(deltaTime, unscaledDeltaTime);
			}
		}
	}
}