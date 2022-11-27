using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
	public partial class CommonViewTreeNode
	{
		private bool _isNotDestroyGameObject;

		public virtual GameObject InstantiateGameObject(GameObject prefab)
		{
			return Object.Instantiate(prefab);
		}

		protected virtual void _OnInstantiateGameObject()
		{
		}

		protected virtual void InitGameObjectChildren()
		{
		}

		//是否加载完预设且创建完gameObject
		protected bool IsGameObjectInited()
		{
			return this.GetGameObject() != null;
		}


		public virtual void SetGameObject(GameObject gameObject, bool? isNotDestroyGameObject = false)
		{
			Transform transform = gameObject == null ? null : gameObject.transform;
			ApplyToTransform(transform);
			if (gameObject == null)
				return;
			this._isNotDestroyGameObject = isNotDestroyGameObject.Value;
			InitGameObjectChildren();
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