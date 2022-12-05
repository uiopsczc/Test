namespace CsCat
{
	public class UpdateComponent : Component
	{
		protected override bool _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if(!base._Update(deltaTime, unscaledDeltaTime))
				return false;
			foreach (var component in this.GetEntity().ForeachComponent())
			{
				if(component is UpdateComponent)
					continue;
				component.Update(deltaTime, unscaledDeltaTime);
			}
			return true;
		}

		protected override bool _FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (!base._FixedUpdate(deltaTime, unscaledDeltaTime))
				return false;
			foreach (var component in this.GetEntity().ForeachComponent())
			{
				if (component is UpdateComponent)
					continue;
				component.FixedUpdate(deltaTime, unscaledDeltaTime);
			}
			return true;
		}

		protected override bool _LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (base._LateUpdate(deltaTime, unscaledDeltaTime))
				return false;
			foreach (var component in this.GetEntity().ForeachComponent())
			{
				if (component is UpdateComponent)
					continue;
				component.LateUpdate(deltaTime, unscaledDeltaTime);
			}

			return true;
		}
	}
}