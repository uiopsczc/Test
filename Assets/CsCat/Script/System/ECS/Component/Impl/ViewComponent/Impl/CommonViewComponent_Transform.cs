using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
	public partial class CommonViewComponent
	{
		private Transform _parentTransform;

		public void SetParentTransform(Transform parentTransform)
		{
			this._parentTransform = parentTransform;
			var transform = this.GetTransform();
			if (transform != null)
				transform.SetParent(this._parentTransform,
					!LayerMask.LayerToName(this.GetGameObject().layer).Equals("UI"));
		}

		public Transform GetParentTransform()
		{
			return this._parentTransform;
		}
	}
}