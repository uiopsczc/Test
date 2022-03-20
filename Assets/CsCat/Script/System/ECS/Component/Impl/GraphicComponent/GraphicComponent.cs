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

			parentTransform = null;
			gameObject = null;
			isNotDestroyGameObject = false;
			prefab = null;
			_prefabPath = null;
			prefabAssetCat = null;
			isLoadDone = false;
			isHide = false;
		}
	}
}