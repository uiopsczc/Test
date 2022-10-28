using System.Collections.Generic;

namespace CsCat
{
	public class TickObject : GameEntity
	{
		private TickObject child;
		private Component component;
		

		protected override bool isNotDeleteChildRelationshipImmediately => true;

		protected override bool isNotDeleteComponentRelationShipImmediately => true;

		public TickObject parentTickObject => _cache.GetOrAddDefault("parent_tickObject", () => parent as TickObject);


		public override bool IsCanUpdate()
		{
			return !isCanNotUpdate && base.IsCanUpdate();
		}


		public virtual void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (!this.IsCanUpdate()) return;
			for (var i = 0; i < childKeyList.Count; i++)
			{
				var childKey = childKeyList[i];
				child = GetChild(childKey) as TickObject;
				child?.Update(deltaTime, unscaledDeltaTime);
			}

			foreach (var componentKey in componentPoolIndexList)
			{
				component = GetComponent(componentKey);
				component?.Update(deltaTime, unscaledDeltaTime);
			}

			_Update(deltaTime, unscaledDeltaTime);
		}

		public virtual void FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (!this.IsCanUpdate()) return;
			for (var i = 0; i < childKeyList.Count; i++)
			{
				var childKey = childKeyList[i];
				child = GetChild(childKey) as TickObject;
				child?.FixedUpdate(deltaTime, unscaledDeltaTime);
			}

			for (var i = 0; i < componentPoolIndexList.Count; i++)
			{
				var componentKey = componentPoolIndexList[i];
				component = GetComponent(componentKey);
				component?.FixedUpdate(deltaTime, unscaledDeltaTime);
			}

			_FixedUpdate(deltaTime, unscaledDeltaTime);
		}


		public virtual void LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (!this.IsCanUpdate()) return;
			for (var i = 0; i < childKeyList.Count; i++)
			{
				var childKey = childKeyList[i];
				child = GetChild(childKey) as TickObject;
				child?.LateUpdate(deltaTime, unscaledDeltaTime);
			}

			for (var i = 0; i < componentPoolIndexList.Count; i++)
			{
				var componentKey = componentPoolIndexList[i];
				component = GetComponent(componentKey);
				component?.LateUpdate(deltaTime, unscaledDeltaTime);
			}

			_LateUpdate(deltaTime, unscaledDeltaTime);
		}


		protected virtual void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
		}

		protected virtual void _FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
		}

		protected virtual void _LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
		}

		protected override void _Destroy()
		{
			base._Destroy();
			isCanNotUpdate = false;
		}
	}
}