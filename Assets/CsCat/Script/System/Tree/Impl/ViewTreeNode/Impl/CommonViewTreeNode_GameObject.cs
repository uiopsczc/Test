using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
	public partial class CommonViewTreeNode
	{

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

		protected virtual void DoSetGameObject(GameObject gameObject)
		{
			_SetGameObject(gameObject);
			_PostSetGameObject();
		}

		protected virtual void _SetGameObject(GameObject gameObject)
		{
			Transform transform = gameObject == null ? null : gameObject.transform;
			ApplyToTransform(transform);
		}

		protected virtual void _PostSetGameObject()
		{
			_InitGameObjectChildren();
		}

		protected virtual void _DestroyGameObject()
		{
			var gameObject = this.GetGameObject();
			if (gameObject != null)
				gameObject.Destroy();
		}

		private void _Reset_GameObject()
		{
			_DestroyGameObject();
		}

		private void _Destroy_GameObject()
		{
			_DestroyGameObject();
		}
	}
}