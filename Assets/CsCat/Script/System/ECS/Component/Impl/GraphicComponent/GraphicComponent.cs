using UnityEngine;

namespace CsCat
{
	public partial class GraphicComponent : GameComponent
	{
		private ResLoadComponentPlugin resLoadComponentPlugin;
		private GameObjectEntity _gameObjectEntity;

		public void Init(ResLoadComponent resLoadComponent)
		{
			base.Init();
			resLoadComponentPlugin = new ResLoadComponentPlugin(resLoadComponent);
			this._gameObjectEntity = this.GetEntity<GameObjectEntity>();
			this.AddListener(GetGameEntity().eventDispatchers, ECSEventNameConst.OnAllAssetsLoadDone,
				OnAllAssetsLoadDone);
		}

		public void ApplyTransformComponent(TransformComponent transformComponent)
		{
			if (this._transform == null)
				return;
			this._transform.SetParent(transformComponent.GetParentTransform(),
				this._gameObject.layer != LayerMask.NameToLayer("UI"));
			this._transform.localPosition = transformComponent.GetLocalPosition();
			this._transform.localEulerAngles = transformComponent.GetLocalEulerAngles();
			this._transform.localScale = transformComponent.GetLocalScale();
		}

		protected override void _Reset()
		{
			base._Reset();
			resLoadComponentPlugin.Destroy();
			DestroyGameObject();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			resLoadComponentPlugin.Destroy();
			DestroyGameObject();


			resLoadComponentPlugin = null;

			_parentTransform = null;
			_gameObject = null;
			_isNotDestroyGameObject = false;
			prefab = null;
			_prefabPath = null;
			prefabAssetCat = null;
			isLoadDone = false;
			_isHide = false;
		}
	}
}