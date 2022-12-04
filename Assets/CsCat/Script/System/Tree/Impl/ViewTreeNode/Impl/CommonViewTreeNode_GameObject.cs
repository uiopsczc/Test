using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
	public partial class CommonViewTreeNode
	{
		private bool _isNotDestroyGameObject;

		protected virtual GameObject _DoInstantiateGameObject(GameObject prefab)
		{
			GameObject clone = _InstantiateGameObject(prefab);
			_PostInstantiateGameObject();
			return clone;
		}

		protected virtual GameObject _InstantiateGameObject(GameObject prefab)
		{
			return Object.Instantiate(prefab);
		}

		protected virtual void _PostInstantiateGameObject()
		{
		}

		protected virtual void _InitGameObjectChildren()
		{
		}

		//是否加载完预设且创建完gameObject
		protected bool _IsGameObjectInited()
		{
			return this.GetGameObject() != null;
		}

		protected virtual void DoSetGameObject(GameObject gameObject, bool? isNotDestroyGameObject = false)
		{
			_SetGameObject(gameObject, isNotDestroyGameObject);
			_PostSetGameObject();
		}

		protected virtual void _SetGameObject(GameObject gameObject, bool? isNotDestroyGameObject = false)
		{
			Transform transform = gameObject == null ? null : gameObject.transform;
			ApplyToTransform(transform);
			if (gameObject == null)
				return;
			this._isNotDestroyGameObject = isNotDestroyGameObject.Value;
		}

		protected virtual void _PostSetGameObject()
		{
			_InitGameObjectChildren();
		}

		public virtual void DestroyGameObject()
		{
			var gameObject = this.GetGameObject();
			if (gameObject != null && !_isNotDestroyGameObject)
				gameObject.Destroy();
		}

		private void _Reset_GameObject()
		{
			_isNotDestroyGameObject = false;
		}

		protected void _Destroy_GameObject()
		{
			_isNotDestroyGameObject = false;
		}
	}
}