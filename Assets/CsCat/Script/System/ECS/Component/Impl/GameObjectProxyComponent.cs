using UnityEngine;

namespace CsCat
{
	public partial class GameObjectProxyComponent : Component
	{
		private GameObjectProxy _gameObjectProxy;
		protected void _Init(GameObject gameObject)
		{
			this._gameObjectProxy = new GameObjectProxy(gameObject);
		}

		public void SetActive(bool isActive)
		{
			this._gameObjectProxy.SetActive(isActive);
		}

		public bool IsActive()
		{
			return this._gameObjectProxy.IsActive();
		}

	}
}