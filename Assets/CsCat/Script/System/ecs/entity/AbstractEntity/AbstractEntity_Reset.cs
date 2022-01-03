using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity
	{
		public Action resetCallback;


		public void Reset(bool isLoopChildren = false)
		{
			if (isLoopChildren)
				ResetAllChildren();
			ResetAllComponents();
			_Reset();
			_PostReset();
		}

		protected virtual void _Reset()
		{
		}

		protected virtual void _PostReset()
		{
			resetCallback?.Invoke();
			resetCallback = null;
		}

		public void ResetAllChildren()
		{
			foreach (var child in ForeachChild())
				child.Reset(true);
		}

		public void ResetAllComponents()
		{
			foreach (var component in ForeachComponent())
				component.Reset();
		}

		void _OnDespawn_Reset()
		{
			resetCallback = null;
		}
	}
}