using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity
	{
		public bool isCanNotUpdate = false;
		public virtual bool IsCanUpdate()
		{
			return !isCanNotUpdate&&isEnabled && !this.isPaused && !IsDestroyed();
		}

		public virtual void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (!this.IsCanUpdate())
				return;
			for (var i = 0; i < childKeyList.Count; i++)
			{
				var childKey = childKeyList[i];
				var child = GetChild(childKey);
				child.DoDestroy();
				child?.Update(deltaTime, unscaledDeltaTime);
				if (child.IsDestroyed())
					i--;
			}

			foreach (var componentKey in componentKeyList)
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

			for (var i = 0; i < componentKeyList.Count; i++)
			{
				var componentKey = componentKeyList[i];
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

			for (var i = 0; i < componentKeyList.Count; i++)
			{
				var componentKey = componentKeyList[i];
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

		void _OnReset_Update()
		{
			isCanNotUpdate = false;
		}

		void _OnDespawn_Update()
		{
			isCanNotUpdate = false;
		}
	}
}