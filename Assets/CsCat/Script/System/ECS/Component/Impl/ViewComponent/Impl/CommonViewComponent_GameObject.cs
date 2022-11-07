using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
	public partial class CommonViewComponent
	{
		private bool _isNotDestroyGameObject;

		public virtual GameObject InstantiateGameObject(GameObject prefab)
		{
			return Object.Instantiate(prefab);
		}

		protected virtual void InitGameObjectChildren()
		{
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
	}
}