namespace CsCat
{
  public partial class GraphicComponent : GameComponent
  {
    private ResLoadComponentPlugin resLoadComponentPlugin;
    public void Init(ResLoadComponent resLoadComponent)
    {
      base.Init();
      resLoadComponentPlugin = new ResLoadComponentPlugin(resLoadComponent);
      this.AddListener(GetGameEntity().eventDispatchers,ECSEventNameConst.OnAllAssetsLoadDone, OnAllAssetsLoadDone);
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

      parent_transform = null;
      gameObject = null;
      is_not_destroy_gameObject = false;
      prefab = null;
      _prefab_path = null;
      prefab_assetCat = null;
      is_load_done = false;
      is_hide = false;

    }

  }
}